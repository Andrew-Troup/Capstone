using CoreApplication.ModelHandlers;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CoreApplication.User_Interfaces.Peripherial
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public delegate void LoginHandled(object sender, CustomEventArgs e);
        public event LoginHandled FormCompleted;

        private const int GWL_STYLE = -16;
        private const int WS_SYSMENU = 0x80000;
        [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);


        public LoginWindow()
        {
            InitializeComponent();
            Loaded += LoginWindow_Loaded;
        }

        private void LoginWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // Code to remove close box from window
            var hwnd = new System.Windows.Interop.WindowInteropHelper(this).Handle;
            SetWindowLong(hwnd, GWL_STYLE, GetWindowLong(hwnd, GWL_STYLE) & ~WS_SYSMENU);
        }

        private void ForgotPassword_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            ForgotPasswordUserControl control = new ForgotPasswordUserControl();
            userNameTextBox.IsEnabled = false;
            passwordTextBox.IsEnabled = false;
            control.CompletedForm += CompletedForm;
            subContentFrame.Children.Add(control);
        }

        private void CreateAccount_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            CreateAccountUserControl control = new CreateAccountUserControl();
            userNameTextBox.IsEnabled = false;
            passwordTextBox.IsEnabled = false;
            control.CompletedForm += CompletedForm;
            subContentFrame.Children.Add(control);
        }

        private void CompletedForm(object sender, EventArgs e)
        {
            subContentFrame.Children.Clear();
            userNameTextBox.IsEnabled = true;
            passwordTextBox.IsEnabled = true;
        }

        private void loginButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BsonDocument doc = MainHandlers.DatabaseHandler.GetAccountFromLogin(userNameTextBox.Text, passwordTextBox.Text);
                OnCompletedForm(doc);
            }
            catch(Exception ex)
            {
                Background = Brushes.Red;
                Thread.Sleep(300);
                Background = Brushes.White;
            }
        }

        protected void OnCompletedForm(BsonDocument e)
        {
            FormCompleted?.Invoke(this, new CustomEventArgs(e));
        }
    }

    public class CustomEventArgs
    {
        public BsonDocument Document { get; set; }
        public CustomEventArgs(BsonDocument s) => Document = s;
    }
}
