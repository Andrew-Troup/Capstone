namespace CoreApplication.Models.Records.Class
{
    using CoreApplication.Models.Base;
    using System;
    using System.Collections.Generic;
    using System.Security.RightsManagement;
    using System.Text;

    class OnlineClassRecordInformation : CourseRecordInformation
    {
        #region Variables

        private string _classUrl;

        #endregion

        #region Properties

        public string ClassUrl
        {
            get => _classUrl;
            set
            {
                _classUrl = value;
                RaiseUpdateDatabase(_classUrl);
            }
        }

        #endregion

        public OnlineClassRecordInformation(string classUrl, string courseID, string courseName, string creditHours, string description) : base(courseID, courseName, creditHours, description)
        {
            _classUrl = classUrl;
        }
    }
}
