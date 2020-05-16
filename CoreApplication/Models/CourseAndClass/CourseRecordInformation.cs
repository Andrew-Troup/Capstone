using System;
using System.Collections.Generic;
using System.Text;

namespace CoreApplication.Models.CourseAndClass
{
    class CourseRecordInformation
    {
        public string CourseID { get; set; }

        public string CourseName { get; set; }

        public string CreditHours { get; set; }

        public string Description { get; set; }

        public CourseRecordInformation(string courseID, string courseName, string creditHours, string description)
        {
            CourseID = courseID;
            CourseName = courseName;
            CreditHours = creditHours;
            Description = description;
        }

        public CourseRecordInformation()
        {

        }
    }
}
