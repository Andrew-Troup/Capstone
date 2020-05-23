namespace CoreApplication.Models.Records
{
    using CoreApplication.Models.Base;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Text;

    class PersonalInformation : UpdateDatabase
    {
        #region Variables

        private string _email;

        private string _firstName;

        private string _lastName;

        private string _middleName;

        private string _ID;

        private string _dob;

        #endregion

        #region Properties

        /// <summary>
        /// Current Student's email
        /// </summary>
        public string Email 
        { 
            get => _email; 
            set
            {
                _email = value;
                RaiseUpdateDatabase($"Email:{_email}");
            }
        }

        /// <summary>
        /// Current student's first name
        /// </summary>
        public string FirstName
        {
            get => _firstName;
            set
            {
                _firstName = value;
                RaiseUpdateDatabase($"FirstName:{_firstName}");
            }
        }

        /// <summary>
        /// Current student's last name
        /// </summary>
        public string LastName 
        {
            get => _lastName;
            set 
            {
                _lastName = value;
                RaiseUpdateDatabase($"LastName:{_lastName}");
            }
        }

        /// <summary>
        /// Current student's middle name
        /// </summary>
        public string MiddleName
        {
            get => _middleName;
            set
            {
                _middleName = value;
                RaiseUpdateDatabase($"MiddleName:{_middleName}");
            }
        }

        /// <summary>
        /// Current student's ID
        /// </summary>
        public string ID 
        {
            get => _ID;
        }

        /// <summary>
        /// date of birth of the student.
        /// </summary>
        public string DOB
        {
            get => _dob;
            set
            {
                _dob = value;
                RaiseUpdateDatabase($"DOB:{_dob}");
            }
        }
        #endregion

        public PersonalInformation(string firstName, string middleName, string lastName, string studentID, string email, string dob)
        {
            _firstName = firstName;
            _middleName = middleName;
            _lastName = lastName;
            _ID = studentID;
            _email = email;
            _dob = dob;
        }

        public PersonalInformation()
        {

        }

    }
}
