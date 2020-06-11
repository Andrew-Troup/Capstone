namespace CoreApplication
{
    using CoreApplication.ModelHandlers;
    using CoreApplication.ModelHandlers.Records;
    using CoreApplication.User_Interfaces;
    using CoreApplication.User_Interfaces.Left_Sides;
    using CoreApplication.User_Interfaces.Peripherial;
    using CoreApplication.User_Interfaces.Right_Sides;
    using MongoDB.Bson;
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
            BsonValue output;
            if (e.Document.TryGetValue("CloseApplication", out output))
            {
                if (Boolean.Parse(output.AsString))
                {
                    _window.Close();
                    Close();
                }
            }
            else
            {
                MainHandlers.WindowManager = new MainWindowManager(e.Document);
                _window.Close();
                if ((bool)!studentRecordRadioButton.IsChecked)
                    studentRecordRadioButton.IsChecked = true;
                else
                    studentRecordRadioButton_Checked(null, null);
            }
        }

        private void studentRecordRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            leftSideFrame.Children.Clear();
            leftSideFrame.Children.Add(MainHandlers.WindowManager.GetUserControl(UserControlTypes.StudentRecordLayout));

            rightSideFrame.Children.Clear();
            rightSideFrame.Children.Add(MainHandlers.WindowManager.GetUserControl(UserControlTypes.StudentRecordTreeView));
            
        }

        private void courseRecordRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            leftSideFrame.Children.Clear();
            leftSideFrame.Children.Add(MainHandlers.WindowManager.GetUserControl(UserControlTypes.CourseRecordLayout));

            rightSideFrame.Children.Clear();
            rightSideFrame.Children.Add(MainHandlers.WindowManager.GetUserControl(UserControlTypes.CourseRecordTreeView));
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
