using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArduinoRFID.DataModel
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string MatricNo { get; set; }
        public string Department { get; set; }
        public string TagId { get; set; }
        public List<int> CoursesId { get; set; }
    }
}
