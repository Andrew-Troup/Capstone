using System;
using System.Collections.Generic;
using System.Text;

namespace CoreApplication.Models
{
    class MainWindowManager
    {
        #region Enumerations

        public enum ActiveControls 
        {
            StudentRecord,
            ClassRecord,
            CourseRecord
        }


        #endregion

        /// <summary>
        /// Where I will be storing the control to prevent it from being lost
        /// </summary>
        public Dictionary<ActiveControls, object> ViewStorage;

        /// <summary>
        /// The control as to which we are looking at currently
        /// </summary>
        public ActiveControls ActiveControl { get; set; }

        public MainWindowManager()
        {
            ViewStorage = new Dictionary<ActiveControls, object>();
        }

    }
}
