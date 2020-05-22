using Database.Communications;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace Database.Handlers
{
    class DatabaseHandler
    {
        #region Properties

        public DatabaseCommunications Database { get; private set; }

        #endregion

        public DatabaseHandler()
        {
            Database = new DatabaseCommunications();
        }

        #region Methods

        public BsonDocument GetStudent()

        #endregion
    }
}
