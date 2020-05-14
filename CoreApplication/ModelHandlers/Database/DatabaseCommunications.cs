using CoreApplication.Models.Database;
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
        public MongoClient Client { get; private set; }

        public DatabaseCommunications(User user)
        {
            Client = new MongoClient($"mongodb+srv://{user}@student-classcourserecords-ejxro.azure.mongodb.net/test?retryWrites=true&w=majority");
        }
    }
}
