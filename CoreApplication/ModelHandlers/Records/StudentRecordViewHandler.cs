namespace CoreApplication.ModelHandlers.Records
{
    using CoreApplication.ModelHandlers.Records.Base;
    using CoreApplication.Models.Records;
    using CoreApplication.Models.Records.Student_Record;
    using MongoDB.Bson;
    using System;
    using System.Collections.Generic;
    using System.Text;

    class StudentRecordViewHandler : BaseViewHandler
    {
        #region Properties

        public StudentAcademicInformation AcademicInformation { get; set; }

        #endregion

        public StudentRecordViewHandler()
        {
            AcademicInformation = new StudentAcademicInformation();
            PersonalInformation = new PersonalInformation();
            InitializeStudentRecord();
        }

        public StudentRecordViewHandler(BsonDocument document)
        {            
            PersonalInformation = new PersonalInformation(document[1].AsString, document[2].AsString, document[3].AsString,
                        document[4].AsString, document[5].AsString, document[6].AsString);

            BsonDocument subDoc = document[7].AsBsonDocument;
            AcademicInformation = new StudentAcademicInformation(subDoc[0].AsString, subDoc[1].AsString, subDoc[2].AsString, subDoc[3].AsBsonArray,
                        subDoc[4].AsBsonArray);

            InitializeStudentRecord();
        }

        private void InitializeStudentRecord()
        {
            PersonalInformation.Value_Changed += Base_Value_Changed;
            AcademicInformation.Value_Changed += Base_Value_Changed;
        }        
    }
}
