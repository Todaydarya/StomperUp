using MaterialDesignThemes.Wpf;
using StomperUp.Class;
using StomperUp.Model;
using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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

        private async void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (cbUserAndCourse.SelectedIndex == 1)
            {
                var users = await ConnectionDB.GetUsers();
                string[] searchTerms = tbSearch.Text.ToLower().Split(' ');

                items.ItemsSource = users.ToList().Where(p => searchTerms.Any(term => p.firstName.ToLower().Contains(term)
                                                                                    || p.surName.ToLower().Contains(term)
                                                                                    || p.email.ToLower().Contains(term)
                                                                                    || p.phone.ToLower().Contains(term)
                                                                                    || p.role.ToLower().Contains(term))).ToList();
            }
            else if (cbUserAndCourse.SelectedIndex == 0)
            {

            }
        }

        private void ToggleButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }

        private void editUser_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
