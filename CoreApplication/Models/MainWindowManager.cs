namespace CoreApplication.Models
{
    using CoreApplication.ModelHandlers;
    using CoreApplication.ModelHandlers.Records;
    using CoreApplication.ModelHandlers.Records.Base;
    using CoreApplication.Models.Base;
    using CoreApplication.Models.Database;
    using MongoDB.Bson;
    using MongoDB.Driver;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using static CoreApplication.ModelHandlers.Database.DatabaseCommunications;

    class MainWindowManager
    {
        #region Properties

        /// <summary>
        /// Where I will be storing the control to prevent it from being lost
        /// </summary>
        public Dictionary<ActiveControls, object> ViewStorage;

        /// <summary>
        /// The control as to which we are looking at currently
        /// </summary>
        public ActiveControls ActiveControl { get; set; }

        /// <summary>
        /// The user that is logged into the system
        /// </summary>
        public UserLoginInformation CurrentUser { get; set; }

        /// <summary>
        /// Information gathered about the user.
        /// </summary>
        public BaseViewHandler ViewHandler { get; set; }

        /// <summary>
        /// Tells the us if the user type is a student
        /// </summary>
        public bool IsAdmin { get => (CurrentUser.UserType == UserTypes.Admin); }

        #endregion

        public MainWindowManager()
        {
            //CurrentUser = new UserLoginInformation("Andrew-Troup", "andrew");
            //CurrentUser.UserType = UserTypes.Admin;
            ViewStorage = new Dictionary<ActiveControls, object>();

           // var filter = Builders<BsonDocument>.Filter.Eq("StudentID", "1236433");
            //ViewHandler = new StudentRecordViewHandler(MainHandlers.Database.GetCollection(Collections.Students).Find(filter).FirstOrDefault());
        }
    }
}
