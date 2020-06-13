namespace CoreApplication.Models.Query
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Text;

    internal class QueryDatabaseArgs
    {
        public ObservableCollection<QueryDatabaseModel> Arg {get; set;}
        public QueryDatabaseArgs(ObservableCollection<QueryDatabaseModel> a) => Arg = a;
    }
}
