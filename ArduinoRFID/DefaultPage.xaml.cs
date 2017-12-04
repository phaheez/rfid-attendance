using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using ArduinoRFID.DataModel;

namespace ArduinoRFID
{
    /// <summary>
    /// Interaction logic for DefaultPage.xaml
    /// </summary>
    public partial class DefaultPage : Page
    {
        private readonly RFIDEntities _db = new RFIDEntities();
        private readonly RfidAttendanceRepo _repo = new RfidAttendanceRepo();

        private readonly SerialPort _serialPort = new SerialPort();
        private string _receivedData;

        public DefaultPage()
        {
            InitializeComponent();

            GetAvailablePorts();

            BtnConnect.IsEnabled = false;
            BtnEnroll.IsEnabled = false;

            CbxCourse.ItemsSource = ReturnSheduledCourse();
            //CbxCourse.ItemsSource = _db.Courses;
        }

        private void ArduinoPortName_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BtnConnect.IsEnabled = true;
        }

        private void BtnConnect_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if ( (string)BtnConnect.Content == "Connect" )
                {
                    _serialPort.PortName = ArduinoPortName.Text;
                    _serialPort.BaudRate = 9600;
                    _serialPort.Parity = Parity.None;
                    _serialPort.StopBits = StopBits.One;
                    _serialPort.DataBits = 8;
                    _serialPort.Handshake = Handshake.None;
                    _serialPort.RtsEnable = true;
                    if ( !( _serialPort.IsOpen ) )
                        _serialPort.Open();
                    BtnConnect.Content = "Disconnect";
                    ArduinoPortName.IsEnabled = false;

                    BtnEnroll.IsEnabled = true;

                    _serialPort.DataReceived += SerialPortOnDataReceived;

                    return;
                }

                if ( (string)BtnConnect.Content == "Disconnect" )
                {
                    if ( _serialPort.IsOpen )
                        _serialPort.Close();
                    BtnConnect.Content = "Connect";
                    BtnEnroll.IsEnabled = false;
                    ArduinoPortName.IsEnabled = true;
                }
            }
            catch ( Exception ex )
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SerialPortOnDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            _repo.Load();

            _serialPort.NewLine = "\r\n";
            _receivedData = _serialPort.ReadLine();

            try
            {
                string tagId = _receivedData;
                string cmd;

                var tag = _db.Students.SingleOrDefault(s => s.TagID == tagId);

                if ( tag == null )
                {
                    cmd = "ACCESS_DENIED";
                    SendMessage(cmd);
                    return;
                }

                var student = _db.Students.Single(s => s.MatricNo == tag.MatricNo);

                Dispatcher.Invoke((Action)( () => { Course c = CbxCourse.SelectedItem as Course; } ));
                Dispatcher.Invoke(new Action(() => { }), DispatcherPriority.ContextIdle);
                
                Course course = CbxCourse.SelectedItem as Course;

                //var course = _db.Courses.FirstOrDefault(p => p.Code == (string)CbxCourse.SelectedItem);

                string date = DateTime.Now.Date.ToString("d");

                int attendanceId = 0;

                var singleOrDefault = _db.Attendances.SingleOrDefault(p => p.CourseID == course.ID && String.Compare(p.CreatedDate, date, StringComparison.InvariantCulture) >= 0);
                if ( singleOrDefault != null )
                {
                    attendanceId = singleOrDefault.ID;
                }

                if (course != null)
                {
                    var startTime = ReturnStartTime(course.ID);
                    var endTime = ReturnEndTime(course.ID);
                    var arrivalTime = Convert.ToDateTime(DateTime.Now.ToString("t"));

                    var startT = Convert.ToDateTime(startTime);
                    var endT = Convert.ToDateTime(endTime);

                    if ( arrivalTime < startT ) //if lecture never started
                    {
                        //access denied
                        cmd = "ACCESS_DENIED";
                        SendMessage(cmd);
                        return;
                    }

                    if ( arrivalTime > endT ) //if lecture has finished
                    {
                        //access denied
                        cmd = "ACCESS_DENIED";
                        SendMessage(cmd);
                        return;
                    }
                }

                var sheet = new AttendanceSheet //save student attendance record to database
                {
                    AttendanceID = attendanceId,
                    MatricNo = student.MatricNo,
                    ArrivalTime = DateTime.Now.ToString("t")
                };

                var maxId = ( _db.AttendanceSheets.Select(p => (int?)p.ID).Max() ?? 0 ) + 1;

                sheet.ID = maxId;

                //check if student has already entered
                var exist =
                    _db.AttendanceSheets.FirstOrDefault(
                        ( a => a.MatricNo == sheet.MatricNo && a.AttendanceID == sheet.AttendanceID ));

                if ( exist != null )
                {
                    //access denied
                    cmd = "ACCESS_DENIED";
                    SendMessage(cmd);
                    return;
                }

                try
                {
                    _db.AttendanceSheets.AddObject(sheet);
                }
                catch ( Exception )
                {
                    _repo.AttendanceSheets = new List<AttendanceSheet> { sheet };
                }
                _db.SaveChanges();

                cmd = "ACCESS_GRANTED";
                SendMessage(cmd);
            }
            catch ( Exception ex )
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BtnEnroll_Click(object sender, RoutedEventArgs e)
        {
            //_serialPort.DataReceived += SerialPortOnDataReceived;
        }

        private void SendMessage(string message)
        {
            _serialPort.Write(message);
        }

        private void GetAvailablePorts()
        {
            var ports = SerialPort.GetPortNames();
            var port = ports.ToList();
            ArduinoPortName.ItemsSource = port;
        }


        /// <summary>
        /// External Method
        /// </summary>
        /// <param name="courseId"></param>
        /// <returns></returns>

        public string ReturnStartTime(int courseId)
        {
            int cid = _db.Courses.Where(p => p.ID == courseId).Select(p => p.ID).SingleOrDefault();
            string date = DateTime.Now.Date.ToString("d");
            var ret = _db.Attendances.SingleOrDefault(p => p.CourseID == cid && string.Compare(p.CreatedDate, date, StringComparison.InvariantCulture) >= 0);
            return ret != null ? ret.StartTime : "";
        }

        public string ReturnEndTime(int courseId)
        {
            int cid = _db.Courses.Where(p => p.ID == courseId).Select(p => p.ID).SingleOrDefault();
            string date = DateTime.Now.Date.ToString("d");
            var ret = _db.Attendances.SingleOrDefault(p => p.CourseID == cid && String.Compare(p.CreatedDate, date, StringComparison.InvariantCulture) >= 0);
            return ret != null ? ret.EndTime : "";
        }

        public List<string> ReturnSheduledCourse()
        {
            var date = DateTime.Now.Date.ToString("d");
            var id =
                _db.Attendances.Where(
                    p => string.Compare(p.CreatedDate, date, StringComparison.InvariantCulture) == 0)
                    .Select(p => p.CourseID)
                    .ToList();
            List<string> list = new List<string>();
            foreach (var code in id)
            {
                var singleOrDefault = _db.Courses.SingleOrDefault(p => p.ID == code);
                if (singleOrDefault != null) list.Add(singleOrDefault.Code);
            }
            return list;
        }
    }
}

/*private readonly AppDataContext _appData = new AppDataContext();

        private readonly SerialPort _serialPort = new SerialPort();
        private string _receivedData;

        public DefaultPage()
        {
            InitializeComponent();

            GetAvailablePorts();

            BtnConnect.IsEnabled = false;
            BtnEnroll.IsEnabled = false;
            //ReturnAllAttendanceCourseCode();
        }

        private void ArduinoPortName_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BtnConnect.IsEnabled = true;
        }

        private void BtnConnect_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if ( (string)BtnConnect.Content == "Connect" )
                {

                    _serialPort.PortName = ArduinoPortName.Text;
                    _serialPort.BaudRate = 9600;
                    _serialPort.Parity = Parity.None;
                    _serialPort.StopBits = StopBits.One;
                    _serialPort.DataBits = 8;
                    _serialPort.Handshake = Handshake.None;
                    _serialPort.RtsEnable = true;
                    if ( !( _serialPort.IsOpen ) )
                        _serialPort.Open();
                    BtnConnect.Content = "Disconnect";
                    ArduinoPortName.IsEnabled = false;

                    BtnEnroll.IsEnabled = true;

                    _serialPort.DataReceived += SerialPortOnDataReceived;

                    return;
                }

                if ( (string)BtnConnect.Content == "Disconnect" )
                {
                    if ( _serialPort.IsOpen )
                        _serialPort.Close();
                    BtnConnect.Content = "Connect";
                    BtnEnroll.IsEnabled = false;
                    ArduinoPortName.IsEnabled = true;
                }
            }
            catch ( Exception ex )
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SerialPortOnDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            _appData.Load();

            _serialPort.NewLine = "\r\n";
            _receivedData = _serialPort.ReadLine();

            try
            {
                string tagId = _receivedData;
                string cmd = "";
                //var matricNo = _db.Students.Where(s => s.TagId == tagId).Select(s => s.MatricNo).FirstOrDefault();
                var matricNo = _appData.Students.Where(s => s.TagId == tagId).Select(s => s.MatricNo).SingleOrDefault();

                if ( matricNo != null )
                {
                    var student = _appData.Students.FirstOrDefault(s => s.MatricNo == matricNo);

                    //issues
                    var currentAttendance =
                        _appData.Attendance.FirstOrDefault(a => ( a.StartTime <= DateTime.Now && a.StartTime.AddMinutes(a.Range) >= DateTime.Now ));

                    var attendanceAvailable = _appData.Attendance.FirstOrDefault(a => ( a.StartTime <= DateTime.Now && a.EndTime >= DateTime.Now ));

                    if ( attendanceAvailable == null ) //lecture room free
                    {
                        //Accees Deny
                        cmd = "ACCESS_GRANTED";
                        SendMessage(cmd);
                        return;
                    }

                    if ( student == null ) //student not found in DB
                    {
                        cmd = "ACCESS_DENIED";
                        //equate matric number
                        SendMessage(cmd);
                        return;
                    }

                    if ( currentAttendance == null ) //dont open if late too late for class
                    {
                        cmd = "ACCESS_DENIED";
                        SendMessage(cmd);
                        return;
                    }

                    //if attendace exist & student exist // check if student register for course
                    var isRegCourse = false;
                    if ( student != null )
                        foreach ( var courseId in student.CoursesId )
                        {
                            if ( currentAttendance.CourseId == courseId )
                            {
                                isRegCourse = true;
                                break;
                            }
                        }
                    //esle
                    if ( !isRegCourse ) //if current course is not registered
                    {
                        //send accessDeny
                        cmd = "ACCESS_DENIED";
                        SendMessage(cmd);
                        return;
                    }

                    var sheet = new AttendanceSheet {
                        ArrivalTime = DateTime.Now,
                        AttendanceId = currentAttendance.Id,
                        StudentId = student.Id,
                        Title = String.Format("{0}", _appData.Courses.FirstOrDefault(c => c.Id == currentAttendance.CourseId).Code),
                        SubTitle = String.Format("{0}", _appData.Attendance.FirstOrDefault(a => a.Id == currentAttendance.Id).StartTime.ToString("D"))
                    };

                    try
                    {
                        sheet.Id = _appData.AttendanceSheets.Count + 1;
                    }
                    catch
                    {
                        sheet.Id = 1;
                    }

                    //check if student has already entered
                    var exist = _appData.AttendanceSheets.FirstOrDefault(( a => a.StudentId == sheet.StudentId && a.AttendanceId == sheet.AttendanceId ));
                    if ( exist == null )
                    {
                        try
                        {
                            _appData.AttendanceSheets.Add(sheet);
                        }
                        catch
                        {
                            _appData.AttendanceSheets = new ObservableCollection<AttendanceSheet> { sheet };
                        }
                        _appData.SaveData(_appData);
                    }
                    //send data
                    cmd = "ACCESS_GRANTED";
                    SendMessage(cmd);
                    return;
                }
                //Accees Deny
                cmd = "ACCESS_DENIED";
                SendMessage(cmd);
            }
            catch ( Exception ex )
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BtnEnroll_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SendMessage(string message)
        {
            _serialPort.Write(message);
        }

        private void GetAvailablePorts()
        {
            var ports = SerialPort.GetPortNames();
            var port = ports.ToList();
            ArduinoPortName.ItemsSource = port;
        }

        private void ReturnAllAttendanceCourseCode()
        {
            try
            {
                var cCode = new List<string>();
                var id = _appData.Attendance.Select(p => p.CourseId).ToList();
                foreach ( var code in id )
                {
                    var singleOrDefault = _appData.Courses.SingleOrDefault(p => p.Id == code);
                    if ( singleOrDefault != null )
                    {
                        var res = singleOrDefault.Code;
                        cCode.Add(res);
                    }
                }
                ScheduledCourse.ItemsSource = cCode;
            }
            catch ( Exception ex )
            {
                MessageBox.Show(ex.Message);
            }
        }*/
