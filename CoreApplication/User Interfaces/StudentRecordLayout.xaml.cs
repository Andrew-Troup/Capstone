namespace CoreApplication.User_Interfaces
{
    using CoreApplication.ModelHandlers;
    using CoreApplication.ModelHandlers.Records;
    using CoreApplication.Models.Student_Record;
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

        private string[] majors = { "Computer Science", "History", "Literature", "Physics", "Computer Engineering"};
        internal StudentRecord StudentsRecord { get; set; }

        public StudentRecordLayout()
        {
            InitializeComponent();

            if (!MainHandlers.WindowManager.IsAdmin)
            {
                completedClasesListView.Visibility = Visibility.Collapsed;
                editAcademicInformationToggleButton.Visibility = Visibility.Collapsed;
            }

            var filter = Builders<BsonDocument>.Filter.Eq("StudentID", "1236433");
            StudentsRecord = new StudentRecord(MainHandlers.Database.GetCollection(Collections.Students).Find(filter).FirstOrDefault());
            Loaded += Page_Loaded;

            foreach (string major in majors)
                majorComboBox.Items.Add(major);
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            studentInformationGrid.DataContext = StudentsRecord.PersonalInformation;
            academicInformationGrid.DataContext = StudentsRecord.AcademicInformation;
            foreach (ClassTakenInformation pair in StudentsRecord.AcademicInformation.TakenClasses)
                completedClasesListView.Items.Add(pair);
        }       
    }
}
