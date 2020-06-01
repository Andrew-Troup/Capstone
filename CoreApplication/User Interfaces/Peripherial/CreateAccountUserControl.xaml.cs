namespace CoreApplication.User_Interfaces.Peripherial
{
    using CoreApplication.ModelHandlers;
    using CoreApplication.Models.Records.Student;
    using MongoDB.Bson;
    using MongoDB.Driver;
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
    using System.Windows.Navigation;
    using System.Windows.Shapes;

    /// <summary>
    /// Interaction logic for CreateAccountUserControl.xaml
    /// </summary>
    public partial class CreateAccountUserControl : UserControl
    {
        public event EventHandler CompletedForm;

        public CreateAccountUserControl()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {            
            string userType = null;
            if (!string.IsNullOrEmpty(UserNameTextBox.Text) && !string.IsNullOrEmpty(passwordTextBox.Text) && !string.IsNullOrEmpty(idTextBox.Text))
            {
                int i = Convert.ToInt32(idTextBox.Text);
                if (i >= 100000)
                    userType = "Student";
                else
                    userType = "Admin";

                BsonDocument temp = new BsonDocument {
                    { "UserName", UserNameTextBox.Text },
                    { "Password", passwordTextBox.Text },
                    { "ID", idTextBox.Text },
                    { "Type", userType }
                };

                MainHandlers.DatabaseHandler.Database.GetCollection(Common.Models.Collections.Users).InsertOne(temp);
                Background = Brushes.Green;
                Thread.Sleep(300);
                Background = Brushes.White;
                CompletedForm?.Invoke(this, null);
                /*}
                else
                {
                    Background = Brushes.Red;
                    Thread.Sleep(300);
                    Background = Brushes.White;
                }*/
            }
        }

        protected void OnCompletedForm(EventArgs e)
        {
            EventHandler handler = CompletedForm;
            if(handler != null)
            {
                handler(this, e);
            }
        }
    }
}
