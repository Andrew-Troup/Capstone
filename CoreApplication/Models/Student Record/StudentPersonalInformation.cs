using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace CoreApplication.Models.Student_Record
{
    class StudentPersonalInformation
    {
        /// <summary>
        /// Current Student's email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Current student's first name
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Current student's last name
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Current student's ID
        /// </summary>
        public string StudentID { get; set; }

        public StudentPersonalInformation(string firstName, string lastName, string studentID, string email)
        {
            FirstName = firstName;
            LastName = lastName;
            StudentID = studentID;
            Email = email;
        }

        public StudentPersonalInformation()
        {

        }

    }
}
