namespace CoreApplication.ModelHandlers.Records
{
    using CoreApplication.ModelHandlers.Records.Base;
    using CoreApplication.Models.Records.Class;
    using CoreApplication.Models.Records.Class.Base;
    using CoreApplication.Models.Records.Class.Support;
    using MongoDB.Bson;
    using MongoDB.Driver;
    using System;
    using System.Collections.Generic;
    using System.Collections.Immutable;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;
    using System.Windows.Data;

    class ClassRecordHandler
    {
        #region Enumerations

        public enum SortByTypes
        {
            Ascending,
            Descending
        }

        public enum FilterByTypes
        {
            All,
            Online,
            Face_To_Face
        }


        #endregion

        public event EventHandler UpdateUI;

        public event EventHandler UpdateCourseTreeViewUI;

        #region Variables

        private CourseRecordInformation _selctedClass;

        private object _lock;

        #endregion

        #region Properties

        /// <summary>
        /// Contains all items being displayed
        /// </summary>
        public ObservableCollection<DepartmentCourseInformationsPair> Classes { get; private set; }

        /// <summary>
        /// Current filter being applied to the List.
        /// </summary>
        public FilterByTypes CurrentFilterType { get; set; }

        /// <summary>
        /// Currently selected sort by type.
        /// </summary>
        public SortByTypes CurrentSortByType { get; set; }

        /// <summary>
        /// Classes found during the search.
        /// </summary>
        public ObservableCollection<DepartmentCourseInformationsPair> SearchedClasses { get; set; }

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
            SearchedClasses = new ObservableCollection<DepartmentCourseInformationsPair>();
            _lock = new object();
            BindingOperations.EnableCollectionSynchronization(SearchedClasses, _lock);

            CurrentSortByType = SortByTypes.Ascending;
            CurrentFilterType = FilterByTypes.All;
            foreach (BsonDocument department in departments)
                Classes.Add(new DepartmentCourseInformationsPair(department));
        }

        #region Methods

        #region Public

        /// <summary>
        /// Public interface to the sortby functionality
        /// </summary>
        /// <param name="type"></param>
        public void FilterBy(string type)
        {
            if (type.Contains("-"))
                type = type.Replace('-', '_');

            FilterByTypes temp = (FilterByTypes)Enum.Parse(typeof(FilterByTypes), type);
            if (CurrentFilterType != temp)
            {
                CurrentFilterType = temp;
                if (SearchedClasses.Count == 0)
                {
                    FilterBy(Classes);
                    SortBy(Classes);
                }
                else
                {
                    FilterBy(SearchedClasses);
                    SortBy(Classes);
                }

                
                UpdateCourseTreeViewUI(this, null);
            }
        }

        /// <summary>
        /// Loads the selected item's class information into local storage.
        /// </summary>
        /// <param name="acronym"></param>
        public void LoadClassInformation(string acronym)
        {
            FilterDefinition<BsonDocument> filter = Builders<BsonDocument>.Filter.Eq("CourseInformation.Department", acronym);
            DepartmentCourseInformationsPair current = FindFromAcronym(acronym);
            MainHandlers.DatabaseHandler.Database.GetCollection(Common.Models.Collections.Classes).Find(filter).ForEachAsync(x=>current.LoadClassAsync(x, CurrentFilterType));
        }

        /// <summary>
        /// Values we are searching database for.
        /// </summary>
        /// <param name="content"></param>
        public void SearchDatabase(string content)
        {
            if (SearchedClasses.Count > 0)
                SearchedClasses.Clear();

            foreach (DepartmentCourseInformationsPair information in Classes)
                if (information.DepartmentName.Contains(content))
                    SearchedClasses.Add(information);

            lock(_lock)
            {
                // Loads the classes into the current searched items.
                foreach (DepartmentCourseInformationsPair load in SearchedClasses)
                {
                    FilterDefinition<BsonDocument> filter = Builders<BsonDocument>.Filter.Eq("CourseInformation.Department", load.DepartmentAcronym);
                    //DepartmentCourseInformationsPair temp = new DepartmentCourseInformationsPair();
                    List<BsonDocument> docs = MainHandlers.DatabaseHandler.Database.GetCollection(Common.Models.Collections.Classes).Find(filter).ToList();
                    foreach (BsonDocument doc in docs)
                        load.LoadClass(doc, CurrentFilterType);
                    //load.ClassRecords = temp.ClassRecords;
                }

                SortBy(SearchedClasses);
            }           
        }

        /// <summary>
        /// Public interface to the sortby functionality
        /// </summary>
        /// <param name="type"></param>
        public void SortBy(string type)
        {
            SortByTypes temp = (SortByTypes)Enum.Parse(typeof(SortByTypes), type);
            if (CurrentSortByType != temp)
            {
                CurrentSortByType = temp;
                if (SearchedClasses.Count == 0)
                    Classes = SortBy(Classes);
                else
                    SearchedClasses = SortBy(SearchedClasses);

                UpdateCourseTreeViewUI(this, null);
            }
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

        /// <summary>
        /// Defines if the user wants to sort the list by ascending or descending order
        /// </summary>
        /// <param name="isAscending"></param>
        private ObservableCollection<DepartmentCourseInformationsPair> SortBy(ObservableCollection<DepartmentCourseInformationsPair> collection)
        {
            // Sorting the outer lists.
            switch (CurrentSortByType)
            {
                case SortByTypes.Descending:
                    collection = new ObservableCollection<DepartmentCourseInformationsPair>(collection.OrderByDescending(x => x.DepartmentName).ToList());
                    break;

                case SortByTypes.Ascending:
                    collection = new ObservableCollection<DepartmentCourseInformationsPair>(collection.OrderBy(x => x.DepartmentName).ToList());
                    break;
            }

            // Sorting in the inner lists.
            for (int i = 0; i < collection.Count; i++)
                if(collection[i].ClassRecords.Count > 0)
                    collection[i].ClassRecords = collection[i].SortClasses(CurrentSortByType);

            return collection;
        }

        /// <summary>
        /// Filters the desired collection by the available filter type
        /// </summary>
        /// <param name="temp"></param>
        /// <param name="collection"></param>
        private ObservableCollection<DepartmentCourseInformationsPair> FilterBy(ObservableCollection<DepartmentCourseInformationsPair> collection)
        {
            FilterDefinition<BsonDocument> filter = null;
            foreach(DepartmentCourseInformationsPair item in collection)
            {
                if (item.ClassRecords.Count > 0)
                {
                    filter = Builders<BsonDocument>.Filter.Eq("CourseInformation.Department", item.DepartmentAcronym);
                    List<BsonDocument> docs = MainHandlers.DatabaseHandler.Database.GetCollection(Common.Models.Collections.Classes).Find(filter).ToList();
                    item.ClassRecords.Clear();
                    foreach (BsonDocument doc in docs)
                        item.LoadClass(doc, CurrentFilterType);
                }
            }

            return collection;            
        }
        
        #endregion

        #endregion
    }
}
