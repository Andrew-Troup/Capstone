namespace CoreApplication.ModelHandlers.Records
{
    using CoreApplication.ModelHandlers.Records.Base;
    using CoreApplication.Models.Records.Class.Base;
    using CoreApplication.Models.Records.Class.Support;
    using MongoDB.Bson;
    using MongoDB.Driver;
    using System;
    using System.Collections.Generic;
    using System.Collections.Immutable;
    using System.Collections.ObjectModel;
    using System.Text;

    class ClassRecordHandler
    {
        public event EventHandler UpdateUI;

        private CourseRecordInformation _selctedClass;

        #region Properties

        /// <summary>
        /// Contains all items being displayed
        /// </summary>
        public ObservableCollection<DepartmentCourseInformationsPair> Classes { get; private set; }

        /// <summary>
        /// Holds the currently selected class.
        /// </summary>
        public CourseRecordInformation SelectedClass 
        {
            get => _selctedClass; 
            set 
            { 
                _selctedClass = value; 
                UpdateUI.Invoke(this, null); 
            } 
        }

        #endregion

        public ClassRecordHandler(List<BsonDocument> departments)
        {
            Classes = new ObservableCollection<DepartmentCourseInformationsPair>();
            foreach (BsonDocument department in departments)
                Classes.Add(new DepartmentCourseInformationsPair(department));
        }

        #region Methods

        #region Public

        /// <summary>
        /// Loads the selected item's class information into local storage.
        /// </summary>
        /// <param name="acronym"></param>
        public void LoadClassInformation(string acronym)
        {
            FilterDefinition<BsonDocument> filter = Builders<BsonDocument>.Filter.Eq("CourseInformation.Department", acronym);
            DepartmentCourseInformationsPair current = FindFromAcronym(acronym);
            MainHandlers.DatabaseHandler.Database.GetCollection(Common.Models.Collections.Classes).Find(filter).ForEachAsync(x=>current.LoadClass(x));
        }

        /// <summary>
        /// Defines if the user wants to sort the list by ascending or descending order
        /// </summary>
        /// <param name="isAscending"></param>
        public void SortByAscending(bool isAscending)
        {
            
        }

        #endregion

        #region Private

        /// <summary>
        /// Finds the current class department pair given the acronym
        /// </summary>
        /// <param name="acronym"></param>
        /// <returns></returns>
        private DepartmentCourseInformationsPair FindFromAcronym(string acronym)
        {
            foreach (DepartmentCourseInformationsPair item in Classes)
                if (item.DepartmentAcronym.Equals(acronym))
                    return item;

            return null;
        }


        #endregion

        #endregion
    }
}
