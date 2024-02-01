using MongoDB.Bson;
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
    /// Логика взаимодействия для AddLesson.xaml
    /// </summary>
    public partial class AddLesson : Window
    {
        ObjectId idCourse;
        public AddLesson(ObjectId course)
        {
            InitializeComponent();
            idCourse = course;
        }

        private async void btnNext_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(tbNameLesson.Text) || string.IsNullOrEmpty(tbCoin.Text))
            {
                MessageBox.Show("Заполните поля");
            }
            else
            {
                LessonsModel newLesson = new LessonsModel
                {
                    nameLessons = tbNameLesson.Text,
                    coin = int.Parse(tbCoin.Text),
                    idCourse = idCourse
                };
                await ConnectionDB.AddLesson(newLesson);
                this.Close();
            }
        }
    }
}
