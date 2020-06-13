namespace CoreApplication.Models.Records.Admin.Support
{
    using CoreApplication.ModelHandlers;
    using CoreApplication.ModelHandlers.Records;
    using MongoDB.Bson;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Text;

    class ClassesBeingTaught
    {
        #region Enumerations

        private enum Positions
        {
            ClassName,
            ClassID,
            StudentIDs
        }

        #endregion

        #region Properties

        #region Public

        /// <summary>
        /// Class name being taught
        /// </summary>
        public string ClassName { get; private set; }

        /// <summary>
        /// Class ID being taught.
        /// </summary>
        public string ClassID { get; private set; }        

        /// <summary>
        /// List of students in the class.
        /// </summary>
        public ObservableCollection<StudentRecordHandler> StudentRecords { get; private set; }

        #endregion

        #endregion

        public ClassesBeingTaught(BsonDocument document)
        {
            StudentRecords = new ObservableCollection<StudentRecordHandler>();
            ClassName = document[(int) Positions.ClassName].AsString;
            ClassID = document[(int) Positions.ClassID].AsString;
            foreach (BsonDocument doc in document[(int)Positions.StudentIDs].AsBsonArray)
            {
                try
                {
                    StudentRecords.Add(new StudentRecordHandler(MainHandlers.DatabaseHandler.GetStudent(doc[0].AsString)));
                }
                catch(Exception) {}
            }
        }

        /// <summary>
        /// Loads in a series of classes being taught
        /// </summary>
        /// <param name="docs"></param>
        public ClassesBeingTaught(List<BsonDocument> docs, string className, string classID)
        {
            ClassName = className;
            ClassID = classID;
            StudentRecords = new ObservableCollection<StudentRecordHandler>();
            foreach (BsonDocument doc in docs)
                StudentRecords.Add(new StudentRecordHandler(doc));
        }
    }
}
