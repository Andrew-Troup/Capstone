namespace CoreApplication.User_Interfaces.Peripherial
{
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
    using static CoreApplication.ModelHandlers.Database.DatabaseCommunications;

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
            /*BsonDocument doc = MainHandlers.Database.GetCollection(Collections.Students).Find(
                    Builders<BsonDocument>.Filter.Eq("Email", emailTextBox.Text)
                    & Builders<BsonDocument>.Filter.Eq("StudentID", studentIDTextBox.Text)).FirstOrDefault();*/

            //if(doc != null)
            //{
                var writeConcern = WriteConcern.WMajority.With(wTimeout: TimeSpan.FromMilliseconds(5000));
                BsonDocument temp = new BsonDocument { 
                    { "createUser", "Andrew" }, 
                    { "pwd", "Andrew" },
                    {"customData", new BsonDocument("ID", "1234") },
                    { "roles", new BsonArray{ 
                        new BsonDocument 
                        { 
                            { "role", "readWrite" }, 
                            { "db", "Students" } 
                        },
                        new BsonDocument
                        {
                            { "role", "read" },
                            { "db", "Classes" }
                        }
                    } },
                    {"writeConcern", writeConcern.ToBsonDocument() } 
                };

                MainHandlers.Database.RecordsDatabase.RunCommand<BsonDocument>(temp);
                Background = Brushes.Green;
                Thread.Sleep(300);
                Background = Brushes.White;              
            /*}
            else
            {
                Background = Brushes.Red;
                Thread.Sleep(300);
                Background = Brushes.White;
            }*/
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
