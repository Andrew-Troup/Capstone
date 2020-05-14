using System;
using System.Collections.Generic;
using System.Text;

namespace CoreApplication.Models.Database
{
    class User
    {
        public string UserID { get; private set; }

        public string Password { get; private set; }

        public User(string userID, string password)
        {
            UserID = userID;
            Password = password;
        }

        public override string ToString()
        {
            return $"{UserID}:{Password}";
        }
    }
}
