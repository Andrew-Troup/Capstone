namespace CoreApplication.User_Interfaces.Left_Sides
{
    using CoreApplication.ModelHandlers;
    using CoreApplication.ModelHandlers.Records;
    using CoreApplication.Models;
    using CoreApplication.Models.Records.Student_Record;
    using MongoDB.Bson;
    using MongoDB.Driver;
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
    using static CoreApplication.ModelHandlers.Database.DatabaseCommunications;

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
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (!MainHandlers.WindowManager.IsAdmin)
            {
                studentInformationGrid.DataContext = MainHandlers.WindowManager.ViewHandler.PersonalInformation;
                academicInformationGrid.DataContext = ((StudentRecordViewHandler)MainHandlers.WindowManager.ViewHandler).AcademicInformation;
                //foreach (ClassTakenInformation pair in StudentsRecord.AcademicInformation.TakenClasses)
                //    completedClasesListView.Items.Add(pair);

                //foreach (ClassTakenInformation pair in StudentsRecord.AcademicInformation.CurrentClasses)
                //    currentClassesListView.Items.Add(pair);
            }
        }       
    }
}
