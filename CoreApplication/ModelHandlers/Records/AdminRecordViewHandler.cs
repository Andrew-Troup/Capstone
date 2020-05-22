namespace CoreApplication.ModelHandlers.Records
{
    using CoreApplication.ModelHandlers.Records.Base;
    using CoreApplication.Models.Records;
    using CoreApplication.Models.Records.Admin;
    using MongoDB.Bson;
    using System;
    using System.Collections.Generic;
    using System.Text;

    internal class AdminRecordViewHandler : BaseViewHandler
    {   
        public AdminRecord Record { get; private set; } 

        public AdminRecordViewHandler(BsonDocument document)
        {
            PersonalInformation = new PersonalInformation(document[1].AsString, document[2].AsString, document[3].AsString,
                        document[4].AsString, document[5].AsString, document[6].AsString);

            Record = new AdminRecord(document[7].AsBsonArray);
            InitializeStudentRecord();
        }

        private void InitializeStudentRecord()
        {
            PersonalInformation.Value_Changed += Base_Value_Changed;            
        }
    }
}
