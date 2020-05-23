namespace CoreApplication.ModelHandlers.Records
{
    using CoreApplication.ModelHandlers.Records.Base;
    using CoreApplication.Models.Records;
    using CoreApplication.Models.Records.Admin;
    using MongoDB.Bson;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
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

        private void InitializeStudentRecord()
        {
            PersonalInformation.Value_Changed += Base_Value_Changed;            
        }
       
    }
}
