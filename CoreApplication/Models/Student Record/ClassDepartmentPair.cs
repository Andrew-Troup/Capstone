using System;
using System.Collections.Generic;
using System.Text;

namespace CoreApplication.Models.Student_Record
{
    class ClassDepartmentPair
    {
        public string Class { get; set; }

        public string Department { get; set; }

        public ClassDepartmentPair(string classz, string departments)
        {
            Class = classz;
            Department = departments;
        }

        public ClassDepartmentPair()
        {
           
        }
    }
}
