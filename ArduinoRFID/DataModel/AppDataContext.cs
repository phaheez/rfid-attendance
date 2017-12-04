using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace ArduinoRFID.DataModel
{
    public class AppDataContext
    {
        readonly string _path = Environment.CurrentDirectory + @"\AttendanceRFID.json";


        public ObservableCollection<Student> Students { get; set; }

        public ObservableCollection<Course> Courses { get; set; }

        public ObservableCollection<Attendance> Attendance { get; set; }

        public ObservableCollection<AttendanceSheet> AttendanceSheets { get; set; }

        public AppDataContext()
        {
            //_path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), FolderName, DbName);
        }
        
        public void Load()
        {
            if (!File.Exists(_path))
            {
                File.Create(_path).Dispose();
            }

            var json = File.ReadAllText(_path, Encoding.UTF8);

            var db = JsonConvert.DeserializeObject<AppDataContext>(json);
            if (db == null)
                db = new AppDataContext();
            Students = db.Students;
            Courses = db.Courses;
            Attendance = db.Attendance;
            AttendanceSheets = db.AttendanceSheets;
        }

        public void SaveData(AppDataContext applicationDb)
        {
            if ( !File.Exists(_path) )
            {
                File.Create(_path).Dispose();
            }

            var json = JsonConvert.SerializeObject(applicationDb, Formatting.Indented,
                new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
            File.WriteAllText(_path, json);
        }
    }
}
