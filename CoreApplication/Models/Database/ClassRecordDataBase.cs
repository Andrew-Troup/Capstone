using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CoreApplication.Models.Database
{
    class ClassRecordDataBase
    {
        private const string _collection = "Class";
        private const string _dataBase = "Records";
        public string CollectionName { get => _collection; }

        public string DataBaseName { get => _dataBase; }

    }
}