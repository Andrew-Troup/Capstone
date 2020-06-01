namespace CoreApplication.Models.Records.Student
{
    using CoreApplication.ModelHandlers;
    using CoreApplication.ModelHandlers.Records;
    using CoreApplication.Models.Base;
    using CoreApplication.Models.Records.Class.Base;
    using CoreApplication.Models.Records.Student.Support;
    using MongoDB.Bson;
    using MongoDB.Driver;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Text;

    class StudentAcademicInformation : UpdateDatabase
    {
        #region Variables         
        private static IReadOnlyDictionary<string, double> GradeConversions = new Dictionary<string, double>() { { "A", 4.0 }, { "A-", 3.67 }, {"B+", 3.33 }, {"B", 3.00 }, 
                                                                                { "C+", 2.67 }, { "C", 2.00 }, { "C-", 1.67 }, { "D+", 1.33}, { "D-", 0.67}, {"F", 0.00 } };

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

        public StudentAcademicInformation(string gpa, string totalUnits, string major, BsonArray classesTaken, BsonArray grades, BsonArray registeredClasses)
        {
            AllClasses = new ObservableCollection<TypeClassesPair>();
            GPA = gpa;
            TotalUnits = totalUnits;
            Major = major;
            PopulateClassesTaken(classesTaken, grades, registeredClasses);
        }

        public StudentAcademicInformation()
        {
            AllClasses = new ObservableCollection<TypeClassesPair>();
        }

        #region Methods

        #region Public 

        /// <summary>
        /// Determins if the student has taken the course in the past.
        /// </summary>
        /// <param name="information"></param>
        /// <returns></returns>
        public bool ContainsClass(string classNumber)
        {
            bool value = false;
            foreach(TypeClassesPair pair in AllClasses)
            {
                foreach (ClassTakenInformation info in pair.Classes)
                {
                    if (info.ClassNumber.Equals(classNumber))
                    {
                        value = true;
                        break;
                    }
                }
            }

            return value;
        }

        /// <summary>
        /// Gets the desired TypeClassPair
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public TypeClassesPair GetTypeClassPair(string type)
        {
            foreach (TypeClassesPair pair in AllClasses)
                if (pair.Name.Equals(type))
                    return pair;

            return null;
        }

        #endregion

        #region Private

        /// <summary>
        /// Updates the database when a change has been marked in the UI.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClassGradeValueChanged(object sender, ValueChangedArgs e)
        {
            string[] parts = e.Text.Split('\t');
            string[] classInfo = parts[0].Split(':');
            string[] gradeInfo = parts[1].Split(':');
            if (!String.IsNullOrEmpty(gradeInfo[1]))
            {
                var classAcademicList = FindClassList(classInfo[1]);
                StudentRecordHandler handler = ((AdminRecordHandler)MainHandlers.WindowManager.ViewHandler).SelectedStudent;
                FilterDefinition<BsonDocument> studentFilter = Builders<BsonDocument>.Filter.Eq("StudentID", handler.PersonalInformation.ID);
                var newValue = Builders<BsonDocument>.Update.Set($"AcademicInformation.{classAcademicList.name}.{classAcademicList.index}.Grade", gradeInfo[1]);
                MainHandlers.DatabaseHandler.Database.GetCollection(Common.Models.Collections.Students).UpdateOne(studentFilter, newValue);
                ReCalculateGPA();
            }
        }

        /// <summary>
        /// Finds the class that the item is present.
        /// </summary>
        /// <param name="fullClassName"></param>
        /// <returns></returns>
        private (string name, int index) FindClassList(string fullClassName)
        {
            
            foreach (TypeClassesPair pair in AllClasses)
            {
                int index = 0;
                foreach (ClassTakenInformation info in pair.Classes)
                {                    
                    if (info.ToString().Equals(fullClassName))
                        return (pair.Name, index);

                    index++;
                }
            }
            return (null, 0);
        }

        /// <summary>
        /// Populates the class information for the student.
        /// </summary>
        /// <param name="classesTaken"></param>
        /// <param name="currentClasses"></param>
        /// <param name="registerdClasses"></param>
        private void PopulateClassesTaken(BsonArray classesTaken, BsonArray currentClasses, BsonArray registerdClasses)
        {
            ObservableCollection<ClassTakenInformation> classTaken = new ObservableCollection<ClassTakenInformation>();
            foreach (BsonDocument taken in classesTaken)
            {
                ClassTakenInformation info = new ClassTakenInformation(taken[0].AsString, taken[1].AsString);
                info.Value_Changed += ClassGradeValueChanged;
                classTaken.Add(info);
            }

            ObservableCollection<ClassTakenInformation> currentClass = new ObservableCollection<ClassTakenInformation>();
            foreach (BsonDocument current in currentClasses)
            {
                ClassTakenInformation info;
                if (current.ElementCount == 1)
                    info = new ClassTakenInformation(current[0].AsString, string.Empty);
                else
                    info = new ClassTakenInformation(current[0].AsString, current[1].AsString);

                info.Value_Changed += ClassGradeValueChanged;
                currentClass.Add(info);
            }

            ObservableCollection<ClassTakenInformation> registeredClass = new ObservableCollection<ClassTakenInformation>();
            foreach (BsonDocument registered in registerdClasses)
            {
                ClassTakenInformation info = new ClassTakenInformation(registered[0].AsString, registered[1].AsString);
                info.Value_Changed += ClassGradeValueChanged;
                registeredClass.Add(info);
            }

            AllClasses.Add(new TypeClassesPair("ClassesTaken", classTaken));
            AllClasses.Add(new TypeClassesPair("Enrolled", currentClass));
            AllClasses.Add(new TypeClassesPair("Registered", registeredClass));
        }

        /// <summary>
        /// Recalculates the GPA after a grade has been changed.
        /// </summary>
        private void ReCalculateGPA()
        {
            int classCount = 0;
            double sum = 0;
            foreach(TypeClassesPair pair in AllClasses)
            {
                foreach(ClassTakenInformation taken in pair.Classes)
                {
                    if (String.IsNullOrEmpty(taken.Grade))
                        sum += 4.0;
                    else
                        sum += GradeConversions[taken.Grade];
                }

                classCount += pair.Classes.Count;
            }

            string gpa = (sum / classCount).ToString("0.##");
            GPA = gpa;
        }

        #endregion

        #endregion
    }
}
