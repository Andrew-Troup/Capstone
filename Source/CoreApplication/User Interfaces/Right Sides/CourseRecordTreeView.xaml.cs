﻿namespace CoreApplication.User_Interfaces.Right_Sides
{
    using CoreApplication.ModelHandlers;
    using CoreApplication.ModelHandlers.Records;
    using CoreApplication.Models.Records.Class.Base;
    using CoreApplication.Models.Records.Class.Support;
    using MongoDB.Bson;
    using MongoDB.Driver;
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
    /// Interaction logic for CourseRecordListView.xaml
    /// </summary>
    public partial class CourseRecordTreeView : UserControl
    {
        public CourseRecordTreeView()
        {
            InitializeComponent();
            Loaded += Page_Loaded;
            MainHandlers.WindowManager.ClassRecords.UpdateCourseTreeViewUI += ClassRecords_UpdateCourseTreeViewUI;
            sortByComboBox.SelectedIndex = 0;
            filterByComboBox.SelectedIndex = 0;
            clearSearchButton.IsEnabled = false;            
        }

        private void ClassRecords_UpdateCourseTreeViewUI(object sender, EventArgs e)
        {
            if (MainHandlers.WindowManager.ClassRecords.SearchedClasses.Count == 0)
                classTreeView.ItemsSource = MainHandlers.WindowManager.ClassRecords.Classes;
            else
                classTreeView.ItemsSource = MainHandlers.WindowManager.ClassRecords.SearchedClasses;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if(MainHandlers.WindowManager.ClassRecords == null)
                MainHandlers.WindowManager.ClassRecords = new ClassRecordHandler(MainHandlers.DatabaseHandler.Database.GetCollection(Common.Models.Collections.Departments).Find(new BsonDocument()).ToEnumerable().ToList());

            ClassRecords_UpdateCourseTreeViewUI(null, null);
        }

        private void classTreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            switch (e.NewValue)
            {
                case DepartmentCourseInformationsPair x:
                    if (x.ClassRecords.Count == 0)
                    {
                        var filter = Builders<BsonDocument>.Filter.Eq("CourseInformation.Department", x.DepartmentAcronym);
                        x.LoadClasses(MainHandlers.DatabaseHandler.Database.GetCollection(Common.Models.Collections.Classes).Find(filter).ToEnumerable().ToList(), MainHandlers.WindowManager.ClassRecords.CurrentSortByType);
                    }
                    break;

                case CourseRecordInformation y:
                    MainHandlers.WindowManager.ClassRecords.SelectedClass = y;
                    break;
            }                       
        }

        private void sortByComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string text = ((ComboBoxItem)((ComboBox)sender).SelectedItem).Content.ToString();
            if(!string.IsNullOrEmpty(text))
                MainHandlers.WindowManager.ClassRecords.SortBy(text);        
        }

        private void searchDatabaseButton_Click(object sender, RoutedEventArgs e)
        {
            string text = searchTextBox.Text;
            if (!string.IsNullOrEmpty(text))
            {
                MainHandlers.WindowManager.ClassRecords.SearchDatabase(text);                
                classTreeView.ItemsSource = MainHandlers.WindowManager.ClassRecords.SearchedClasses;
                clearSearchButton.IsEnabled = true;
            }
        }

        private void clearSearchButton_Click(object sender, RoutedEventArgs e)
        {
            clearSearchButton.IsEnabled = false;
            MainHandlers.WindowManager.ClassRecords.SearchedClasses.Clear();
            searchTextBox.Text = string.Empty;
            ClassRecords_UpdateCourseTreeViewUI(null, null);
        }

        private void filterByComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string text = ((ComboBoxItem)((ComboBox)sender).SelectedItem).Content.ToString();
            if (!string.IsNullOrEmpty(text))
            {
                MainHandlers.WindowManager.ClassRecords.FilterBy(text);
                //ClassRecords_UpdateCourseTreeViewUI(null, null);
            }
        }
    }
}
