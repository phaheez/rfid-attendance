using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArduinoRFID.DataModel
{
    public class AttendanceSheet
    {
        public int Id { get; set; }
        public DateTime ArrivalTime { get; set; }
        public int StudentId { get; set; }
        public int AttendanceId { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }
    }

    public class GroupedSheet
    {
        public string Title { get; set; }
        public string SubTitle { get; set; }
    }

    public class StudentSheet : Student
    {
        public string ArrivalTime { get; set; }
    }
}
