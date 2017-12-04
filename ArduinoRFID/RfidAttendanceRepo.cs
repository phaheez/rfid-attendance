using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace ArduinoRFID
{
    public class RfidAttendanceRepo
    {
        private readonly RFIDEntities _db = new RFIDEntities();


        public List<Student> Students { get; set; }

        public List<Course> Courses { get; set; }

        public List<Attendance> Attendances { get; set; }

        public List<AttendanceSheet> AttendanceSheets { get; set; }



        public void Load()
        {
            try
            {
                using ( var db = new RFIDEntities() )
                {
                    Students = db.Students.ToList();
                    Courses = db.Courses.ToList();
                    Attendances = db.Attendances.ToList();
                    AttendanceSheets = db.AttendanceSheets.ToList();
                }
            }
            catch ( Exception ex )
            {
                MessageBox.Show(ex.Message);
            }
        }

        /*public List<string> ReturnSheduledCourse()
        {
            var date = DateTime.Now.Date.ToString("d");
            var cCode = new List<string>();
            var id =
                _db.Attendances.Where(
                    p => string.Compare(p.CreatedDate, date, StringComparison.InvariantCulture) == 0)
                    .Select(p => p.CourseID)
                    .ToList();
            foreach ( var code in id )
            {
                var singleOrDefault = _db.Courses.Single(p => p.ID == code);
                if ( singleOrDefault != null )
                {
                    var res = singleOrDefault.Code;
                    cCode.Add(res);
                }
            }
            return cCode;
        }

        public void SaveAttendanceSheetToDb(string matricno, int courseId)
        {
            string date = DateTime.Now.Date.ToString("d");

            int sheetId = IsSheetExist(matricno, courseId);
            int attendanceId = 0;

            var singleOrDefault = _db.Attendances.SingleOrDefault(p => p.CourseID == courseId && String.Compare(p.CreatedDate, date, StringComparison.InvariantCulture) >= 0);
            if ( singleOrDefault != null )
            {
                attendanceId = singleOrDefault.ID;
            }

            var startTime = ReturnStartTime(courseId);
            var endTime = ReturnEndTime(courseId);

            var arrivalTime = Convert.ToDateTime(DateTime.Now.ToString("t"));

            if ( startTime == "" && endTime == "" && attendanceId == 0 )
            {
                //access denied
                //send "Course Attendance Has Due" message to arduino
                MessageBox.Show(@"This Course Attendance Has Due.", @"Student Attendance System Using FingerPrint");
                return;
            }

            if ( sheetId > 0 )
            {
                //access denied
                //send "Already Enroll For This Course" message to arduino
                MessageBox.Show("You Have Already Enroll \nFor This Course.", @"Student Attendance System Using FingerPrint");
                return;
            }

            var startT = Convert.ToDateTime(startTime);
            var endT = Convert.ToDateTime(endTime);

            if ( arrivalTime < startT ) //if lecture never started
            {
                //access denied
                MessageBox.Show("Is Not Yet Time For This Course Attendance \nPlease Come Back Later.", @"Student Attendance System Using FingerPrint", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if ( arrivalTime > endT ) //if lecture has finished
            {
                //access denied
                MessageBox.Show("You Are Already Late For This Course \nPlease Go Back Home.", @"Student Attendance System Using FingerPrint", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var sheet = new AttendanceSheet //save student attendance record to database
            {
                AttendanceID = attendanceId,
                MatricNo = matricno,
                ArrivalTime = DateTime.Now.ToString("t")
            };

            var maxId = ( _db.AttendanceSheets.Select(p => (int?)p.ID).Max() ?? 0 ) + 1;

            sheet.ID = maxId;

            try
            {
                _db.AttendanceSheets.AddObject(sheet);
            }
            catch
            {
                AttendanceSheets = new List<AttendanceSheet> { sheet };
            }

        }

        public int IsSheetExist(string matric, int courseId)
        {
            string date = DateTime.Now.Date.ToString("d");
            var res =
                _db.Attendances.SingleOrDefault(
                    p =>
                        p.CourseID == courseId &&
                        String.Compare(p.CreatedDate, date, StringComparison.InvariantCulture) >= 0);
            int ret = 0;
            if ( res != null )
            {
                int attendid = res.ID;
                ret =
                    _db.AttendanceSheets.Where(p => p.MatricNo == matric && p.AttendanceID == attendid)
                        .Select(p => p.ID)
                        .SingleOrDefault();
            }
            return ret;
        }

        public int ReturnAttendanceId(int courseId)
        {
            string date = DateTime.Now.Date.ToString("d");
            var res = _db.Attendances.SingleOrDefault(p => p.CourseID == courseId && String.Compare(p.CreatedDate, date, StringComparison.InvariantCulture) >= 0);
            return res != null ? res.ID : 0;
        }

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
        }*/
    }
}
