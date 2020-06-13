namespace CoreApplication.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Text;

    public enum RowEquivilators
    {
        [Description("None")]
        None,

        [Description("And")]
        And,

        [Description("Or")]
        Or
    }

    public enum DatabaseQueryValues 
    {
        [Description("First Name")]
        FirstName,

        [Description("Last Name")]
        LastName,

        [Description("Middle Name")]
        MiddleName,

        [Description("Student ID")]
        StudentID,

        [Description("Email")]
        Email,

        [Description("Date of Birth")]
        DOB
    }

    public enum InsertLocation 
    {
        Before,
        After,
        Into
    }

}
