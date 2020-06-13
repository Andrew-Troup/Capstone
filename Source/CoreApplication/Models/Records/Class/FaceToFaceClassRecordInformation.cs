namespace CoreApplication.Models.Records.Class
{
    using CoreApplication.Models.Base;
    using CoreApplication.Models.Records.Class.Base;
    using MongoDB.Bson;
    using System;
    using System.Collections.Generic;
    using System.Text;

    class FaceToFaceClassRecordInformation : CourseRecordInformation
    {
        #region Variables

        private string _classRoom;

        private string _classBuilding;

        #endregion

        #region Properties

        /// <summary>
        /// The class room of the face to face class.
        /// </summary>
        public string ClassRoom
        {
            get => _classRoom;
            set
            {
                _classRoom = value;
                RaiseUpdateDatabase(_classRoom);
            }
        }

        /// <summary>
        /// The class building of the face to face class
        /// </summary>
        public string ClassBuilding
        {
            get => _classBuilding;
            set
            {
                _classBuilding = value;
                RaiseUpdateDatabase(_classBuilding);
            }
        }

        #endregion

        public FaceToFaceClassRecordInformation(BsonDocument document) : base(document[3].AsString, document[4].AsString, document[5].AsBsonDocument)
        {
            _classBuilding = document[1].AsString;
            _classRoom = document[2].AsString;
        }

        public FaceToFaceClassRecordInformation() : base()
        {

        }
    }
}
