using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace CoreApplication.Models.Records.Student.Support
{
    class TypeClassesPair
    {
        public string Name { get; set; }

        public ObservableCollection<ClassTakenInformation> Classes { get; set; }

        public TypeClassesPair(string name, ObservableCollection<ClassTakenInformation> classes)
        {
            Name = name;
            Classes = classes;
        }

        public TypeClassesPair()
        {
            Classes = new ObservableCollection<ClassTakenInformation>();
        }
    }
}
