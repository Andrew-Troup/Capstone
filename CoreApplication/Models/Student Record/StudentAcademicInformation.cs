using System;
using System.Collections.Generic;
using System.Text;

namespace CoreApplication.Models.Student_Record
{
    class StudentAcademicInformation
    {
        /// <summary>
        /// Student's GPA
        /// </summary>
        public double GPA { get; set; }

        public double TotalUnits { get; set; }

        /// <summary>
        /// Student's major
        /// </summary>
        public string Major { get; set; }

        public StudentAcademicInformation( double gpa, double totalUnits, string major)
        {
            GPA = gpa;
            TotalUnits = totalUnits;
            Major = major;
        }

        public StudentAcademicInformation()
        {
        }
    }
}
