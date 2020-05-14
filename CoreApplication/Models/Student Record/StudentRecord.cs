using System;
using System.Collections.Generic;
using System.Text;

namespace CoreApplication.Models.Student_Record
{
    class StudentRecord
    {
        public StudentAcademicInformation AcademicInformaiton { get; set; }

        public List<ClassDepartmentPair> Taken { get; set; }

        public StudentPersonalInformation PersonalInformation { get; set; }

        public StudentRecord(double gpa, double totalUnits, string major, List<string> classesTaken, List<string> departments,
            string firstName, string lastName, string studentID, string email)
        {
            AcademicInformaiton = new StudentAcademicInformation(gpa, totalUnits, major);
            Taken = new List<ClassDepartmentPair>();
            for (int i = 0; i < classesTaken.Count; i++)
                Taken.Add(new ClassDepartmentPair(classesTaken[i], departments[i]));

            PersonalInformation = new StudentPersonalInformation(firstName, lastName, studentID, email);
        }

        public StudentRecord()
        {
            AcademicInformaiton = new StudentAcademicInformation();
            Taken = new List<ClassDepartmentPair>();
            PersonalInformation = new StudentPersonalInformation();
        }
    }
}
