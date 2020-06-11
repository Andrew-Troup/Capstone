namespace CoreApplication.Models.Query
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    class QueryDatabaseModel
    {
        #region Constants

        private const string _FIRST_ = "{\"";

        private const string _MIDDLE_ = "\": \"";

        private const string _LAST_ = "\"}";

        #endregion

        #region Properties

        /// <summary>
        /// First part of the query output
        /// </summary>
        public string First { get => _FIRST_; }

        /// <summary>
        /// Middle part of the query output
        /// </summary>
        public string Last { get => _LAST_; }

        /// <summary>
        /// Query tag of the current row.
        /// </summary>
        public DatabaseQueryValues QueryTag { get; set; }

        /// <summary>
        /// End part of the query output
        /// </summary>
        public string Middle { get => _MIDDLE_; }

        /// <summary>
        /// The combining values for each row.
        /// </summary>
        public RowEquivilators CombineRowsValue { get; set; }


        #endregion

        public QueryDatabaseModel()
        {
            QueryTag = DatabaseQueryValues.First_Name;
            CombineRowsValue = RowEquivilators.None;
        }
    }
}
