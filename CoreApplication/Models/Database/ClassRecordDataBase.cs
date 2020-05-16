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

        /// <summary>
        /// Name of the collection we are accessing
        /// </summary>
        public string CollectionName { get => _collection; }

        /// <summary>
        /// Name of the database we are accessing.
        /// </summary>
        public string DataBaseName { get => _dataBase; }

    }
}