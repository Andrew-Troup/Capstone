namespace CoreApplication.Models.Records.Class
{
    using CoreApplication.Models.Base;
    using CoreApplication.Models.Records.Class.Base;
    using MongoDB.Bson;
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

        public OnlineClassRecordInformation(BsonDocument document) : base(document[2].AsString, document[3].AsString, document[4].AsBsonDocument)
        {
            _classUrl = document[1].AsString;
        }
    }
}
