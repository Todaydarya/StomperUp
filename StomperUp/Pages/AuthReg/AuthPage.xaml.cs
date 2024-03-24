using StomperUp.Class;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using StomperUp.Pages.AuthReg;
using System.Linq;
using StomperUp.Windows;
using MaterialDesignThemes;
using StomperUp.Pages.User;
using BCrypt.Net;
using StomperUp.Model;
using System.Collections.Generic;
using System;
using System.Configuration;

namespace StomperUp.Pages.AuthReg
{
    /// <summary>
    /// Логика взаимодействия для AuthPage.xaml
    /// </summary>
    public partial class AuthPage : Page
    {
        public AuthPage()
        {
            InitializeComponent();
            CheckClass.isAuthPage = true;
            tbEmail.Focus();
        }
                
        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            Authorization();
        }

        private void pbPassword_KeyDown(object sender, KeyEventArgs e)
        {
            EnterCheck(e);
        }


        private void tbEmail_KeyDown(object sender, KeyEventArgs e)
        {
            EnterCheck(e);
        }

        public void EnterCheck(KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Authorization();
            }
        }

        private void NavigateReg_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            NavigationClass.navigate.Navigate(new RegPage());
        }

        private void reg_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            NavigationClass.navigate.Navigate(new RegPage());
        }

        private void Password_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            AuthClose();
            CheckClass.isAuthPage = false;
            GmailVerification gmail = new GmailVerification(null);
            gmail.ShowDialog();
            
        }


        private void tbLogin_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

        }

        

        
        public async void Authorization()
        {
            if (string.IsNullOrEmpty(tbEmail.Text))
            {
                SnackbarFour.MessageQueue.Enqueue("Введите логин");
            }
            else if (string.IsNullOrEmpty(pbPassword.Password))
            {
                SnackbarFour.MessageQueue.Enqueue("Введите пароль");
            }
            else
            {
                var loadingCard = FindResource("LoadingCard") as FrameworkElement;
                loadingCard.Visibility = Visibility.Visible;
                List<UserModel> users = await ConnectionDB.GetUsers();
                var usersAuth = users.FirstOrDefault(user => user.email == tbEmail.Text);
                if (usersAuth == null)
                {
                    if (MessageBox.Show("Такой пользователь не зарегистрирован. Зарегистрироваться?", "Внимание", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        NavigationClass.navigate.Navigate(new RegPage());
                    }
                    loadingCard.Visibility = Visibility.Collapsed;
                }
                else
                {
                    bool isPasswordCorrect = BCrypt.Net.BCrypt.Verify(pbPassword.ToString(), usersAuth.password);
                    do
                    {

                    }
                    while (isPasswordCorrect);
                    loadingCard.Visibility = Visibility.Collapsed;
                    if (usersAuth.role == "admin")
                    {
                        AuthClose();
                        AdminWindow w2 = new AdminWindow();

                        w2.Closed += new EventHandler(w2_Closed);
                        (Application.Current.MainWindow as MainWindow).Hide();
                        w2.Show();
                    }
                    else
                    {
                        CheckClass.idUser = usersAuth._id;
                        Properties.Settings.Default.idUser = usersAuth._id.ToString();
                        Properties.Settings.Default.Save();
                        CheckClass.isAuthPage = false;
                        MessageBox.Show($"Добро пожаловать, {usersAuth.firstName}");
                        AuthClose();
                    }
                }
            }
          }
        void AuthClose()
        {
            foreach (Window window in App.Current.Windows)
            {
                if (window.IsActive == true)
                {
                    window.Close();
                }
            }
        }
        void w2_Closed(object sender, EventArgs e)
        {
            (Application.Current.MainWindow as MainWindow).Close();
            // или (Application.Current.MainWindow as MainWindow).Show(); если нужно продолжить работу
        }
    }
}
