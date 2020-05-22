namespace CoreApplication.Models.Records.Class
{
    using CoreApplication.Models.Base;
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


        #endregion

        public CourseRecordInformation(string courseID, string courseName, string creditHours, string description)
        {
            _courseID = courseID;
            _courseName = courseName;
            _creditHours = creditHours;
            _description = description;
        }

        public CourseRecordInformation()
        {

        }
    }
}
