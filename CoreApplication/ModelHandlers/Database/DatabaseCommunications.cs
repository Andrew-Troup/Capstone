using CoreApplication.ModelHandlers.Records;
using CoreApplication.Models.Database;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreApplication.ModelHandlers.Database
{
    class DatabaseCommunications
    {
        // TODO: this is useless, but I may be able to connect with a user name.
        private const string _databaseKey = "49ffabac-7230-43e4-8e7a-223786422914";

        private const string _databaseName = "Records";
        #region Enumerations

        public enum Collections
        {
            Students,
            Classes
        }

        #endregion


        #region Properties

        public MongoClient Client { get; private set; }

        public IMongoDatabase RecordsDatabase { get; set; }

        #endregion

        public DatabaseCommunications()
        {
            Client = new MongoClient($"mongodb+srv://Andrew-Troup:Airforce!@student-classcourserecords-ejxro.azure.mongodb.net/test?retryWrites=true&w=majority");
            RecordsDatabase = Client.GetDatabase(_databaseName);
        }

        #region Methods

        public IMongoCollection<BsonDocument> GetCollection(Collections type)
        {
            string value = type.ToString();
            return RecordsDatabase.GetCollection<BsonDocument>(value);
        }

        #endregion
    }
}
