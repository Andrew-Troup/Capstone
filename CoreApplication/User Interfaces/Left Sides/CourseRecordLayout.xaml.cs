using CoreApplication.ModelHandlers;
using CoreApplication.ModelHandlers.Records;
using CoreApplication.Models.Records.Class.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CoreApplication.User_Interfaces.Left_Sides
{
    /// <summary>
    /// Interaction logic for CourseRecordLayout.xaml
    /// </summary>
    public partial class CourseRecordLayout : UserControl
    {
        public CourseRecordLayout()
        {
            InitializeComponent();
            Loaded += CourseRecordLayout_Loaded;

            MainHandlers.WindowManager.ClassRecords.UpdateUI += ClassRecords_UpdateUI;
        }

        private void ClassRecords_UpdateUI(object sender, EventArgs e)
        {
            classInformationGrid.DataContext = MainHandlers.WindowManager.ClassRecords.SelectedClass;
        }

        private void CourseRecordLayout_Loaded(object sender, RoutedEventArgs e)
        {
            if (!MainHandlers.WindowManager.IsAdmin)
            {
                editInformationToggleButton.Visibility = Visibility.Collapsed;
                registerStudentButton.DataContext = MainHandlers.WindowManager.ViewHandler.PersonalInformation;
            }
            else
            {
                StudentRecordHandler handler = ((AdminRecordHandler)MainHandlers.WindowManager.ViewHandler).SelectedStudent;
                if (handler != null)
                    registerStudentButton.DataContext = handler.PersonalInformation;
                else
                    registerStudentButton.Visibility = Visibility.Hidden;
            }
            
        }

        private void registerStudentButton_Click(object sender, RoutedEventArgs e)
        {
            CourseRecordInformation information = MainHandlers.WindowManager.ClassRecords.SelectedClass;
            if (information != null)
            {
                string content = string.Empty;
                try
                {
                    
                    if (MainHandlers.WindowManager.IsAdmin)
                    {
                        StudentRecordHandler student = ((AdminRecordHandler)MainHandlers.WindowManager.ViewHandler).SelectedStudent;
                        student.RegisterStudent(information);
                        content = string.Format("Student: {0} {1} was registered for: {2}", student.PersonalInformation.FirstName, student.PersonalInformation.LastName, information.CourseName);
                    }
                    else
                    {
                        StudentRecordHandler student = ((StudentRecordHandler)MainHandlers.WindowManager.ViewHandler);
                        student.RegisterStudent(information);
                        content = string.Format("Student: {0} {1} was registered for: {2}", student.PersonalInformation.FirstName, student.PersonalInformation.LastName, information.CourseName);
                    }
                }catch(Exception em)
                {
                    content = em.Message;
                }

                MessageBox.Show(content, "Ok");
            }
        }
    }
}
