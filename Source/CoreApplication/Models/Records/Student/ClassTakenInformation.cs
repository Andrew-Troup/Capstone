namespace CoreApplication.Models.Records.Student
{
    using CoreApplication.ModelHandlers;
    using CoreApplication.Models.Base;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Text.RegularExpressions;

    class ClassTakenInformation : UpdateDatabase
    {
        #region Variables

        private string _class;

        private string _department;

        private string _classNumber;

        private string _grade;

        #endregion

        #region Properties

        public string Class 
        { 
            get => _class;
            set
            {
                _class = value;
                RaiseUpdateDatabase($"ClassName:{_department}{_classNumber} - {_class}");
            }
        }

        public string Department 
        {
            get => _department;
            set
            {
                _department = value;
                RaiseUpdateDatabase($"ClassName:{_department}{_classNumber} - {_class}");
            }
        }

        public string ClassNumber 
        {
            get => _classNumber;
            set 
            {
                _classNumber = value;
                RaiseUpdateDatabase($"ClassName:{_department}{_classNumber} - {_class}");
            }
        }
        
        public string Grade 
        { 
            get => _grade;
            set
            {
                if (MainHandlers.WindowManager.IsAdmin)
                {
                    _grade = value;
                    RaiseUpdateDatabase(string.Format("ClassName:{0}{1} - {2}\tGrade:{3}", _department, _classNumber, _class, _grade));
                }
            }
        }

        #endregion

        public ClassTakenInformation(string classz, string departments, string classNumber, string grade)
        {
            _class = classz;
            _department = departments;
            _grade = grade;
            _classNumber = classNumber;
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
                _grade = grade;
            else
                _grade = string.Empty;
        }

        public ClassTakenInformation()
        {
           
        }

        public override string ToString()
        {
            return $"{_department}{_classNumber} - {_class}";
        }
    }
}
