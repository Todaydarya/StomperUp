using MaterialDesignThemes.Wpf;
using StomperUp.Class;
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

namespace StomperUp.Pages.User
{
    /// <summary>
    /// Логика взаимодействия для ProfilePage.xaml
    /// </summary>
    public partial class ProfilePage : Page
    {
        public ProfilePage()
        {
            InitializeComponent();
            UserDB();
        }
        public async void UserDB()
        {
            var users = await ConnectionDB.GetUsers();
            var userSelect = users.Where(p => p._id.ToString() == CheckClass.idUser);
            DataContext = userSelect;
        }
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            btnEdit.Visibility = Visibility.Collapsed;
            btnSave.Visibility = Visibility.Visible;
            spEditProfile.IsEnabled = true;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            //Обработчик на сохранение данных
        }

        private void TextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void GitHub_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
