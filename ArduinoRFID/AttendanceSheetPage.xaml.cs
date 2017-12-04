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
    /// Interaction logic for AttendanceSheetPage.xaml
    /// </summary>
    public partial class AttendanceSheetPage : Page
    {
        private readonly RFIDEntities _db = new RFIDEntities();
        private readonly RfidAttendanceRepo _repo = new RfidAttendanceRepo();

        private readonly List<AttendanceSheet> _defaultViewModel;
        private List<AttendanceSheet> grp = new List<AttendanceSheet>();

        public List<AttendanceSheet> DefaultViewModel
        {
            get { return _defaultViewModel; }
        }

        public AttendanceSheetPage()
        {
            InitializeComponent();

            _repo.Load();

            var groupedData = _repo.AttendanceSheets;

            //var groupedData = _appData.AttendanceSheets.GroupBy(a => a.AttendanceId);
            if ( groupedData != null )
            {
                var res = groupedData.GroupBy(a => a.AttendanceID);
                foreach ( var g in res )
                {
                    foreach ( var sheet in g )
                    {
                        grp.Add(sheet);
                        break;
                    }
                }

                _defaultViewModel = grp;
            }
        }

        private void AttendanceSheetPage_OnLoaded(object sender, RoutedEventArgs e)
        {
            itemListView.ItemsSource = DefaultViewModel;
        }

        private void ItemListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selected = itemListView.SelectedItem as AttendanceSheet;

            var sheets = _repo.AttendanceSheets.Where(s => selected != null && s.AttendanceID == selected.AttendanceID);
            var students = new List<Student>();
            foreach ( var sheet in sheets )
            {
                var std = _repo.Students.FirstOrDefault(s => s.MatricNo == sheet.MatricNo);
                students.Add(std);
            }
            AttendaceSheetListView.ItemsSource = students;
            Subtitle.DataContext = new { Count = "Total Number of Students: " + students.Count };
        }

        private void AttendanceSheetExport_Click(object sender, RoutedEventArgs e)
        {
            //Export to Excel
        }

        private void AttendanceSheetDelete_Click(object sender, RoutedEventArgs e)
        {
            if ( itemListView.SelectedItem == null )
                return;
            var sheet = itemListView.SelectedItem as AttendanceSheet;
            var attendanceSheets = new AttendanceSheet[_repo.AttendanceSheets.Count];

            _repo.AttendanceSheets.CopyTo(attendanceSheets, 0);

            foreach ( var s in attendanceSheets )
            {
                if ( sheet != null && s.AttendanceID == sheet.AttendanceID )
                    _repo.AttendanceSheets.Remove(s);
            }
            _db.SaveChanges();
            //Refresh();
        }

        private void Refresh()
        {
            var groupedData = _repo.AttendanceSheets.GroupBy(a => a.AttendanceID);

            foreach ( var g in groupedData )
            {
                foreach ( var sheet in g )
                {
                    grp.Add(sheet);
                    break;
                }
            }

            itemListView.ItemsSource = grp;
            var index = itemListView.SelectedIndex;
            itemListView.SelectedIndex = -1;
            itemListView.SelectedIndex = index;
        }
    }
}


/*private readonly ObservableCollection<AttendanceSheet> _defaultViewModel = new ObservableCollection<AttendanceSheet>();
        private readonly AppDataContext _appData = new AppDataContext();
        private ObservableCollection<AttendanceSheet> grp = new ObservableCollection<AttendanceSheet>();
        
        public ObservableCollection<AttendanceSheet> DefaultViewModel
        {
            get { return this._defaultViewModel; }
        }

        public AttendanceSheetPage()
        {
            InitializeComponent();

            _appData.Load();

            var groupedData = _appData.AttendanceSheets;

            //var groupedData = _appData.AttendanceSheets.GroupBy(a => a.AttendanceId);
            if ( groupedData != null )
            {
                var res = groupedData.GroupBy(a => a.AttendanceId);
                foreach ( var g in res )
                {
                    foreach ( var sheet in g )
                    {
                        grp.Add(sheet);
                        break;
                    }
                }

                _defaultViewModel = grp;
            }
        }

        private void AttendanceSheetPage_OnLoaded(object sender, RoutedEventArgs e)
        {
            itemListView.ItemsSource = DefaultViewModel;
        }

        private void ItemListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selected = itemListView.SelectedItem as AttendanceSheet;

            var sheets = _appData.AttendanceSheets.Where(s => selected != null && s.AttendanceId == selected.AttendanceId);
            var students = new List<Student>();
            foreach ( var sheet in sheets )
            {
                var std = _appData.Students.FirstOrDefault(s => s.Id == sheet.StudentId);
                //var stdSheet = new StudentSheet { ArrivalTime = };
                students.Add(std);
            }
            AttendaceSheetListView.ItemsSource = students;
            Subtitle.DataContext = new { Count = "Total Number of Students: " + students.Count };
        }

        private void AttendanceSheetExport_Click(object sender, RoutedEventArgs e)
        {
            //Export to Excel
        }

        private void AttendanceSheetDelete_Click(object sender, RoutedEventArgs e)
        {
            if ( itemListView.SelectedItem == null )
                return;
            var sheet = itemListView.SelectedItem as AttendanceSheet;
            var attendanceSheets = new AttendanceSheet[_appData.AttendanceSheets.Count];

            _appData.AttendanceSheets.CopyTo(attendanceSheets, 0);

            foreach ( var s in attendanceSheets )
            {
                if ( sheet != null && s.AttendanceId == sheet.AttendanceId )
                    _appData.AttendanceSheets.Remove(s);
            }
            _appData.SaveData(_appData);
            //Refresh();
        }

        private void Refresh()
        {
            var groupedData = _appData.AttendanceSheets.GroupBy(a => a.AttendanceId);

            foreach ( var g in groupedData )
            {
                foreach ( var sheet in g )
                {
                    grp.Add(sheet);
                    break;
                }
            }

            itemListView.ItemsSource = grp;
            var index = itemListView.SelectedIndex;
            itemListView.SelectedIndex = -1;
            itemListView.SelectedIndex = index;
        }*/