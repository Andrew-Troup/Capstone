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
using System.Windows.Shapes;

namespace CoreApplication.User_Interfaces.Peripherial
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
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
    }
}
