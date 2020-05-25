namespace CoreApplication.Models.Records.Class
{
    using CoreApplication.Models.Base;
    using MongoDB.Bson;
    using System;
    using System.Collections.Generic;
    using System.Text;

    class CourseRecordInformation : UpdateDatabase
    {
        #region Variables

        private string _courseID;

        private string _courseName;

        private string _creditHours;

        private string _description;

        private string _startDate;

        private string _endDate;

        #endregion

        #region Properties

        /// <summary>
        /// Course Id for the associated class.
        /// </summary>
        public string CourseID 
        { 
            get => _courseID;
            set 
            {
                _courseID = value;
                RaiseUpdateDatabase(_courseID);
            }
        }

        /// <summary>
        /// Course name
        /// </summary>
        public string CourseName
        {
            get => _courseName;
            set
            {
                _courseName = value;
                RaiseUpdateDatabase(_courseName);
            }
        }

        /// <summary>
        /// Credit hours
        /// </summary>
        public string CreditHours
        {
            get => _creditHours;
            set
            {
                _creditHours = value;
                RaiseUpdateDatabase(_creditHours);
            }
        }

        /// <summary>
        /// Description of the class.
        /// </summary>
        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                RaiseUpdateDatabase(_description);
            }
        }

        /// <summary>
        /// /The start date for the class
        /// </summary>
        public string StartDate
        {
            get => _startDate;
            set
            {
                _startDate = value;
                RaiseUpdateDatabase($"BeginDate:{_startDate}");
            }
        }

        /// <summary>
        /// End date for the course
        /// </summary>
        public string EndDate
        {
            get => _endDate;
            set
            {
                _endDate = value;
                RaiseUpdateDatabase($"EndDate:{_endDate}");
            }
        }

        #endregion

        public CourseRecordInformation(string beginDate, string endDate, BsonDocument document)
        {
            _startDate = beginDate;
            _endDate = endDate;
            _courseName = document[0].AsString;
            _courseID = document[1].AsString;
            _creditHours = document[2].AsString;
            _description = document[3].AsString;
        }

        public CourseRecordInformation()
        {

        }
    }
}
