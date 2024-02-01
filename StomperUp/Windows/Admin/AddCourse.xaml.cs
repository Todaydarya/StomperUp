using StomperUp.Class;
using StomperUp.Model;
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
using System.Windows.Shapes;

namespace StomperUp.Windows.Admin
{
    /// <summary>
    /// Логика взаимодействия для AddCourse.xaml
    /// </summary>
    public partial class AddCourse : Window
    {
        public AddCourse()
        {
            InitializeComponent();
        }

        private async void btnNext_Click(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrEmpty(tbNameCourse.Text) || cbProgrammingLanguage.SelectedItem == null)
            {
                MessageBox.Show("Заполните поля");
            }
            else
            {
                CourseModel newCourse = new CourseModel
                {
                    nameCourse = tbNameCourse.Text,
                    programmingLanguage = cbProgrammingLanguage.Text,
                    coin = 0,
                    countLesson = 0
                };
                await ConnectionDB.AddCourse(newCourse);
                this.Close();
                AddLesson addLesson = new AddLesson(newCourse._id);
                addLesson.ShowDialog();
            }
        }
    }
}
