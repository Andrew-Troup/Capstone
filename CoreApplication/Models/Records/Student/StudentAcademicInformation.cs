namespace CoreApplication.Models.Records.Student
{
    using CoreApplication.Models.Base;
    using CoreApplication.Models.Records.Student.Support;
    using MongoDB.Bson;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Text;

    class StudentAcademicInformation : UpdateDatabase
    {
        #region Variables

        private string _gpa;

        private string _totalUnits;

        private string _major;

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
        

        public ObservableCollection<TypeClassesPair> AllClasses { get; private set; }

        #endregion

        public StudentAcademicInformation(string gpa, string totalUnits, string major, BsonArray classesTaken, BsonArray grades)
        {
            AllClasses = new ObservableCollection<TypeClassesPair>();
            GPA = gpa;
            TotalUnits = totalUnits;
            Major = major;
            PopulateClassesTaken(classesTaken, grades);
        }

        public StudentAcademicInformation()
        {
            AllClasses = new ObservableCollection<TypeClassesPair>();
        }

        private void PopulateClassesTaken(BsonArray classesTaken, BsonArray currentClasses)
        {
            ObservableCollection<ClassTakenInformation> ClassTaken = new ObservableCollection<ClassTakenInformation>();
            foreach(BsonDocument taken in classesTaken)
                ClassTaken.Add(new ClassTakenInformation(taken[0].AsString, taken[1].AsString));


            ObservableCollection<ClassTakenInformation> currentClass = new ObservableCollection<ClassTakenInformation>();
            foreach(BsonDocument current in currentClasses)
            {
                if(current.ElementCount == 1)
                    currentClass.Add(new ClassTakenInformation(current[0].AsString, string.Empty));
                else
                    currentClass.Add(new ClassTakenInformation(current[0].AsString, current[1].AsString));
            }

            AllClasses.Add(new TypeClassesPair("Completed Classes", ClassTaken));
            AllClasses.Add(new TypeClassesPair("Current Classes", currentClass));
            AllClasses.Add(new TypeClassesPair("Registered Classes", new ObservableCollection<ClassTakenInformation>()));
        }
    }
}
