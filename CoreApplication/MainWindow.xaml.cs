namespace CoreApplication
{
    using CoreApplication.ModelHandlers;
    using CoreApplication.ModelHandlers.Records;
    using CoreApplication.User_Interfaces;
    using CoreApplication.User_Interfaces.Left_Sides;
    using CoreApplication.User_Interfaces.Peripherial;
    using CoreApplication.User_Interfaces.Right_Sides;
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
        LoginWindow _window;
        public MainWindow()
        {
            InitializeComponent();
            Show();
            ShowLoginForm();
            
        }

        private void LoginWindow_FormCompleted(object sender, CustomEventArgs e)
        {
            MainHandlers.WindowManager = new MainWindowManager(e.Document);
            _window.Close();
            if((bool) !studentRecordRadioButton.IsChecked)
                studentRecordRadioButton.IsChecked = true;
            else
                studentRecordRadioButton_Checked(null, null);
        }

        private void studentRecordRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            leftSideFrame.Children.Clear();
            StudentRecordLayout record = new StudentRecordLayout();
            leftSideFrame.Children.Add(record);

            rightSideFrame.Children.Clear();
            if (!MainHandlers.WindowManager.IsAdmin)
            {                               
                StudentRecordTreeView listView = new StudentRecordTreeView();
                rightSideFrame.Children.Add(listView);
            }
            else
            {
                AdminStudentTreeView listView = new AdminStudentTreeView();
                rightSideFrame.Children.Add(listView);
            }
            
        }

        private void courseRecordRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            leftSideFrame.Children.Clear();
            CourseRecordLayout record = new CourseRecordLayout();
            leftSideFrame.Children.Add(record);

            rightSideFrame.Children.Clear();
            CourseRecordTreeView listView = new CourseRecordTreeView();
            rightSideFrame.Children.Add(listView);
        }

        private void logoutButton_Click(object sender, RoutedEventArgs e)
        {
            leftSideFrame.Children.Clear();
            rightSideFrame.Children.Clear();
            ShowLoginForm();            
        }

        private void ShowLoginForm()
        {
            _window = new LoginWindow();
            _window.FormCompleted += LoginWindow_FormCompleted;
            _window.ShowDialog();
        }
    }
}
