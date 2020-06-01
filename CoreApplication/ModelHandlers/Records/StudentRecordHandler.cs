namespace CoreApplication.ModelHandlers.Records
{
    using CoreApplication.ModelHandlers.Records.Base;
    using CoreApplication.Models.Records;
    using CoreApplication.Models.Records.Class.Base;
    using CoreApplication.Models.Records.Student;
    using CoreApplication.Models.Records.Student.Support;
    using MongoDB.Bson;
    using MongoDB.Driver;
    using System;
    using System.Collections.Generic;
    using System.Text;

    class StudentRecordHandler : BaseViewHandler
    {
        #region Properties

        public StudentAcademicInformation AcademicInformation { get; set; }

        #endregion

        #region Constructors

        public StudentRecordHandler()
        {
            AcademicInformation = new StudentAcademicInformation();
            PersonalInformation = new PersonalInformation();
            InitializeStudentRecord();
        }

        public StudentRecordHandler(BsonDocument document)
        {
            if (document != null)
            {
                PersonalInformation = new PersonalInformation(document[1].AsString, document[2].AsString, document[3].AsString,
                            document[4].AsString, document[5].AsString, document[6].AsString);

                BsonDocument subDoc = document[7].AsBsonDocument;
                AcademicInformation = new StudentAcademicInformation(subDoc[0].AsString, subDoc[1].AsString, subDoc[2].AsString, subDoc[3].AsBsonArray,
                            subDoc[4].AsBsonArray, subDoc[5].AsBsonArray);
            }
            else
            {
                PersonalInformation = new PersonalInformation();
                AcademicInformation = new StudentAcademicInformation();
            }

            InitializeStudentRecord();
        }

        #endregion

        #region Methods

        public void RegisterStudent(CourseRecordInformation information)
        {
            TypeClassesPair pair = AcademicInformation.GetTypeClassPair("Registered");
            if (!AcademicInformation.ContainsClass(information.ClassNumber))
            {
                if (pair.Classes.Count > 3)
                {
                    if (Convert.ToDouble(AcademicInformation.GPA) > 3.5)                    
                        pair.Classes.Add(new ClassTakenInformation(information.CourseName, information.Department, information.ClassNumber, ""));
                    else
                        throw new Exception("Insufficeint GPA for the Student to register for more than 3 classes");
                }
                else
                    pair.Classes.Add(new ClassTakenInformation(information.CourseName, information.Department, information.ClassNumber, ""));
            }
            else
                throw new Exception($"The student is already registered to: {information.CourseName}");

            var filter = Builders<BsonDocument>.Filter.Eq("StudentID", PersonalInformation.ID);
            BsonDocument doc = new BsonDocument(){ { "ClassName", $"{information.Department}{information.ClassNumber} - {information.CourseName}" }, { "Grade", "" } };
            var update = Builders<BsonDocument>.Update.AddToSet("AcademicInformation.Registered", doc);
            MainHandlers.DatabaseHandler.Database.GetCollection(Common.Models.Collections.Students).UpdateOne(filter, update);
        }
        
        private void InitializeStudentRecord()
        {
            PersonalInformation.Value_Changed += Base_Value_Changed;
            AcademicInformation.Value_Changed += Base_Value_Changed;
        }

        #endregion
    }
}
