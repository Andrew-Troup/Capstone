namespace CoreApplication.ModelHandlers
{
    using CoreApplication.ModelHandlers.Database;
    using CoreApplication.Models;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public static class MainHandlers
    {
        internal static DatabaseCommunications Database { get; set; }

        internal static MainWindowManager WindowManager {get; set;}

        static MainHandlers()
        {
            Database = new DatabaseCommunications();
            WindowManager = new MainWindowManager();
        }
    }
}
