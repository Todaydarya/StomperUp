using StomperUp.Class;
using StomperUp.Pages.AuthReg;
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
using StomperUp.Windows;
using StomperUp.Model;
using MongoDB.Bson;

namespace StomperUp.Pages.User
{
    /// <summary>
    /// Логика взаимодействия для SettingsPage.xaml
    /// </summary>
    public partial class SettingsPage : Page
    {
        public SettingsPage()
        {
            InitializeComponent();
        }

        private void exitProfile_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (MessageBox.Show("Выйти с аккаунта?", "Внимание", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                NavigationClass.navigate.Navigate(new MainPage());
                CheckClass.idUser = ObjectId.Empty;
            }
        }

        private async void editPassword_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var users = await ConnectionDB.GetUsers();
            var usersAuth = users.FirstOrDefault(user => user._id == CheckClass.idUser);
            GmailVerification gmail = new GmailVerification(usersAuth.email);
            gmail.ShowDialog();
        }
    }
}
