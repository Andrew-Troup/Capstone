using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreApplication.Models.Records.Admin.Support
{
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

        /// <summary>
        /// Class name being taught
        /// </summary>
        public string ClassName { get; private set; }

        /// <summary>
        /// List of students in the class.
        /// </summary>
        public List<string> StudentIDs { get; private set; }

        /// <summary>
        /// Class ID being taught.
        /// </summary>
        private string ClassID { get; set; }

        #endregion

        public ClassesBeingTaught(BsonDocument document)
        {
            StudentIDs = new List<string>();
            ClassName = document[(int) Positions.ClassName].AsString;
            ClassID = document[(int) Positions.ClassID].AsString;
            foreach (BsonDocument doc in document[(int)Positions.StudentIDs].AsBsonArray)
                StudentIDs.Add(doc[0].AsString);
        }
    }
}
