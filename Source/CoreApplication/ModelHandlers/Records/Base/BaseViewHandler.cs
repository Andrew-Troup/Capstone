﻿namespace CoreApplication.ModelHandlers.Records.Base
{
    using CoreApplication.Models.Records;
    using MongoDB.Bson;
    using MongoDB.Driver;
    using System;
    using System.Collections.Generic;
    using System.Text;

    class BaseViewHandler
    {
        #region Properties

        /// <summary>
        /// Current personal information
        /// </summary>
        public PersonalInformation PersonalInformation{get; set;}

        /// <summary>
        /// Stores the data to be updated to the server later.
        /// </summary>
        public List<string> UpdateInformation { get; set; }

        #endregion

        public BaseViewHandler(string firstName, string middleName, string lastName, string id, string email, string dob)
        {
            PersonalInformation = new PersonalInformation(firstName, middleName, lastName, id, email, dob);
            UpdateInformation = new List<string>();
        }

        public BaseViewHandler()
        {
            UpdateInformation = new List<string>();
        }

        public void Base_Value_Changed(object sender, Models.Base.ValueChangedArgs e)
        {
            UpdateInformation.Add(e.Text);
        }

        public List<string> GetMajors()
        {
            List<string> majors = new List<string>();
            List<BsonDocument> documents = MainHandlers.DatabaseHandler.Database.GetCollection(Common.Models.Collections.Majors).Find(new BsonDocument()).ToList();
            foreach (BsonDocument document in documents)
                majors.Add(document[1].AsString);

            return majors;
        }
    }
}
