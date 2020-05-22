namespace Database.Communications
{
    using Common.Models;
    using Common.Models.Login;
    using MongoDB.Bson;
    using MongoDB.Driver;
    using System;
    using System.Collections.Generic;
    using System.Text;

    class DatabaseCommunications
    {
        #region Constants

        // TODO: this is useless, but I may be able to connect with a user name.
        private const string _databaseKey = "49ffabac-7230-43e4-8e7a-223786422914";

        private const string _databaseName = "Records";

        private const string _loginDatabaseName = "Login";

        #endregion

        #region Properties

        public MongoClient Client { get; private set; }

        public IMongoDatabase RecordsDatabase { get; private set; }

        #endregion

        public DatabaseCommunications()
        {
            //Default:Default
            Client = new MongoClient($"mongodb+srv://Andrew-Troup:Airforce!@student-classcourserecords-ejxro.azure.mongodb.net/test?retryWrites=true&w=majority");
            Client.Cluster.StartSession();
            RecordsDatabase = Client.GetDatabase(_databaseKey);
        }

        #region Methods

        public IMongoCollection<BsonDocument> GetCollection(Collections type)
        {
            string value = type.ToString();
            return RecordsDatabase.GetCollection<BsonDocument>(value);
        }

        public void Login(UserLoginInformation user)
        {
            try
            {
                Client = new MongoClient($"mongodb+srv://{user.ToString()}@student-classcourserecords-ejxro.azure.mongodb.net/test?retryWrites=true&w=majority");
                RecordsDatabase = Client.GetDatabase(_databaseKey);
            }
            catch(Exception)
            {
                // DOes nothing since the login information failed.
            }
        }

        #endregion
    }
}
