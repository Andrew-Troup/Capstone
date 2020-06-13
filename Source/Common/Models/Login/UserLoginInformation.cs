using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Models.Login
{
    public class UserLoginInformation
    {
        /// <summary>
        /// Login user name
        /// </summary>
        public string UserName { get; private set; }

        /// <summary>
        /// User's ID
        /// </summary>
        public string UserID { get; private set; }

        /// <summary>
        /// User's Password
        /// </summary>
        public string Password { get; private set; }

        /// <summary>
        /// The type of user that we are logging in as.
        /// </summary>
        public UserTypes UserType { get; set; }

        /// <summary>
        /// Basic Constructor
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="password"></param>
        public UserLoginInformation(string userName, string password, string userID, string userType)
        {
            UserName = userName;
            UserID = userID;
            Password = password;
            UserType = (UserTypes)Enum.Parse(typeof(UserTypes), userType);
        }

        /// <summary>
        /// Override of toString to allow me to easily append this data to the database login string.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{UserID}:{Password}";
        }
    }
}
