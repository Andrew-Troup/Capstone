﻿using CoreApplication.ModelHandlers;
using CoreApplication.ModelHandlers.Records;
using CoreApplication.Models.Records.Student;
using System;
using System.Collections.Generic;
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

namespace CoreApplication.User_Interfaces.Right_Sides
{
    /// <summary>
    /// Interaction logic for StudentRecordListView.xaml
    /// </summary>
    public partial class StudentRecordListView : UserControl
    {
        public StudentRecordListView()
        {
            InitializeComponent();
            Loaded += Page_Loaded;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            studentClassesTreeView.ItemsSource = ((StudentRecordHandler)MainHandlers.WindowManager.ViewHandler).AcademicInformation.AllClasses;
        }
    }
}
