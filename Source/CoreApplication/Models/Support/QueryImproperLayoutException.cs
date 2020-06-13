namespace CoreApplication.Models.Support
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    class QueryImproperLayoutException : Exception
    {
        public int Row { get; private set; }

        public int Column { get; private set; }

        public object Element { get; private set; }

        public QueryImproperLayoutException(int row, int column, object issue) 
            : base($"Row: {row}, Column: {column}, had an error")
        {
            Row = row;
            Column = column;
            Element = issue;
        }
    }
}
