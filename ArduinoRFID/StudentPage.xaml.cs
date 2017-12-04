using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
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
    /// Interaction logic for StudentPage.xaml
    /// </summary>
    public partial class StudentPage : Page
    {
        private readonly RFIDEntities _db = new RFIDEntities();
        private readonly RfidAttendanceRepo _repo = new RfidAttendanceRepo();

        private readonly List<Student> _defaultViewModel;

        public List<Student> DefaultViewModel
        {
            get { return _defaultViewModel; }
        }

        private readonly SerialPort _serialPort = new SerialPort();
        private string _receivedData;

        public StudentPage()
        {
            InitializeComponent();

            _repo.Load();

            GetAvailablePorts();
            LoadDept();

            BtnConnect.IsEnabled = false;
            BtnScanCard.IsEnabled = false;

            var context = _db.Students.ToList();
            _defaultViewModel = context;

            itemListView.SelectionChanged += itemListView_SelectionChanged;
        }

        private void StudentPage_OnLoaded(object sender, RoutedEventArgs e)
        {
            itemListView.ItemsSource = DefaultViewModel;

            //var selectedStudent = itemListView.SelectedItem as Student;
            //if ( selectedStudent != null )
            //    UpdateStdDept.ItemsSource = selectedStudent.Department;
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

                    BtnScanCard.IsEnabled = true;

                    _serialPort.DataReceived += SerialPortOnDataReceived;

                    return;
                }

                if ( (string)BtnConnect.Content == "Disconnect" )
                {
                    if ( _serialPort.IsOpen )
                        _serialPort.Close();
                    BtnConnect.Content = "Connect";
                    TxtTagId.Clear();
                    BtnScanCard.IsEnabled = false;
                    ArduinoPortName.IsEnabled = true;
                }
            }
            catch ( Exception ex )
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void TxtTagId_TextChanged(object sender, TextChangedEventArgs e)
        {
            if ( TxtTagId.Text != string.Empty )
            {
                BtnScanCard.IsEnabled = false;
                return;
            }
            BtnScanCard.IsEnabled = true;
        }

        private void BtnScanCard_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string cmd = "WRITE";
                SendMessage(cmd);
            }
            catch ( Exception ex )
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BtnAddStudent_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //if ( string.IsNullOrEmpty(TxtStudentName.Text) || string.IsNullOrEmpty(TxtMatricNo.Text) || string.IsNullOrEmpty(TxtTagId.Text) )
                if ( string.IsNullOrEmpty(TxtStudentName.Text) || string.IsNullOrEmpty(TxtMatricNo.Text) )
                {
                    MessageBox.Show(@"All field required");
                    return;
                }

                if ( IsMatricNo(TxtMatricNo.Text.Trim()) == false )
                {
                    MessageBox.Show("Enter a valid MatricNo!");
                    return;
                }

                // TODO: save to tag
                //WRITE
                string cmd = "WRITE";
                SendMessage(cmd);
                MessageBox.Show("Place card on RFID reader");
                
                Thread.Sleep(1000); // wait for (1sec) before saving to database
                //Thread.Sleep(TimeSpan.FromSeconds(1));
                //Task.Delay(100) --wait for a particular seconds before saving to db
                var res = _receivedData;

                var student = new Student
                {
                    Name = TxtStudentName.Text.Trim(),
                    MatricNo = TxtMatricNo.Text.Trim(),
                    //TagID = TxtTagId.Text.Trim(),
                    TagID = _receivedData,
                    Department = StudentDept.Text
                };

                var maxId = ( _db.Students.Select(p => (int?)p.ID).Max() ?? 0 ) + 1;

                student.ID = maxId;

                try
                {
                    _db.Students.AddObject(student);
                }
                catch
                {
                    _repo.Students = new List<Student> { student };
                }

                

                _db.SaveChanges();
                //MessageBox.Show("Created successfully");

                Refresh();
                itemListView.ScrollIntoView(student);

                TxtStudentName.Clear();
                TxtMatricNo.Clear();
                TxtTagId.Clear();


                /*_db.SaveChanges();
                Refresh();
                itemListView.ScrollIntoView(student);

                // TODO: successful account
                //ACCOUNT_CREATED
                string cmd = "ACCOUNT_CREATED";
                SendMessage(cmd);
                TxtStudentName.Clear();
                TxtMatricNo.Clear();
                TxtTagId.Clear();*/
            }
            catch ( Exception ex )
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void itemListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if ( itemListView.SelectedItem == null )
                return;
            var student = itemListView.SelectedItem as Student;
            _db.Students.Attach(student);
            _db.Students.DeleteObject(student);
            _db.SaveChanges();
            Refresh();
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if ( itemListView.SelectedItem == null )
                return;
            var single = itemListView.SelectedItem as Student;
            var student = _db.Students.Single(p => p.ID == single.ID);
            student.Name = UpdateStdName.Text.Trim();
            student.MatricNo = UpdateStdMatric.Text.Trim();
            //student.Department = UpdateStdDept.Text;
            _db.SaveChanges();
            MessageBox.Show("Changes made successfully");
            Refresh();
        }

        private void Refresh()
        {
            itemListView.ItemsSource = _db.Students.ToList();
            var index = itemListView.SelectedIndex;
            itemListView.SelectedIndex = -1;
            itemListView.SelectedIndex = index;
        }

        private bool IsMatricNo(string msg)
        {
            if ( msg.Length == 6 )
                return true;
            return false;
        }

        private async void SerialPortOnDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                _serialPort.NewLine = "\r\n";
                _receivedData = _serialPort.ReadLine();
                
                
                //Dispatcher.Invoke(DispatcherPriority.Send, new UpdateUiTextDelegate(DisplayData), _receivedData);
            }
            catch ( Exception ex )
            {
                MessageBox.Show(ex.Message);
            }

        }

        private delegate void UpdateUiTextDelegate(string text);
        private void DisplayData(string command)
        {
            TxtTagId.Text = command;
        }

        private void GetAvailablePorts()
        {
            var ports = SerialPort.GetPortNames();
            var port = ports.ToList();
            ArduinoPortName.ItemsSource = port;
        }

        private void SendMessage(string message)
        {
            _serialPort.Write(message);
        }

        private void LoadDept()
        {
            var dept = new List<string> { "CSE" };
            StudentDept.ItemsSource = dept;
            StudentDept.SelectedIndex = 0;
        }
    }
}

/*private readonly ObservableCollection<Student> _defaultViewModel = new ObservableCollection<Student>();
        private readonly AppDataContext _appData = new AppDataContext();

        private readonly SerialPort _serialPort = new SerialPort();
        private string received_data;

        public ObservableCollection<Student> DefaultViewModel
        {
            get { return this._defaultViewModel; }
        }

        public StudentPage()
        {
            InitializeComponent();

            _appData.Load();

            GetAvailablePorts();
            LoadDept();

            BtnConnect.IsEnabled = false;
            BtnScanCard.IsEnabled = false;

            var context = _appData.Students;
            if ( context != null )
                _defaultViewModel = context;

            StudentCourses.DataContext = _appData;
            var selectedStudent = itemListView.SelectedItem as Student;
            if ( selectedStudent != null )
            {
                var courses = new List<Course>();
                foreach ( var course in courses )
                {
                    foreach ( var studentCourse in selectedStudent.CoursesId )
                    {
                        if ( course.Id == studentCourse )
                            courses.Add(course);
                    }
                }
                CourseList.ItemsSource = courses;
            }

            itemListView.SelectionChanged += ItemListView_SelectionChanged;
        }

        private void StudentPage_OnLoaded(object sender, RoutedEventArgs e)
        {
            itemListView.ItemsSource = DefaultViewModel;
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

                    BtnScanCard.IsEnabled = true;

                    _serialPort.DataReceived += SerialPortOnDataReceived;

                    return;
                }

                if ( (string)BtnConnect.Content == "Disconnect" )
                {
                    if ( _serialPort.IsOpen )
                        _serialPort.Close();
                    BtnConnect.Content = "Connect";
                    TxtTagId.Clear();
                    BtnScanCard.IsEnabled = false;
                    ArduinoPortName.IsEnabled = true;
                }
            }
            catch ( Exception ex )
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void TxtTagId_TextChanged(object sender, TextChangedEventArgs e)
        {
            if ( TxtTagId.Text != string.Empty )
            {
                BtnScanCard.IsEnabled = false;
                return;
            }
            BtnScanCard.IsEnabled = true;
        }

        private void BtnScanCard_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string cmd = "WRITE";
                SendMessage(cmd);
            }
            catch ( Exception ex )
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BtnAddStudent_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if ( string.IsNullOrEmpty(TxtStudentName.Text) || string.IsNullOrEmpty(TxtMatricNo.Text) || string.IsNullOrEmpty(TxtTagId.Text) )
                {
                    MessageBox.Show(@"All field required");
                    return;
                }

                if ( IsMatricNo(TxtMatricNo.Text.Trim()) == false )
                {
                    MessageBox.Show("Enter a valid MatricNo!");
                    return;
                }

                var student = new Student
                {
                    Name = TxtStudentName.Text.Trim(),
                    MatricNo = TxtMatricNo.Text.Trim(),
                    TagId = TxtTagId.Text.Trim(),
                    CoursesId = new List<int>(),
                    Department = StudentDept.Text
                };

                try
                {
                    student.Id = _appData.Students.LastOrDefault().Id + 1;
                }
                catch
                {
                    student.Id = 1;
                }

                try
                {
                    _appData.Students.Add(student);
                }
                catch
                {
                    _appData.Students = new ObservableCollection<Student> { student };
                }

                _appData.SaveData(_appData);
                Refresh();
                itemListView.ScrollIntoView(student);

                // TODO: successful account
                //ACCOUNT_CREATED
                string cmd = "ACCOUNT_CREATED";
                SendMessage(cmd);
                TxtStudentName.Clear();
                TxtMatricNo.Clear();
                TxtTagId.Clear();
            }
            catch ( Exception ex )
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ItemListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedStudent = itemListView.SelectedItem as Student;
            if ( selectedStudent != null )
            {
                var courses = new List<Course>();

                foreach ( var course in _appData.Courses )
                {
                    foreach ( var studentCourse in selectedStudent.CoursesId )
                    {
                        if ( course.Id == studentCourse )
                            courses.Add(course);
                    }
                }
                CourseList.ItemsSource = courses;
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if(itemListView.SelectedItem == null)
                return;
            var student = itemListView.SelectedItem as Student;
            _appData.Students.Remove(student);
            _appData.SaveData(_appData);
            Refresh();
        }

        private void AddStudentCourse_Click(object sender, RoutedEventArgs e)
        {
            if ( itemListView.SelectedItem == null )
                return;
            var selectedCourse = StudentCourses.SelectionBoxItem as Course;
            if ( selectedCourse == null )
            {
                MessageBox.Show("Select a course");
                return;
            }

            var selectedStudent = itemListView.SelectedItem as Student;
            if ( selectedStudent != null )
                selectedStudent.CoursesId.Add(selectedCourse.Id);
            MessageBox.Show("Added Succefully");
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if ( itemListView.SelectedItem == null )
                return;
            _appData.SaveData(_appData);
            MessageBox.Show("Changes made successfully");
            Refresh();
        }

        private void Refresh()
        {
            itemListView.ItemsSource = _appData.Students;
            var index = itemListView.SelectedIndex;
            itemListView.SelectedIndex = -1;
            itemListView.SelectedIndex = index;
        }

        private bool IsMatricNo(string msg)
        {
            if ( msg.Length == 6 )
                return true;
            return false;
        }

        private void SerialPortOnDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                _serialPort.NewLine = "\r\n";
                received_data = _serialPort.ReadLine();
                //Dispatcher.Invoke(DispatcherPriority.Send, new EventHandler(DisplayText));
                Dispatcher.Invoke(DispatcherPriority.Send, new UpdateUiTextDelegate(DisplayData), received_data);
            }
            catch ( Exception ex )
            {
                MessageBox.Show(ex.Message);
            }

        }

        private delegate void UpdateUiTextDelegate(string text);
        private void DisplayData(string command)
        {
            TxtTagId.Text = command;
        }

        private void GetAvailablePorts()
        {
            var ports = SerialPort.GetPortNames();
            var port = ports.ToList();
            ArduinoPortName.ItemsSource = port;
        }

        private void SendMessage(string message)
        {
            _serialPort.Write(message);
        }

        private void LoadDept()
        {
            var dept = new List<string> { "CSE" };
            StudentDept.ItemsSource = dept;
            StudentDept.SelectedIndex = 0;
        }*/
