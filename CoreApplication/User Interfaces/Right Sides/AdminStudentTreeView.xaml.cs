﻿namespace CoreApplication.User_Interfaces.Right_Sides
{
    using CoreApplication.ModelHandlers;
    using CoreApplication.ModelHandlers.Records;
    using CoreApplication.Models.Records.Admin;
    using CoreApplication.Models.Records.Admin.Support;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
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
    /// Interaction logic for AdminStudentListView.xaml
    /// </summary>
    public partial class AdminStudentTreeView : UserControl
    {
        public AdminStudentTreeView()
        {
            InitializeComponent();
            Loaded += Page_Loaded;
            if (((AdminRecordHandler)MainHandlers.WindowManager.ViewHandler).SelectedStudent != null)
                SelectItem();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            adminTreeView.ItemsSource = ((AdminRecordHandler)MainHandlers.WindowManager.ViewHandler).Record.Teaching;
        }

        private void adminTreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            try
            {
                ((AdminRecordHandler)MainHandlers.WindowManager.ViewHandler).SelectedStudent = (StudentRecordHandler)e.NewValue;
            }
            catch(Exception)
            {
                // We clicked on a main tree item rather than a student item.
            }
        }

        private void SelectItem()
        {
            
        }
    }
}
