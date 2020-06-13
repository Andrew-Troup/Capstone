namespace CoreApplication.ModelHandlers.Records
{
    using CoreApplication.ModelHandlers.Records.Base;
    using CoreApplication.Models.Records;
    using CoreApplication.Models.Records.Admin;
    using MongoDB.Bson;
    using MongoDB.Driver;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;

    internal class AdminRecordHandler : BaseViewHandler
    {
        public event EventHandler UpdateUI;

        #region Variables

        private StudentRecordHandler _selectedStudent;

        #endregion

        #region Properties

        public AdminRecord Record { get; private set; } 

        public StudentRecordHandler SelectedStudent 
        {
            get => _selectedStudent;          
            set
            {
                _selectedStudent = value;
                UpdateUI.Invoke(this, null);
            }
        }

        #endregion

        internal AdminRecordHandler(BsonDocument document)
        {            
            PersonalInformation = new PersonalInformation(document[1].AsString, document[2].AsString, document[3].AsString,
                        document[4].AsString, document[5].AsString, "");

            Record = new AdminRecord(document[6].AsBsonArray);
            InitializeStudentRecord();
        }

        #region Methods

        #region Public

        public void SearchDatabase(string text)
        {
            try
            {
                int studentID;
                BsonDocument doc;
                if (int.TryParse(text, out studentID))
                    doc = MainHandlers.DatabaseHandler.GetStudent(studentID.ToString());
                else
                {
                    string[] parts = text.Split(' ');
                    FilterDefinition<BsonDocument>[] filters = new FilterDefinition<BsonDocument>[3];

                    filters[0] = Builders<BsonDocument>.Filter.Eq("FirstName", parts[0]);
                    if (parts.Length >= 2)
                    {
                        filters[1] = Builders<BsonDocument>.Filter.Eq("LastName", parts[parts.Length - 1]);
                        if (parts.Length == 3)
                            filters[2] = Builders<BsonDocument>.Filter.Eq("MiddleName", parts[1]);
                    }                    

                    FilterDefinition<BsonDocument> filter = Builders<BsonDocument>.Filter.And(filters.Where(x=> x != null));
                    doc = MainHandlers.DatabaseHandler.Database.GetCollection(Common.Models.Collections.Students).Find(filter).ToBsonDocument();
                }
            }
            catch(Exception e)
            {

            }
        }
        #endregion
        
        #region Private

        private void InitializeStudentRecord()
        {
            PersonalInformation.Value_Changed += Base_Value_Changed;            
        }

        #endregion

        #endregion

    }
}
