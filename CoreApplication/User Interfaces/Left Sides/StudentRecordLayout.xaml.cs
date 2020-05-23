namespace CoreApplication.User_Interfaces.Left_Sides
{
    using CoreApplication.ModelHandlers;
    using CoreApplication.ModelHandlers.Records;
    using CoreApplication.Models;
    using CoreApplication.Models.Records.Student;
    using MongoDB.Bson;
    using MongoDB.Driver;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Data;
    using System.Windows.Documents;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using System.Windows.Navigation;
    using System.Windows.Shapes;

    /// <summary>
    /// Interaction logic for StudentRecordLayout.xaml
    /// </summary>
    public partial class StudentRecordLayout : UserControl
    {
        public StudentRecordLayout()
        {
            InitializeComponent();

            if (!MainHandlers.WindowManager.IsAdmin)
            {
                completeClassExpander.Visibility = Visibility.Collapsed;
            }

            Loaded += Page_Loaded;
            if(MainHandlers.WindowManager.IsAdmin)
                ((AdminRecordHandler)MainHandlers.WindowManager.ViewHandler).UpdateUI += StudentRecordLayout_UpdateUI;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (!MainHandlers.WindowManager.IsAdmin)
            {
                studentInformationGrid.DataContext = MainHandlers.WindowManager.ViewHandler.PersonalInformation;
                academicInformationGrid.DataContext = ((StudentRecordHandler)MainHandlers.WindowManager.ViewHandler).AcademicInformation;
            }
        }

        private void StudentRecordLayout_UpdateUI(object sender, EventArgs e)
        {
            StudentRecordHandler handler = ((AdminRecordHandler)MainHandlers.WindowManager.ViewHandler).SelectedStudent;
            studentInformationGrid.DataContext = handler.PersonalInformation;
            academicInformationGrid.DataContext = handler.AcademicInformation;
            completedClasesListView.ItemsSource = handler.AcademicInformation.TakenClasses;
            currentClassesListView.ItemsSource = handler.AcademicInformation.CurrentClasses;

        }

        private void ToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            if ((((ToggleButton)sender).IsChecked == false) && (MainHandlers.WindowManager.ViewHandler.UpdateInformation.Count > 0))
            {
                MainHandlers.DatabaseHandler.UpdateDatabase(MainHandlers.WindowManager.ViewHandler.UpdateInformation, studentIdTextBox.Text);
                MainHandlers.WindowManager.ViewHandler.UpdateInformation.Clear();
            }
        }
    }
}
