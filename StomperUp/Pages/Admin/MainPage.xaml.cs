using StomperUp.Class;
using StomperUp.Model;
using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Controls;

namespace StomperUp.Pages.Admin
{
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
            UserDB();
        }

        public async void UserDB()
        {
            var users = await ConnectionDB.GetUsers();
            items.ItemsSource = users;
            DataContext = users;
        }
    }
}
