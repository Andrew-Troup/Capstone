using Common.Models;
using Database.Communications;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace Database.Handlers
{
    public class DatabaseHandler
    {
        #region Properties

        public DatabaseCommunications Database { get; private set; }

        #endregion

        public DatabaseHandler()
        {
            Database = new DatabaseCommunications();
        }

        #region Methods

        /// <summary>
        /// Logins the user.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public BsonDocument GetAccountFromLogin(string userName, string password)
        {
            BsonDocument account = null;
            BsonDocument output;
            try
            {
                BsonDocument doc = new BsonDocument{
                    { "UserName", userName },
                    { "Password", password },
                };

                output = Database.GetCollection(Collections.Users).Find(doc).FirstOrDefault<BsonDocument>();
            }
            catch (Exception)
            {
                throw new Exception($"Failed to find User Name: {userName} with password: {password}");
            }

            BsonValue value = null;
            output.GetValue("ID", value);
            FilterDefinition<BsonDocument> filter;
            if (Convert.ToInt32(value) >= 100000)
            {
                filter = Builders<BsonDocument>.Filter.Eq("StudentID", value.AsString);
                account = Database.GetCollection(Collections.Students).Find(filter).FirstOrDefault();
            }
            else
            {
                filter = Builders<BsonDocument>.Filter.Eq("AdminID", value.AsString);
                account = Database.GetCollection(Collections.Admins).Find(filter).FirstOrDefault();
            }

            if (account == null)
                throw new Exception("Could not find a student or admin with the ID assocaited to the login values");

            return account;
        }

        /// <summary>
        /// Gets the admin from a filter
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public BsonDocument GetAdmin(FilterDefinition<BsonDocument> filter)
        {
            return Database.GetCollection(Collections.Admins).Find(filter).FirstOrDefault();
        }

        /// <summary>
        /// Gets the admin from the ID
        /// </summary>
        /// <param name="adminID"></param>
        /// <returns></returns>
        public BsonDocument GetAdmin(string adminID)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("AdminID", adminID);
            return Database.GetCollection(Collections.Admins).Find(filter).FirstOrDefault();
        }

        /// <summary>
        /// Gets a student from a filter
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public BsonDocument GetStudent(FilterDefinition<BsonDocument> filter)
        {
            return Database.GetCollection(Collections.Students).Find(filter).FirstOrDefault();
        }

        /// <summary>
        /// Gets a student from a student ID
        /// </summary>
        /// <param name="studentID"></param>
        /// <returns></returns>
        public BsonDocument GetStudent(string studentID)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("StudentID", studentID);
            return Database.GetCollection(Collections.Students).Find(filter).FirstOrDefault();
        }

        /// <summary>
        /// Updates the database from values edited by the user.
        /// </summary>
        /// <param name="values"></param>
        /// <param name="ID"></param>
        public void UpdateDatabase(List<string> values, string ID)
        {
            FilterDefinition<BsonDocument> filter = Builders<BsonDocument>.Filter.Eq("StudentID", ID);
            string[] parts = values[0].Split(':');
            
            UpdateDefinition<BsonDocument> update = Builders<BsonDocument>.Update.Set(parts[0], parts[1]);
            for(int i = 1; i < values.Count; i++)
            {
                parts = values[i].Split(':');
                update = update.Set(parts[0], parts[1]);
            }

            var results = Database.GetCollection(Collections.Students).UpdateOne(filter, update);
        }

        #endregion
    }
}
