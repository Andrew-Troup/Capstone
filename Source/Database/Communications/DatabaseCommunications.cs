namespace Database.Communications
{
    using Common.Models;
    using Common.Models.Login;
    using MongoDB.Bson;
    using MongoDB.Driver;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class DatabaseCommunications
    {
        #region Constants

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
            RecordsDatabase = Client.GetDatabase(_databaseName);
        }

        #region Methods

        public IMongoCollection<BsonDocument> GetCollection(Collections type)
        {
            string value = type.ToString();
            IMongoCollection<BsonDocument> collection = RecordsDatabase.GetCollection<BsonDocument>(value);
            return collection;
        }

        public void Login(UserLoginInformation user)
        {
            try
            {
                Client = new MongoClient($"mongodb+srv://{user.ToString()}@student-classcourserecords-ejxro.azure.mongodb.net/test?retryWrites=true&w=majority");
            }
            catch(Exception)
            {
                // DOes nothing since the login information failed.
                // TODO: The login window will call this information, I need to figure out how to set it up so that if it does not connect we reset the login infromation on the UI
            }
        }

        #endregion
    }
}
