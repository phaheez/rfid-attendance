using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArduinoRFID.DataModel
{
    public class Attendance
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int CourseId { get; set; }
        public int Range { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }
    }
}
