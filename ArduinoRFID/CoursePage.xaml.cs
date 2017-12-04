using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Objects;
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

namespace ArduinoRFID
{
    /// <summary>
    /// Interaction logic for CoursePage.xaml
    /// </summary>
    public partial class CoursePage : Page
    {
        private readonly RFIDEntities _db = new RFIDEntities();
        private readonly RfidAttendanceRepo _repo = new RfidAttendanceRepo();

        private readonly List<Course> _defaultViewModel;

        public List<Course> DefaultViewModel
        {
            get { return _defaultViewModel; }
        }

        public CoursePage()
        {
            InitializeComponent();

            _repo.Load();

            var context = _db.Courses.ToList();
            _defaultViewModel = context;
        }

        private void CoursePage_OnLoaded(object sender, RoutedEventArgs e)
        {
            ItemListView.ItemsSource = DefaultViewModel;
        }

        private void BtnAddCourse_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if ( string.IsNullOrEmpty(TxtNewTitle.Text) || string.IsNullOrEmpty(TxtNewCode.Text) )
                {
                    MessageBox.Show(@"All field required");
                    return;
                }

                var course = new Course {
                    Code = TxtNewCode.Text.Trim(),
                    Title = TxtNewTitle.Text.Trim()
                };

                var maxId = ( _db.Courses.Select(p => (int?)p.ID).Max() ?? 0 ) + 1;

                course.ID = maxId;

                try
                {
                    _db.Courses.AddObject(course);
                }
                catch
                {
                    _repo.Courses = new List<Course> { course };
                }

                _db.SaveChanges();

                MessageBox.Show("Course Added");
                Refresh();
                TxtNewTitle.Clear();
                TxtNewCode.Clear();
                
                ItemListView.ScrollIntoView(course);
                ItemListView.SelectedIndex = course.ID - 1;
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
                if ( ItemListView.SelectedItem == null )
                    return;
                var course = ItemListView.SelectedItem as Course;
                _db.Courses.Attach(course);
                _db.Courses.DeleteObject(course);
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
                if ( ItemListView.SelectedItem == null )
                    return;
                var single = ItemListView.SelectedItem as Course;
                var course = _db.Courses.Single(p => p.ID == single.ID);
                course.Code = TxtUpdateCode.Text.Trim();
                course.Title = TxtUpdateTitle.Text.Trim();
                _db.SaveChanges();
                MessageBox.Show("Changes made successfully");
                Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Refresh()
        {
            ItemListView.ItemsSource = _db.Courses.ToList();
            var index = ItemListView.SelectedIndex;
            ItemListView.SelectedIndex = -1;
            ItemListView.SelectedIndex = index;
        }
    }
}







/*private readonly ObservableCollection<Course> _defaultViewModel = new ObservableCollection<Course>();
        private readonly AppDataContext _appData = new AppDataContext();

        public ObservableCollection<Course> DefaultViewModel
        {
            get { return this._defaultViewModel; }
        }

        public CoursePage()
        {
            InitializeComponent();

            _appData.Load();

            var context = _appData.Courses;
            if ( context != null )
                _defaultViewModel = context;
        }

        private void CoursePage_OnLoaded(object sender, RoutedEventArgs e)
        {
            ItemListView.ItemsSource = DefaultViewModel;
        }

        private void BtnAddCourse_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if ( string.IsNullOrEmpty(TxtNewTitle.Text) || string.IsNullOrEmpty(TxtNewCode.Text) )
                {
                    MessageBox.Show(@"All field required");
                    return;
                }

                var course = new Course {
                    Title = TxtNewTitle.Text.Trim(),
                    Code = TxtNewCode.Text.Trim()
                };

                try
                {
                    var lastOrDefault = _appData.Courses.LastOrDefault();
                    if ( lastOrDefault != null )
                        course.Id = lastOrDefault.Id + 1;
                }
                catch
                {
                    course.Id = 1;
                }

                try
                {
                    _appData.Courses.Add(course);
                }
                catch
                {
                    _appData.Courses = new ObservableCollection<DataModel.Course> { course };
                }

                _appData.SaveData(_appData);
                TxtNewTitle.Clear();
                TxtNewCode.Clear();
                Refresh();
                ItemListView.ScrollIntoView(course);
                ItemListView.SelectedIndex = course.Id - 1;
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
                if ( ItemListView.SelectedItem == null )
                    return;
                var course = ItemListView.SelectedItem as Course;
                _appData.Courses.Remove(course);
                _appData.SaveData(_appData);
                Refresh();
            }
            catch ( Exception ex )
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if ( ItemListView.SelectedItem == null )
                return;
            _appData.SaveData(_appData);
            MessageBox.Show("Changes made successfully");
            Refresh();
        }

        private void Refresh()
        {
            ItemListView.ItemsSource = _appData.Courses;
            var index = ItemListView.SelectedIndex;
            ItemListView.SelectedIndex = -1;
            ItemListView.SelectedIndex = index;
        }*/
