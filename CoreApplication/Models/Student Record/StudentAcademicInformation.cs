namespace CoreApplication.Models.Student_Record
{
    using CoreApplication.Models.Base;
    using MongoDB.Bson;
    using System;
    using System.Collections.Generic;
    using System.Text;

    class StudentAcademicInformation : UpdateDatabase
    {
        #region Variables

        private string _gpa;

        private string _totalUnits;

        private string _major;

        private List<ClassTakenInformation> _takenClasses;

        #endregion

        #region Properties

        /// <summary>
        /// Student's GPA
        /// </summary>
        public string GPA 
        { 
            get => _gpa;
            set 
            {
                _gpa = value;
                RaiseUpdateDatabase($"GPA:{_gpa}");
            }
        }

        /// <summary>
        /// Total units the student has taken
        /// </summary>
        public string TotalUnits 
        {
            get => _totalUnits;
            set
            {
                _totalUnits = value;
                RaiseUpdateDatabase($"TotalUnits:{_totalUnits}");
            }
        }

        /// <summary>
        /// Student's major
        /// </summary>
        public string Major 
        {
            get => _major;
            set
            {
                _major = value;
                RaiseUpdateDatabase($"Major:{_major}");
            }
        }

        /// <summary>
        /// A list of the classes taken by the student
        /// </summary>
        public List<ClassTakenInformation> TakenClasses 
        { 
            get => _takenClasses; 
            private set
            {
                _takenClasses = value;
            }                
        }

        #endregion

        public StudentAcademicInformation(string gpa, string totalUnits, string major, string classesTaken, string grades)
        {
            _takenClasses = new List<ClassTakenInformation>();
            GPA = gpa;
            TotalUnits = totalUnits;
            Major = major;
            PopulateClassesTaken(classesTaken, grades);
        }

        public StudentAcademicInformation()
        {

        }

        private void PopulateClassesTaken(string classesTaken, string grades)
        {
            string[] ct = classesTaken.Split(',');
            string[] grds = grades.Split(',');

            for(int i = 0; i < ct.Length; i++)
            {
                string[] items = ct[i].Split('-');
                TakenClasses.Add(new ClassTakenInformation(items[0].Trim(), items[1].Trim(), grds[i]));
            }
                
        }
    }
}
