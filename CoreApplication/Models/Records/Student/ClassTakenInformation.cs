namespace CoreApplication.Models.Records.Student
{
    using CoreApplication.Models.Base;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Text.RegularExpressions;

    // TODO ALLOWS THIS TO BE EDITABLE
    class ClassTakenInformation : UpdateDatabase
    {
        public string Class { get; set; }

        public string Department { get; set; }

        public string ClassNumber { get; set; }
        
        public string Grade { get; set; }


        public ClassTakenInformation(string classz, string departments, string grade)
        {
            Class = classz;
            Department = departments;
            Grade = grade;
        }

        public ClassTakenInformation(string classDepartmentPair, string grade)
        {
            Regex r = new Regex(@"(?<department>[A-Z]*)(?<classnumber>[0-9]*)");
            string[] parts = classDepartmentPair.Split('-');
            MatchCollection matches = r.Matches(parts[0]);
            ClassNumber = matches[0].Groups["classnumber"].ToString();
            Department = matches[0].Groups["department"].ToString();

            Class = parts[1].Trim();
            if (!string.IsNullOrEmpty(grade))
                Grade = grade;
            else
                Grade = string.Empty;
        }

        public ClassTakenInformation()
        {
           
        }
    }
}
