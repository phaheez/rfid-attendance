using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using ArduinoRFID.DataModel;

namespace ArduinoRFID
{
    /// <summary>
    /// Interaction logic for AttendancePage.xaml
    /// </summary>
    public partial class AttendancePage : Page
    {
        private readonly RFIDEntities _db = new RFIDEntities();
        private readonly RfidAttendanceRepo _repo = new RfidAttendanceRepo();

        private readonly List<Attendance> _defaultViewModel;

        public List<Attendance> DefaultViewModel
        {
            get { return _defaultViewModel; }
        }

        public AttendancePage()
        {
            InitializeComponent();

            _repo.Load();

            var context = _db.Attendances.ToList();
            _defaultViewModel = context;
        }

        private void AttendancePage_OnLoaded(object sender, RoutedEventArgs e)
        {
            itemListView.ItemsSource = DefaultViewModel;
            AttendanceCourse.ItemsSource = _db.Courses;
        }

        private void BtnCreateAttendance_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if ( AttendanceDate.SelectedDate == null || AttendanceStartTime.Value == null || AttendanceEndTime.Value == null || TxtRange.Text == "" || AttendanceCourse.SelectedIndex == -1 )
                {
                    MessageBox.Show(@"Field(s) required");
                    return;
                }
                var selectedCourse = AttendanceCourse.SelectedItem as Course;
                if ( selectedCourse == null )
                    return;

                var start =
                    AttendanceDate.SelectedDate.Value.Date.Date.Add(AttendanceStartTime.Value.Value.TimeOfDay);
                var end =
                    AttendanceDate.SelectedDate.Value.Date.Date.Add(AttendanceEndTime.Value.Value.TimeOfDay);
                
                var startDateTime = AttendanceStartTime.Value.Value.ToString("t");
                var endDateTime = AttendanceEndTime.Value.Value.ToString("t");

                var attendance = new Attendance {
                    CourseID = selectedCourse.ID,
                    StartTime = startDateTime,
                    EndTime = endDateTime,
                    CreatedDate = AttendanceDate.SelectedDate.Value.ToString("d"),
                    //Range = int.Parse(TxtRange.Text.Trim()),
                    Title = string.Format("{0} {1}", selectedCourse.Title, selectedCourse.Code),
                    SubTitle =
                        string.Format("Start: {0}\nEnd: {1}", start.ToString("g"), end.ToString("g"))
                };

                var maxId = ( _db.Attendances.Select(p => (int?)p.ID).Max() ?? 0 ) + 1;

                attendance.ID = maxId;

                try
                {
                    _db.Attendances.AddObject(attendance);
                }
                catch
                {
                    _repo.Attendances = new List<Attendance> { attendance };
                }

                _db.SaveChanges();

                MessageBox.Show(@"Created Successfully");
                Refresh();

                AttendanceDate.SelectedDate = null;
                AttendanceCourse.SelectedIndex = -1;
                AttendanceStartTime.Text = "";
                AttendanceEndTime.Text = "";
                TxtRange.Clear();
                
                itemListView.ScrollIntoView(attendance);
                itemListView.SelectedIndex = attendance.ID - 1;
            }
            catch ( Exception ex )
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if ( itemListView.SelectedItem == null )
                    return;
                var attendance = itemListView.SelectedItem as Attendance;
                _db.Attendances.Attach(attendance);
                _db.Attendances.DeleteObject(attendance);
                _db.SaveChanges();
                Refresh();
            }
            catch ( Exception ex )
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if ( itemListView.SelectedItem == null )
                    return;
                
                if(UpdateStartTime.Value == null || UpdateEndTime.Value == null)
                    return;
                
                var single = itemListView.SelectedItem as Attendance;
                var attendance = _db.Attendances.Single(p => p.ID == single.ID);
                attendance.StartTime = UpdateStartTime.Value.Value.ToString("t");
                attendance.EndTime = UpdateEndTime.Value.Value.ToString("t");
                _db.SaveChanges();
                MessageBox.Show("Changes made successfully");
                Refresh();
            }
            catch ( Exception ex )
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Refresh()
        {
            itemListView.ItemsSource = _db.Attendances.ToList();
            //itemListView.ItemsSource = _repo.Attendances;
            var index = itemListView.SelectedIndex;
            itemListView.SelectedIndex = -1;
            itemListView.SelectedIndex = index;
        }
    }
}


/*private readonly ObservableCollection<Attendance> _defaultViewModel = new ObservableCollection<Attendance>();
        private readonly AppDataContext _appData = new AppDataContext();

        public ObservableCollection<Attendance> DefaultViewModel
        {
            get { return this._defaultViewModel; }
        }

        public AttendancePage()
        {
            InitializeComponent();

            _appData.Load();

            var context = _appData.Attendance;
            if ( context != null )
                _defaultViewModel = context;
        }

        private void AttendancePage_OnLoaded(object sender, RoutedEventArgs e)
        {
            itemListView.ItemsSource = DefaultViewModel;
            AttendanceCourse.DataContext = _appData;
        }

        private void BtnCreateAttendance_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if ( AttendanceDate.SelectedDate == null || AttendanceStartTime.Value == null || AttendanceEndTime.Value == null || TxtRange.Text == "" || AttendanceCourse.SelectedIndex == -1 )
                {
                    MessageBox.Show(@"Field(s) required");
                    return;
                }
                var selectedCourse = AttendanceCourse.SelectedItem as Course;
                if ( selectedCourse == null )
                    return;
                var startDateTime =
                    AttendanceDate.SelectedDate.Value.Date.Date.Add(AttendanceStartTime.Value.Value.TimeOfDay);
                var endDateTime =
                    AttendanceDate.SelectedDate.Value.Date.Date.Add(AttendanceEndTime.Value.Value.TimeOfDay);

                var attendance = new Attendance {
                    CourseId = selectedCourse.Id,
                    StartTime = startDateTime,
                    EndTime = endDateTime,
                    Range = int.Parse(TxtRange.Text.Trim()),
                    Title = string.Format("{0} {1}", selectedCourse.Title, selectedCourse.Code),
                    SubTitle =
                        string.Format("Start: {0}\nEnd: {1}", startDateTime.ToString("g"), endDateTime.ToString("g"))
                };

                try
                {
                    attendance.Id = _appData.Attendance.LastOrDefault().Id + 1;
                }
                catch
                {

                    attendance.Id = 1;
                }

                try
                {
                    _appData.Attendance.Add(attendance);
                }
                catch
                {

                    _appData.Attendance = new ObservableCollection<Attendance> { attendance };
                }

                _appData.SaveData(_appData);
                AttendanceDate.SelectedDate = null;
                AttendanceCourse.SelectedIndex = -1;
                AttendanceStartTime.Text = "";
                AttendanceEndTime.Text = "";
                TxtRange.Clear();
                Refresh();
                itemListView.ScrollIntoView(attendance);
                itemListView.SelectedIndex = attendance.Id - 1;
                MessageBox.Show(@"Created Successfully");
            }
            catch ( Exception ex )
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if ( itemListView.SelectedItem == null )
                return;
            var attendance = itemListView.SelectedItem as Attendance;
            _appData.Attendance.Remove(attendance);
            _appData.SaveData(_appData);
            Refresh();
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
            itemListView.ItemsSource = _appData.Attendance;
            var index = itemListView.SelectedIndex;
            itemListView.SelectedIndex = -1;
            itemListView.SelectedIndex = index;
        }*/