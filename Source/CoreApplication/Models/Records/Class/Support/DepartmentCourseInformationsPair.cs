namespace CoreApplication.Models.Records.Class.Support
{
    using CoreApplication.Models.Records.Admin.Support;
    using CoreApplication.Models.Records.Class.Base;
    using MongoDB.Bson;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;
    using static CoreApplication.ModelHandlers.Records.ClassRecordHandler;

    class DepartmentCourseInformationsPair
    {
        private object _lock;

        #region Properties

        /// <summary>
        /// The full department name of the class regions
        /// </summary>
        public string DepartmentName { get; set; }

        /// <summary>
        /// The abbreviation used in the class record
        /// </summary>
        public string DepartmentAcronym { get; set; }

        /// <summary>
        /// The classes associated to this department
        /// </summary>
        public ObservableCollection<CourseRecordInformation> ClassRecords { get; set; }

        #endregion

        #region Methods

        #region Public

        public DepartmentCourseInformationsPair()
        {            
            ClassRecords = new ObservableCollection<CourseRecordInformation>();
            AsyncProtection();
        }

        public DepartmentCourseInformationsPair(BsonDocument doc)
        {
            DepartmentName = doc[1].AsString;
            DepartmentAcronym = doc[2].AsString;
            ClassRecords = new ObservableCollection<CourseRecordInformation>();
        }

        public void LoadClasses(List<BsonDocument> documents, SortByTypes type)
        {
            foreach (BsonDocument document in documents)
                BaseLoadClass(document, FilterByTypes.All);

            ClassRecords = SortClasses(type);
        }

        public void LoadClass(BsonDocument document, FilterByTypes type)
        {
            BaseLoadClass(document, type);
        }

        public void LoadClassAsync(BsonDocument document, FilterByTypes type)
        {
            if (_lock == null)
                AsyncProtection();
           
            lock (_lock)
            {
                BaseLoadClass(document, type);                
            }
        }

        public ObservableCollection<CourseRecordInformation> SortClasses(SortByTypes type)
        {
            ObservableCollection<CourseRecordInformation> temp = null;
            if (type == SortByTypes.Ascending)
                temp = new ObservableCollection<CourseRecordInformation>(ClassRecords.OrderBy(x => x.CourseName).ToList());
            else
                temp = new ObservableCollection<CourseRecordInformation>(ClassRecords.OrderByDescending(x => x.CourseName).ToList());

            return temp;
        }

        #endregion

        #region Private

        private void AsyncProtection()
        {
            _lock = new object();
            System.Windows.Data.BindingOperations.EnableCollectionSynchronization(ClassRecords, _lock);
        }

        private void BaseLoadClass(BsonDocument document, FilterByTypes type)
        {
            BsonValue classURl;
            if (document.TryGetValue("ClassUrl", out classURl))
            {
                if (type != FilterByTypes.Face_To_Face)
                    ClassRecords.Add(new OnlineClassRecordInformation(document));
            }
            else
            {
                if (type != FilterByTypes.Online)
                    ClassRecords.Add(new FaceToFaceClassRecordInformation(document));
            }
        }

        #endregion

        #endregion
    }
}
