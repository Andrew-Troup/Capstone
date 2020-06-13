namespace CoreApplication.Models.Records.Admin
{
    using CoreApplication.ModelHandlers.Records;
    using CoreApplication.Models.Records.Admin.Support;
    using MongoDB.Bson;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Text;

    class AdminRecord
    {
        #region Properties

        #region Private

        private bool HasQuery { get; set; }

        private int QueryPosition { get; set; }

        #endregion

        #region Public

        /// <summary>
        /// The current classes being taught by the admin.
        /// </summary>
        public ObservableCollection<ClassesBeingTaught> Teaching { get; private set; }

        #endregion

        #endregion

        public AdminRecord(BsonArray array)
        {
            HasQuery = false;
            QueryPosition = -1;
            Teaching = new ObservableCollection<ClassesBeingTaught>();
            foreach (BsonDocument doc in array)
                Teaching.Add(new ClassesBeingTaught(doc));
        }

        #region Methods

        /// <summary>
        /// Adds a query to the student tree view.
        /// </summary>
        /// <param name="docs"></param>
        public void AddQuery(List<BsonDocument> docs)
        {
            if (docs != null)
            {
                // REmove the pre-existing query if there is one.
                RemoveQuery();
                HasQuery = true;
                Teaching.Add(new ClassesBeingTaught(docs, "Query Values", " "));
                QueryPosition = Teaching.Count - 1;
            }
        }

        /// <summary>
        /// Removes the Query from the tree view.
        /// </summary>
        public void RemoveQuery()
        {
            if(HasQuery)
            {
                Teaching.RemoveAt(QueryPosition);
                HasQuery = false;
                QueryPosition = -1;
            }
        }

        #endregion
    }
}
