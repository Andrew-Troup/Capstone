namespace CoreApplication.Models.Records.Admin
{
    using CoreApplication.Models.Records.Admin.Support;
    using MongoDB.Bson;
    using System;
    using System.Collections.Generic;
    using System.Text;

    class AdminRecord
    {
        #region Properties

        /// <summary>
        /// The current classes being taught by the admin.
        /// </summary>
        public List<ClassesBeingTaught> Teaching { get; private set; }
        
        #endregion

        public AdminRecord(BsonArray array)
        {
            Teaching = new List<ClassesBeingTaught>();
            foreach (BsonDocument doc in array)
                Teaching.Add(new ClassesBeingTaught(doc));
        }
    }
}
