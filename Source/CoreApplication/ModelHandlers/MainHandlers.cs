namespace CoreApplication.ModelHandlers
{
    using CoreApplication.ModelHandlers.Records.Base;
    using CoreApplication.Models;
    using Database.Handlers;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public static class MainHandlers
    {

        #region Properties

        internal static DatabaseHandler DatabaseHandler { get; set; }

        internal static MainWindowManager WindowManager {get; set;}

        #endregion

        static MainHandlers()
        {
            DatabaseHandler = new DatabaseHandler();
        }
    }
}
