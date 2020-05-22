namespace CoreApplication.Models.Records.Class
{
    using CoreApplication.Models.Base;
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


        public FaceToFaceClassRecordInformation(string classRoom, string classBuilding, string courseID, string courseName, string creditHours, string description) : base(courseID, courseName, creditHours, description)
        {
            _classBuilding = classBuilding;
            _classRoom = classRoom;
        }

        public FaceToFaceClassRecordInformation() : base()
        {

        }
    }
}
