namespace CoreApplication.Models.Query
{
    using DnsClient;
    using System;
    using System.Collections.Generic;
    using System.Text;

    class QueryDatabaseModel
    {

        public delegate void UpdateValue(object sender, UpdateValueType type);

        public event UpdateValue Update_Value;

        #region Constants

        private const string _FIRST_ = "{\"";

        private const string _MIDDLE_ = "\":\"";

        private const string _LAST_ = "\"}";

        #endregion

        #region Variables

        private string _first;

        private string _last;

        #endregion

        #region Properties

        /// <summary>
        /// First part of the query output
        /// </summary>
        public string First { get => $"{_first}{_FIRST_}"; }

        /// <summary>
        /// Middle part of the query output
        /// </summary>
        public string Last { get => $"{_LAST_}{_last}"; }

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

        /// <summary>
        /// Data to be queried
        /// </summary>
        public string Data { get; set; }
        #endregion

        public QueryDatabaseModel()
        {
            _last = string.Empty;
            _first = string.Empty;
            QueryTag = DatabaseQueryValues.FirstName;
            CombineRowsValue = RowEquivilators.None;
        }

        #region Methods

        /// <summary>
        /// Adds a first bracer to the object
        /// </summary>
        public void AddFirstBracer()
        {
            _first += "{";
        }

        /// <summary>
        /// Adds a last bracer to the object
        /// </summary>
        public void AddLastBracer()
        {
            _last += "}";
        }

        /// <summary>
        /// Removes a first bracer from the object
        /// </summary>
        public void RemoveFirstBracer()
        {
            if (_first.Contains("{"))          
                _first = _first.Remove(_first.IndexOf("{"));
        }
        
        /// <summary>
        /// Removes a last bracer from the object
        /// </summary>
        public void RemoveLastBracer()
        {
            if (_last.Contains("}"))
                _last = _last.Remove(_last.IndexOf("}"));
        }

        public override string ToString()
        {
            if (string.IsNullOrEmpty(Data))
                throw new NullReferenceException("Data was null");

            return "{ \"" + QueryTag.ToString() + "\" : \"" + Data + "\" }";
        }
        #endregion
    }

    public class UpdateValueType
    {
        public RowEquivilators Row { get; set; }
        public UpdateValueType(RowEquivilators row) => Row = row;
    }
}
