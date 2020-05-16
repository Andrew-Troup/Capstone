namespace CoreApplication.Models
{
    using CoreApplication.Models.Database;
    using System;
    using System.Collections.Generic;
    using System.Text;

    class MainWindowManager
    {
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
        public User CurrentUser { get; set; }

        /// <summary>
        /// Tells the us if the user type is a student
        /// </summary>
        public bool IsAdmin { get => (CurrentUser.UserType == UserTypes.Admin); }

        public MainWindowManager()
        {
            CurrentUser = new User("Andrew-Troup", "andrew");
            CurrentUser.UserType = UserTypes.Admin;
            ViewStorage = new Dictionary<ActiveControls, object>();
        }

    }
}
