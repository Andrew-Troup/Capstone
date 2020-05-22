namespace CoreApplication.User_Interfaces.Right_Sides
{
    using CoreApplication.ModelHandlers.Records;
    using CoreApplication.Models.Records.Class;
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

    /// <summary>
    /// Interaction logic for CourseRecordListView.xaml
    /// </summary>
    public partial class CourseRecordListView : UserControl
    {
        internal CourseRecordInformation CourseRecords { get; set; }

        public CourseRecordListView()
        {
            InitializeComponent();
            Loaded += Page_Loaded;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
