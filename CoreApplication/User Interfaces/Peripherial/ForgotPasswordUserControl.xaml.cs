namespace CoreApplication.User_Interfaces.Peripherial
{
    using Common.Models;
    using CoreApplication.ModelHandlers;
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
            BsonDocument doc = MainHandlers.DatabaseHandler.Database.GetCollection(Collections.Users).Find(
                  Builders<BsonDocument>.Filter.Eq("UserName", UserNameTextBox.Text)
                & Builders<BsonDocument>.Filter.Eq("Email", emailTextBox.Text)
                & Builders<BsonDocument>.Filter.Eq("StudentID", studentIDTextBox.Text)).FirstOrDefault();

            if (doc == null)
                passTextBlock.Text = "No Password Found.";
            else
                passTextBlock.Text = doc[1].AsString;

        }
    }
}
