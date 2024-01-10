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
        }

        private async void btnNext_Click(object sender, RoutedEventArgs e)
        {
            var users = await ConnectionDB.GetUsers();
            var usersAuth = users.FirstOrDefault(user => user.email == tbEmail.Text);
            bool isPasswordCorrect = BCrypt.Net.BCrypt.Verify(pbPassword.ToString(), usersAuth.password);

            if (string.IsNullOrEmpty(tbEmail.Text))
            {
                SnackbarFour.MessageQueue.Enqueue("Введите логин");
            }
            else if (string.IsNullOrEmpty(pbPassword.Password))
            {
                SnackbarFour.MessageQueue.Enqueue("Введите пароль");
            }
            do
            {

            }
            while (isPasswordCorrect);
            MessageBox.Show($"Авторизация прошла {usersAuth.firstName}");
            /*if (isPasswordCorrect)
            {
                
            }*/
            /*else
            {
                if (MessageBox.Show("Такой пользователь не зарегистрирован. Зарегистрироваться?", "Внимание", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    NavigationClass.navigate.Navigate(new RegPage());
                }
            }*/
        }

        private void NavigateReg_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            NavigationClass.navigate.Navigate(new RegPage());
        }

        private void reg_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            NavigationClass.navigate.Navigate(new RegPage());
        }

        private void tbLogin_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

        }

        private void Password_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            GmailVerification gmail = new GmailVerification();
            gmail.ShowDialog();
        }
    }
}
