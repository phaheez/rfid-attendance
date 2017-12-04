﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.ComponentModel;
using System.Data.EntityClient;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml.Serialization;

[assembly: EdmSchemaAttribute()]
namespace ArduinoRFID
{
    #region Contexts
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    public partial class RFIDEntities : ObjectContext
    {
        #region Constructors
    
        /// <summary>
        /// Initializes a new RFIDEntities object using the connection string found in the 'RFIDEntities' section of the application configuration file.
        /// </summary>
        public RFIDEntities() : base("name=RFIDEntities", "RFIDEntities")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        /// <summary>
        /// Initialize a new RFIDEntities object.
        /// </summary>
        public RFIDEntities(string connectionString) : base(connectionString, "RFIDEntities")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        /// <summary>
        /// Initialize a new RFIDEntities object.
        /// </summary>
        public RFIDEntities(EntityConnection connection) : base(connection, "RFIDEntities")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        #endregion
    
        #region Partial Methods
    
        partial void OnContextCreated();
    
        #endregion
    
        #region ObjectSet Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public ObjectSet<Attendance> Attendances
        {
            get
            {
                if ((_Attendances == null))
                {
                    _Attendances = base.CreateObjectSet<Attendance>("Attendances");
                }
                return _Attendances;
            }
        }
        private ObjectSet<Attendance> _Attendances;
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public ObjectSet<AttendanceSheet> AttendanceSheets
        {
            get
            {
                if ((_AttendanceSheets == null))
                {
                    _AttendanceSheets = base.CreateObjectSet<AttendanceSheet>("AttendanceSheets");
                }
                return _AttendanceSheets;
            }
        }
        private ObjectSet<AttendanceSheet> _AttendanceSheets;
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public ObjectSet<Course> Courses
        {
            get
            {
                if ((_Courses == null))
                {
                    _Courses = base.CreateObjectSet<Course>("Courses");
                }
                return _Courses;
            }
        }
        private ObjectSet<Course> _Courses;
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public ObjectSet<Student> Students
        {
            get
            {
                if ((_Students == null))
                {
                    _Students = base.CreateObjectSet<Student>("Students");
                }
                return _Students;
            }
        }
        private ObjectSet<Student> _Students;

        #endregion

        #region AddTo Methods
    
        /// <summary>
        /// Deprecated Method for adding a new object to the Attendances EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        public void AddToAttendances(Attendance attendance)
        {
            base.AddObject("Attendances", attendance);
        }
    
        /// <summary>
        /// Deprecated Method for adding a new object to the AttendanceSheets EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        public void AddToAttendanceSheets(AttendanceSheet attendanceSheet)
        {
            base.AddObject("AttendanceSheets", attendanceSheet);
        }
    
        /// <summary>
        /// Deprecated Method for adding a new object to the Courses EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        public void AddToCourses(Course course)
        {
            base.AddObject("Courses", course);
        }
    
        /// <summary>
        /// Deprecated Method for adding a new object to the Students EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        public void AddToStudents(Student student)
        {
            base.AddObject("Students", student);
        }

        #endregion

    }

    #endregion

    #region Entities
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName="RFIDModel", Name="Attendance")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    public partial class Attendance : EntityObject
    {
        #region Factory Method
    
        /// <summary>
        /// Create a new Attendance object.
        /// </summary>
        /// <param name="id">Initial value of the ID property.</param>
        public static Attendance CreateAttendance(global::System.Int32 id)
        {
            Attendance attendance = new Attendance();
            attendance.ID = id;
            return attendance;
        }

        #endregion

        #region Primitive Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 ID
        {
            get
            {
                return _ID;
            }
            set
            {
                if (_ID != value)
                {
                    OnIDChanging(value);
                    ReportPropertyChanging("ID");
                    _ID = StructuralObject.SetValidValue(value);
                    ReportPropertyChanged("ID");
                    OnIDChanged();
                }
            }
        }
        private global::System.Int32 _ID;
        partial void OnIDChanging(global::System.Int32 value);
        partial void OnIDChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public Nullable<global::System.Int32> CourseID
        {
            get
            {
                return _CourseID;
            }
            set
            {
                OnCourseIDChanging(value);
                ReportPropertyChanging("CourseID");
                _CourseID = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("CourseID");
                OnCourseIDChanged();
            }
        }
        private Nullable<global::System.Int32> _CourseID;
        partial void OnCourseIDChanging(Nullable<global::System.Int32> value);
        partial void OnCourseIDChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String Title
        {
            get
            {
                return _Title;
            }
            set
            {
                OnTitleChanging(value);
                ReportPropertyChanging("Title");
                _Title = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("Title");
                OnTitleChanged();
            }
        }
        private global::System.String _Title;
        partial void OnTitleChanging(global::System.String value);
        partial void OnTitleChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String SubTitle
        {
            get
            {
                return _SubTitle;
            }
            set
            {
                OnSubTitleChanging(value);
                ReportPropertyChanging("SubTitle");
                _SubTitle = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("SubTitle");
                OnSubTitleChanged();
            }
        }
        private global::System.String _SubTitle;
        partial void OnSubTitleChanging(global::System.String value);
        partial void OnSubTitleChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String StartTime
        {
            get
            {
                return _StartTime;
            }
            set
            {
                OnStartTimeChanging(value);
                ReportPropertyChanging("StartTime");
                _StartTime = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("StartTime");
                OnStartTimeChanged();
            }
        }
        private global::System.String _StartTime;
        partial void OnStartTimeChanging(global::System.String value);
        partial void OnStartTimeChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String EndTime
        {
            get
            {
                return _EndTime;
            }
            set
            {
                OnEndTimeChanging(value);
                ReportPropertyChanging("EndTime");
                _EndTime = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("EndTime");
                OnEndTimeChanged();
            }
        }
        private global::System.String _EndTime;
        partial void OnEndTimeChanging(global::System.String value);
        partial void OnEndTimeChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String CreatedDate
        {
            get
            {
                return _CreatedDate;
            }
            set
            {
                OnCreatedDateChanging(value);
                ReportPropertyChanging("CreatedDate");
                _CreatedDate = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("CreatedDate");
                OnCreatedDateChanged();
            }
        }
        private global::System.String _CreatedDate;
        partial void OnCreatedDateChanging(global::System.String value);
        partial void OnCreatedDateChanged();

        #endregion

    
    }
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName="RFIDModel", Name="AttendanceSheet")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    public partial class AttendanceSheet : EntityObject
    {
        #region Factory Method
    
        /// <summary>
        /// Create a new AttendanceSheet object.
        /// </summary>
        /// <param name="id">Initial value of the ID property.</param>
        public static AttendanceSheet CreateAttendanceSheet(global::System.Int32 id)
        {
            AttendanceSheet attendanceSheet = new AttendanceSheet();
            attendanceSheet.ID = id;
            return attendanceSheet;
        }

        #endregion

        #region Primitive Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 ID
        {
            get
            {
                return _ID;
            }
            set
            {
                if (_ID != value)
                {
                    OnIDChanging(value);
                    ReportPropertyChanging("ID");
                    _ID = StructuralObject.SetValidValue(value);
                    ReportPropertyChanged("ID");
                    OnIDChanged();
                }
            }
        }
        private global::System.Int32 _ID;
        partial void OnIDChanging(global::System.Int32 value);
        partial void OnIDChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public Nullable<global::System.Int32> AttendanceID
        {
            get
            {
                return _AttendanceID;
            }
            set
            {
                OnAttendanceIDChanging(value);
                ReportPropertyChanging("AttendanceID");
                _AttendanceID = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("AttendanceID");
                OnAttendanceIDChanged();
            }
        }
        private Nullable<global::System.Int32> _AttendanceID;
        partial void OnAttendanceIDChanging(Nullable<global::System.Int32> value);
        partial void OnAttendanceIDChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String MatricNo
        {
            get
            {
                return _MatricNo;
            }
            set
            {
                OnMatricNoChanging(value);
                ReportPropertyChanging("MatricNo");
                _MatricNo = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("MatricNo");
                OnMatricNoChanged();
            }
        }
        private global::System.String _MatricNo;
        partial void OnMatricNoChanging(global::System.String value);
        partial void OnMatricNoChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String ArrivalTime
        {
            get
            {
                return _ArrivalTime;
            }
            set
            {
                OnArrivalTimeChanging(value);
                ReportPropertyChanging("ArrivalTime");
                _ArrivalTime = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("ArrivalTime");
                OnArrivalTimeChanged();
            }
        }
        private global::System.String _ArrivalTime;
        partial void OnArrivalTimeChanging(global::System.String value);
        partial void OnArrivalTimeChanged();

        #endregion

    
    }
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName="RFIDModel", Name="Course")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    public partial class Course : EntityObject
    {
        #region Factory Method
    
        /// <summary>
        /// Create a new Course object.
        /// </summary>
        /// <param name="id">Initial value of the ID property.</param>
        public static Course CreateCourse(global::System.Int32 id)
        {
            Course course = new Course();
            course.ID = id;
            return course;
        }

        #endregion

        #region Primitive Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 ID
        {
            get
            {
                return _ID;
            }
            set
            {
                if (_ID != value)
                {
                    OnIDChanging(value);
                    ReportPropertyChanging("ID");
                    _ID = StructuralObject.SetValidValue(value);
                    ReportPropertyChanged("ID");
                    OnIDChanged();
                }
            }
        }
        private global::System.Int32 _ID;
        partial void OnIDChanging(global::System.Int32 value);
        partial void OnIDChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String Title
        {
            get
            {
                return _Title;
            }
            set
            {
                OnTitleChanging(value);
                ReportPropertyChanging("Title");
                _Title = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("Title");
                OnTitleChanged();
            }
        }
        private global::System.String _Title;
        partial void OnTitleChanging(global::System.String value);
        partial void OnTitleChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String Code
        {
            get
            {
                return _Code;
            }
            set
            {
                OnCodeChanging(value);
                ReportPropertyChanging("Code");
                _Code = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("Code");
                OnCodeChanged();
            }
        }
        private global::System.String _Code;
        partial void OnCodeChanging(global::System.String value);
        partial void OnCodeChanged();

        #endregion

    
    }
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName="RFIDModel", Name="Student")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    public partial class Student : EntityObject
    {
        #region Factory Method
    
        /// <summary>
        /// Create a new Student object.
        /// </summary>
        /// <param name="id">Initial value of the ID property.</param>
        public static Student CreateStudent(global::System.Int32 id)
        {
            Student student = new Student();
            student.ID = id;
            return student;
        }

        #endregion

        #region Primitive Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 ID
        {
            get
            {
                return _ID;
            }
            set
            {
                if (_ID != value)
                {
                    OnIDChanging(value);
                    ReportPropertyChanging("ID");
                    _ID = StructuralObject.SetValidValue(value);
                    ReportPropertyChanged("ID");
                    OnIDChanged();
                }
            }
        }
        private global::System.Int32 _ID;
        partial void OnIDChanging(global::System.Int32 value);
        partial void OnIDChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String Name
        {
            get
            {
                return _Name;
            }
            set
            {
                OnNameChanging(value);
                ReportPropertyChanging("Name");
                _Name = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("Name");
                OnNameChanged();
            }
        }
        private global::System.String _Name;
        partial void OnNameChanging(global::System.String value);
        partial void OnNameChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String MatricNo
        {
            get
            {
                return _MatricNo;
            }
            set
            {
                OnMatricNoChanging(value);
                ReportPropertyChanging("MatricNo");
                _MatricNo = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("MatricNo");
                OnMatricNoChanged();
            }
        }
        private global::System.String _MatricNo;
        partial void OnMatricNoChanging(global::System.String value);
        partial void OnMatricNoChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String TagID
        {
            get
            {
                return _TagID;
            }
            set
            {
                OnTagIDChanging(value);
                ReportPropertyChanging("TagID");
                _TagID = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("TagID");
                OnTagIDChanged();
            }
        }
        private global::System.String _TagID;
        partial void OnTagIDChanging(global::System.String value);
        partial void OnTagIDChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String Department
        {
            get
            {
                return _Department;
            }
            set
            {
                OnDepartmentChanging(value);
                ReportPropertyChanging("Department");
                _Department = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("Department");
                OnDepartmentChanged();
            }
        }
        private global::System.String _Department;
        partial void OnDepartmentChanging(global::System.String value);
        partial void OnDepartmentChanged();

        #endregion

    
    }

    #endregion

    
}