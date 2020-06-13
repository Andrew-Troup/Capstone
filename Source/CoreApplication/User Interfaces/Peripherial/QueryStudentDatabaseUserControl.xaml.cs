namespace CoreApplication.User_Interfaces.Peripherial
{
    using CoreApplication.ModelHandlers;
    using CoreApplication.ModelHandlers.Query;
    using CoreApplication.ModelHandlers.Records;
    using CoreApplication.Models;
    using CoreApplication.Models.Query;
    using CoreApplication.Models.Support;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Documents;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using System.Windows.Navigation;
    using System.Windows.Shapes;

    /// <summary>
    /// Interaction logic for QueryStudentDatabaseUserControl.xaml
    /// </summary>
    public partial class QueryStudentDatabaseUserControl : Window
    {
        #region Variables

        #endregion

        public QueryStudentDatabaseUserControl()
        {
            InitializeComponent();
            Title = "Query Database";
            if(MainHandlers.WindowManager.QueryModel == null)
                MainHandlers.WindowManager.QueryModel = new QueryModelViewHandler();

            queryListView.ItemsSource = MainHandlers.WindowManager.QueryModel.QueryItems;            
        }

        #region Methods
        #region Private

        #region Event Handlers

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListView list = ((ListView)sender);
            MainHandlers.WindowManager.QueryModel.SelectedIndexes = list.SelectedItems.Cast<QueryDatabaseModel>().ToList();
        }

        #region Insert

        private void InsertAfter_MenuItemClick(object sender, EventArgs e)
        {
            MainHandlers.WindowManager.QueryModel.InsertQuery(Models.InsertLocation.After);
            UpdateForm();
        }

        private void InsertBefore_MenuItemClick(object sender, EventArgs e)
        {
            MainHandlers.WindowManager.QueryModel.InsertQuery(Models.InsertLocation.Before);
            UpdateForm();
        }

        private void InsertInto_MenuItemClick(object sender, EventArgs e)
        {
            MainHandlers.WindowManager.QueryModel.InsertQuery(Models.InsertLocation.Into);
            UpdateForm();
        }

        #endregion

        private void DeleteRow_MenuItemClick(object sender, EventArgs e)
        {
            MainHandlers.WindowManager.QueryModel.DeleteQuery();
            UpdateForm();
        }

        #region Form Closers

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            MainHandlers.WindowManager.QueryModel = null;
            Close();
        }

        private void applyButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ((AdminRecordHandler)MainHandlers.WindowManager.ViewHandler).Record.AddQuery(MainHandlers.WindowManager.QueryModel.PerformQuery());
                Close();
            }
            catch(QueryImproperLayoutException a)
            {
                FindIssue(a);
            }
        }

        #endregion

        #endregion

        /// <summary>
        /// Updates the form with the UI elements.
        /// </summary>
        private void UpdateForm()
        {
            queryListView.ItemsSource = MainHandlers.WindowManager.QueryModel.QueryItems;
        }

        private void FindIssue(QueryImproperLayoutException e)
        {
            string issue = string.Empty;
            int index = (e.Row + 1) * (e.Column + 1);
            switch (e.Element)
            {
                case string data:
                    issue = $"index: {index} cannot contain an empty field for the search parameter.";
                    break;

                case RowEquivilators rowE:
                    issue = $"index: {index} cannot contain a None combining value unless it is the last item.";
                    break;
            }

            MessageBox.Show(issue, "Query UI error", MessageBoxButton.OK);
        }

        #endregion

        #endregion
    }
}
