namespace CoreApplication.ModelHandlers
{
    using Common.Models;
    using Common.Models.Login;
    using CoreApplication.ModelHandlers;
    using CoreApplication.ModelHandlers.Records;
    using CoreApplication.ModelHandlers.Records.Base;
    using CoreApplication.Models.Base;
    using CoreApplication.User_Interfaces.Left_Sides;
    using CoreApplication.User_Interfaces.Right_Sides;
    using MongoDB.Bson;
    using MongoDB.Driver;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Windows.Controls;

    class MainWindowManager
    {
        #region Properties

        /// <summary>
        /// Contains all the active / inactive controls used by the user.
        /// </summary>
        public Dictionary<UserControlTypes, UserControl> UserControls { get; private set; }

        /// <summary>
        /// The control as to which we are looking at currently
        /// </summary>
        public ActiveControls ActiveControl { get; set; }

        /// <summary>
        /// The user that is logged into the system
        /// </summary>
        public UserLoginInformation CurrentUser { get; set; }

        /// <summary>
        /// Information gathered about the user.
        /// </summary>
        public BaseViewHandler ViewHandler { get; set; }

        /// <summary>
        /// Stores the class record handler
        /// </summary>
        public ClassRecordHandler ClassRecords { get; set; }

        /// <summary>
        /// Tells the us if the user type is a student
        /// </summary>
        public bool IsAdmin { get => (CurrentUser.UserType == UserTypes.Admin); }

        #endregion

        public MainWindowManager(BsonDocument user)
        {
            CurrentUser = new UserLoginInformation(user[1].AsString, user[2].AsString, user[3].AsString, user[4].AsString);
            ClassRecords = new ClassRecordHandler(MainHandlers.DatabaseHandler.Database.GetCollection(Common.Models.Collections.Departments).Find(new BsonDocument()).ToEnumerable().ToList());
            UserControls = new Dictionary<UserControlTypes, UserControl>();


            if (!IsAdmin)
                ViewHandler = new StudentRecordHandler(MainHandlers.DatabaseHandler.GetStudent(CurrentUser.UserID));
            else
                ViewHandler = new AdminRecordHandler(MainHandlers.DatabaseHandler.GetAdmin(CurrentUser.UserID));
        }

        #region Methods

        /// <summary>
        /// If the user control exists, then it returns the existing one. Else it creates it.
        /// </summary>
        /// <param name="control"></param>
        /// <returns></returns>
        public UserControl GetUserControl(UserControlTypes control)
        {
            UserControl output = null;
            if (UserControls.ContainsKey(control))
                output = UserControls[control];
            else
            {
                switch (control)
                {
                    case UserControlTypes.StudentRecordLayout:
                        output = (UserControl)new StudentRecordLayout();
                        UserControls.Add(UserControlTypes.StudentRecordLayout, output);
                        break;

                    case UserControlTypes.StudentRecordTreeView:
                        if (IsAdmin)
                            output = (UserControl)new AdminStudentTreeView();
                        else
                            output = (UserControl)new StudentRecordTreeView();

                        UserControls.Add(UserControlTypes.StudentRecordTreeView, output);
                        break;

                    case UserControlTypes.CourseRecordLayout:
                        output = (UserControl)new CourseRecordLayout();
                        UserControls.Add(UserControlTypes.CourseRecordLayout, output);
                        break;

                    case UserControlTypes.CourseRecordTreeView:
                        output = (UserControl)new CourseRecordTreeView();
                        UserControls.Add(UserControlTypes.CourseRecordTreeView, output);
                        break;
                }
            }

            return output;
        }

        #endregion
    }
}
