namespace CoreApplication.Models.Records.Class.Support
{
    using CoreApplication.Models.Records.Class.Base;
    using MongoDB.Bson;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Text;

    class DepartmentCourseInformationsPair
    {
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
        public ObservableCollection<CourseRecordInformation> ClassRecords { get; private set; }

        #endregion

        public DepartmentCourseInformationsPair()
        {
            ClassRecords = new ObservableCollection<CourseRecordInformation>();
        }

        public DepartmentCourseInformationsPair(BsonDocument doc)
        {
            DepartmentName = doc[0].AsString;
            DepartmentAcronym = doc[1].AsString;
            ClassRecords = new ObservableCollection<CourseRecordInformation>();
        }

        public void LoadClasses(BsonArray documents)
        {
            foreach (BsonDocument document in documents)
                LoadClass(document);
        }

        public void LoadClass(BsonDocument document)
        {
            BsonValue classURl;
            if (document.TryGetValue("ClassURL", out classURl))
                ClassRecords.Add(new OnlineClassRecordInformation(document));
            else
                ClassRecords.Add(new FaceToFaceClassRecordInformation(document));
        }
    }
}
