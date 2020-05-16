namespace CoreApplication
{
    using CoreApplication.ModelHandlers.Database;
    using CoreApplication.ModelHandlers.Records;
    using CoreApplication.User_Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    { 

        public MainWindow()
        {
            InitializeComponent();
            //LoginWindow window = new LoginWindow();
            //window.Show();

            studentRecordRadioButton.IsChecked = true;

        }

        private void studentRecordRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            leftSideFrame.Children.Clear();
            StudentRecordLayout record = new StudentRecordLayout();
            leftSideFrame.Children.Add(record);

            rightSideFrame.Children.Clear();
            StudentRecordListView listView = new StudentRecordListView();
            rightSideFrame.Children.Add(listView);
        }

        private void classRecordRadioButton_Checked(object sender, RoutedEventArgs e)
        {
        }

        private void courseRecordRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            leftSideFrame.Children.Clear();
            CourseRecordLayout record = new CourseRecordLayout();
            leftSideFrame.Children.Add(record);

            rightSideFrame.Children.Clear();
            CourseRecordListView listView = new CourseRecordListView();
            rightSideFrame.Children.Add(listView);
        }
    }
}
