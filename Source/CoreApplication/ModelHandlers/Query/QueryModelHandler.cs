namespace CoreApplication.ModelHandlers.Query
{
    using CoreApplication.Models;
    using CoreApplication.Models.Query;
    using CoreApplication.Models.Support;
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;
    using MongoDB.Driver;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;
    using System.Threading;

    class QueryModelHandler
    {
        #region Properties

        public List<List<QueryDatabaseModel>> QueryLists { get; private set;}

        /// <summary>
        /// Items being displayed.  
        /// </summary>
        public ObservableCollection<QueryDatabaseModel> QueryItems { 
            get 
            {
                ObservableCollection<QueryDatabaseModel> collection = new ObservableCollection<QueryDatabaseModel>();
                foreach (List<QueryDatabaseModel> items in QueryLists)
                    foreach (QueryDatabaseModel item in items)
                        collection.Add(item);

                return collection;
            } 
        }       

        /// <summary>
        /// Current list of selected items
        /// </summary>
        public List<QueryDatabaseModel> SelectedIndexes { get; set; }

        #endregion

        public QueryModelHandler()
        {
            QueryLists = new List<List<QueryDatabaseModel>>();
            QueryLists.Add(new List<QueryDatabaseModel>() { new QueryDatabaseModel() });
            SelectedIndexes = new List<QueryDatabaseModel>();
            QueryItems.Add(new QueryDatabaseModel());
        }

        #region Methods

        #region Public

        /// <summary>
        /// Inserts a query at a location
        /// </summary>
        /// <param name="locationType"></param>
        public void InsertQuery(InsertLocation locationType)
        {
            QueryDatabaseModel item = SelectedIndexes[0];
            var location = FindIndex(item);
            switch (locationType)
            {
                case InsertLocation.After:
                    if (location.main + 1 > QueryLists.Count)
                        QueryLists.Add(new List<QueryDatabaseModel>() { new QueryDatabaseModel() });
                    else
                        QueryLists.Insert(location.main + 1, new List<QueryDatabaseModel>() { new QueryDatabaseModel() });
                    break;

                case InsertLocation.Before:
                    QueryLists.Insert(location.main, new List<QueryDatabaseModel>() { new QueryDatabaseModel() });
                    break;

                case InsertLocation.Into:
                    QueryDatabaseModel value = new QueryDatabaseModel();
                    value.AddLastBracer();
                    if (QueryLists[location.main].Count == 1)                        
                        QueryLists[location.main][0].AddFirstBracer();
                    else
                        QueryLists[location.main][QueryLists[location.main].Count - 1].RemoveLastBracer();

                    QueryLists[location.main].Add(value);
                    break;
            }
        }

        /// <summary>
        /// deletes a selected query
        /// </summary>
        public void DeleteQuery()
        {
            foreach (QueryDatabaseModel model in SelectedIndexes)
            {
                var location = FindIndex(model);
                if (QueryLists[location.main].Count == 1)
                    QueryLists.RemoveAt(location.main);
                else
                {
                    QueryLists[location.main].RemoveAt(location.sub);
                    if (QueryLists[location.main].Count == 1)
                    {
                        QueryLists[location.main][0].RemoveFirstBracer();
                        QueryLists[location.main][0].RemoveLastBracer();
                    }
                    else
                    {
                        if (location.sub == 0)
                            QueryLists[location.main][0].AddFirstBracer();
                        if (location.sub == QueryLists[location.main].Count)
                            QueryLists[location.main][QueryLists[location.main].Count - 1].AddLastBracer();
                    }
                }
            }
        }

        /// <summary>
        /// Combines the rows into the same object
        /// TODO: I may incorporate this
        /// </summary>
        public void CombineRows()
        {
            if(SelectedIndexes.Count != 0)
            {

            }
        }

        /// <summary>
        /// Performs the query
        /// </summary>
        public List<BsonDocument> PerformQuery()
        {           
            FilterDefinition<BsonDocument> filter = ComposeFilter();
            List<BsonDocument> doc = MainHandlers.DatabaseHandler.Database.GetCollection(Common.Models.Collections.Students).Find(filter).ToEnumerable().ToList();
            //BsonArray array = doc.AsBsonArray;
            return doc;
        }

        #endregion

        #region Private
        
        /// <summary>
        /// Finds the current index
        /// </summary>
        /// <param name="find"></param>
        /// <returns></returns>
        private (int main, int sub) FindIndex(QueryDatabaseModel find)
        {
            for(int i = 0; i < QueryLists.Count; i++)
                for(int j = 0;  j < QueryLists[i].Count; j++)
                    if (QueryLists[i][j].Equals(find))
                        return (i,j);
            
            return (-1,-1);
        }
        
        /// <summary>
        /// Composes the filter used to search.
        /// </summary>
        /// <returns></returns>
        private FilterDefinition<BsonDocument> ComposeFilter()
        {
            FilterDefinition<BsonDocument> filter = null;            
            for(int j = 0; j < QueryLists.Count; j++)
            {
                FilterDefinition<BsonDocument> rowFilter = null;
                RowEquivilators rowE = QueryLists[j][0].CombineRowsValue;
                int i = 0;
                try
                {                    
                    rowFilter = QueryLists[j][0].ToString();                    
                    for (i = 1; i < QueryLists[j].Count; i++)                
                        rowFilter = CombineFilters(rowFilter, QueryLists[j][i].ToString(), rowE, false);                
                }
                catch (NullReferenceException)
                {
                    throw new QueryImproperLayoutException(j, i, string.Empty);
                }

                if (filter == null)
                    filter = rowFilter;
                else
                {
                    bool isEnd = (j == QueryLists.Count - 1);
                    filter = CombineFilters(filter, rowFilter, QueryLists[j][QueryLists[j].Count - 1].CombineRowsValue, isEnd);
                }
            }

            return filter;
        }

        /// <summary>
        /// Combines fitlers.
        /// </summary>
        /// <param name="filter1"></param>
        /// <param name="filter2"></param>
        /// <param name="equivilator"></param>
        /// <param name="isEnd"></param>
        /// <returns></returns>
        private FilterDefinition<BsonDocument> CombineFilters(FilterDefinition<BsonDocument> filter1, 
            FilterDefinition<BsonDocument> filter2, RowEquivilators equivilator, bool isEnd)
        {
            switch (equivilator)
            {
                case RowEquivilators.And:
                    filter1 &= filter2;
                    break;

                case RowEquivilators.Or:
                    filter1 |= filter2;
                    break;

                case RowEquivilators.None:
                    if(!isEnd)
                        throw new QueryImproperLayoutException(0, 0, (object)RowEquivilators.None);

                    break;
            }

            return filter1;
        }

        #endregion

        #endregion
    }
}
