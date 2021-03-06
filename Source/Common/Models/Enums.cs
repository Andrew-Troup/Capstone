﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Models
{
    public enum UserTypes
    {
        Student,
        Admin
    }

    public enum ActiveControls
    {
        StudentRecord,
        ClassRecord
    }

    public enum Collections
    {
        Admins,
        Departments,
        Classes,
        Majors,
        Students,
        Users        
    }
}
