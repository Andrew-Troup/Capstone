namespace CoreApplication.Models.Records.Student
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

        private List<ClassTakenInformation> _currentClasses;

        #endregion

        #region Properties

        public List<ClassTakenInformation> CurrentClasses
        {
            get => _currentClasses;
            set
            {
                _currentClasses = value;                
            }
        }

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

        public StudentAcademicInformation(string gpa, string totalUnits, string major, BsonArray classesTaken, BsonArray grades)
        {
            _takenClasses = new List<ClassTakenInformation>();
            _currentClasses = new List<ClassTakenInformation>();
            GPA = gpa;
            TotalUnits = totalUnits;
            Major = major;
            PopulateClassesTaken(classesTaken, grades);
        }

        public StudentAcademicInformation()
        {

        }

        private void PopulateClassesTaken(BsonArray classesTaken, BsonArray currentClasses)
        {
            foreach(BsonDocument taken in classesTaken)                      
                _takenClasses.Add(new ClassTakenInformation(taken[0].AsString, taken[1].AsString));            
                

           foreach(BsonDocument current in currentClasses)
           {
                if(current.ElementCount == 1)
                    _currentClasses.Add(new ClassTakenInformation(current[0].AsString, string.Empty));
                else
                    _currentClasses.Add(new ClassTakenInformation(current[0].AsString, current[1].AsString));
            }
                
        }
    }
}
