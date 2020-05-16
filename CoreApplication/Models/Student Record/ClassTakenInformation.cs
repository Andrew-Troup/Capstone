using System;
using System.Collections.Generic;
using System.Text;

namespace CoreApplication.Models.Student_Record
{
    class ClassTakenInformation
    {
        public string Class { get; set; }

        public string Department { get; set; }

        public string Grade { get; set; }
        public ClassTakenInformation(string classz, string departments, string grade)
        {
            Class = classz;
            Department = departments;
            Grade = grade;
        }

        public ClassTakenInformation()
        {
           
        }
    }
}
