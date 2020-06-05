namespace CoreApplication.ModelHandlers
{
    using Common.Models;
    using Common.Models.Login;
    using CoreApplication.ModelHandlers;
    using CoreApplication.ModelHandlers.Records;
    using CoreApplication.ModelHandlers.Records.Base;
    using CoreApplication.Models.Base;
    using MongoDB.Bson;
    using MongoDB.Driver;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    class MainWindowManager
    {
        #region Properties

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
        /// Stores the class record handler
        /// </summary>
        public ClassRecordHandler ClassRecords { get; set; }

        /// <summary>
        /// Tells the us if the user type is a student
        /// </summary>
        public bool IsAdmin { get => (CurrentUser.UserType == UserTypes.Admin); }

        #endregion

        public MainWindowManager(BsonDocument user)
        {
            CurrentUser = new UserLoginInformation(user[1].AsString, user[2].AsString, user[3].AsString, user[4].AsString);
            ClassRecords = new ClassRecordHandler(MainHandlers.DatabaseHandler.Database.GetCollection(Common.Models.Collections.Departments).Find(new BsonDocument()).ToEnumerable().ToList());

            if (!IsAdmin)
                ViewHandler = new StudentRecordHandler(MainHandlers.DatabaseHandler.GetStudent(CurrentUser.UserID));
            else
                ViewHandler = new AdminRecordHandler(MainHandlers.DatabaseHandler.GetAdmin(CurrentUser.UserID));
        }
    }
}
