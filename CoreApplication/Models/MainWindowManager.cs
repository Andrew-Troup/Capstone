namespace CoreApplication.Models
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
    using System.Text;

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
            // TODO this is trash
            CurrentUser = new UserLoginInformation("Andrew-Troup", "andrew");
            CurrentUser.UserType = UserTypes.Student;
            ViewStorage = new Dictionary<ActiveControls, object>();


            if (!IsAdmin)
            {
                // CurrentUser.UserID
                ViewHandler = new StudentRecordHandler(MainHandlers.DatabaseHandler.GetStudent("725571"));
            }
            else
                // CurrentUser.UserID
                ViewHandler = new AdminRecordHandler(MainHandlers.DatabaseHandler.GetAdmin("65986"));
        }
    }
}
