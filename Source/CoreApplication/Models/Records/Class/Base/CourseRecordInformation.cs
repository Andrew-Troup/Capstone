namespace CoreApplication.Models.Records.Class.Base
{
    using CoreApplication.Models.Base;
    using MongoDB.Bson;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Windows;

    class CourseRecordInformation : UpdateDatabase
    {
        #region Variables

        private string _courseID;

        private string _courseName;

        private string _creditHours;

        private string _department;

        private string _description;

        private string _startDate;

        private string _endDate;

        private string _classNumber;

        #endregion

        #region Properties        
          
        public string ClassNumber
        {
            get => _classNumber;
            set
            {
                _classNumber = value;
                RaiseUpdateDatabase($"ClassNumber:{_classNumber}");
            }
        }

        /// <summary>
        /// Course Id for the associated class.
        /// </summary>
        public string CourseID 
        { 
            get => _courseID;
            set 
            {
                _courseID = value;
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
                RaiseUpdateDatabase($"Name:{_courseName}");
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
                RaiseUpdateDatabase($"CreditHours:{_creditHours}");
            }
        }

        /// <summary>
        /// Department of the class
        /// </summary>
        public string Department
        {
            get => _department;
            set
            {
                _department = value;
                RaiseUpdateDatabase($"Department:{_department}");
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
                RaiseUpdateDatabase($"Description:{_description}");
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

        #endregion

        public CourseRecordInformation(string beginDate, string endDate, BsonDocument document)
        {
            _startDate = beginDate;
            _endDate = endDate;
            _courseName = document[0].AsString;
            _courseID = document[1].AsString;
            _creditHours = document[2].AsString;
            _description = document[3].AsString;
            _department = document[4].AsString;
            _classNumber = document[5].AsString;
        }

        public CourseRecordInformation()
        {

        }
    }
}
