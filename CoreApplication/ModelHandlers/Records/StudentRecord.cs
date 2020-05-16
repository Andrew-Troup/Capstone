using CoreApplication.Models.Student_Record;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreApplication.ModelHandlers.Records
{
    class StudentRecord
    {
        #region Properties

        public StudentAcademicInformation AcademicInformation { get; set; }

        public StudentPersonalInformation PersonalInformation { get; set; }

        public List<string> UpdateInformation { get; set; }

        #endregion

        public StudentRecord()
        {
            AcademicInformation = new StudentAcademicInformation();
            PersonalInformation = new StudentPersonalInformation();
            InitializeStudentRecord();
        }

        public StudentRecord(BsonDocument document)
        {            
            PersonalInformation = new StudentPersonalInformation(document[1].AsString, document[2].AsString, document[3].AsString,
                        document[4].AsString, document[5].AsString, document[6].AsString);

            BsonDocument subDoc = document[7].AsBsonDocument;
            AcademicInformation = new StudentAcademicInformation(subDoc[0].AsString, subDoc[1].AsString, subDoc[2].AsString, subDoc[3].AsString,
                        subDoc[4].AsString);

            InitializeStudentRecord();
        }

        private void InitializeStudentRecord()
        {
            UpdateInformation = new List<string>();
            PersonalInformation.Value_Changed += StudentInformation_Value_Changed;
            AcademicInformation.Value_Changed += StudentInformation_Value_Changed;
        }

        private void StudentInformation_Value_Changed(object sender, Models.Base.ValueChangedArgs e)
        {
            UpdateInformation.Add(e.Text);
        }
    }
}
