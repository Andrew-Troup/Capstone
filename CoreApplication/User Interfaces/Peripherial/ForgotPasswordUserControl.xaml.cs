namespace CoreApplication.User_Interfaces.Peripherial
{
    using Common.Models;
    using CoreApplication.ModelHandlers;
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
    /// Interaction logic for ForgotPasswordUserControl.xaml
    /// </summary>
    public partial class ForgotPasswordUserControl : UserControl
    {
        public event EventHandler CompletedForm;

        public ForgotPasswordUserControl()
        {
            InitializeComponent();
        }

        private void searchForPasswordButton_Click(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(UserNameTextBox.Text) && !String.IsNullOrEmpty(emailTextBox.Text) && !String.IsNullOrEmpty(studentIDTextBox.Text))
            {
                FilterDefinition<BsonDocument> filter = Builders<BsonDocument>.Filter.And(
                        Builders<BsonDocument>.Filter.Eq("UserName", UserNameTextBox.Text),
                        Builders<BsonDocument>.Filter.Eq("Email", emailTextBox.Text),
                        Builders<BsonDocument>.Filter.Eq("ID", studentIDTextBox.Text)
                        );

                BsonDocument doc = MainHandlers.DatabaseHandler.Database.GetCollection(Collections.Users).Find(filter).FirstOrDefault();

                string text;
                if (doc == null)
                   text = "No Password Found.";
                else
                {
                    text = $"Password Found: {doc[1].AsString}";
                    OnCompletedForm(null);
                }

                MessageBox.Show(text, "Ok");
            }
        }

        protected void OnCompletedForm(EventArgs e)
        {
            CompletedForm?.Invoke(this, null);
        }
    }
}
