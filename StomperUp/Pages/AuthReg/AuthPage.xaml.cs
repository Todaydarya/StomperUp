using StomperUp.Class;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using StomperUp.Pages.AuthReg;
using System.Linq;
using StomperUp.Windows;
using MaterialDesignThemes;
using StomperUp.Pages.User;

namespace StomperUp.Pages.AuthReg
{
    /// <summary>
    /// Логика взаимодействия для AuthPage.xaml
    /// </summary>
    public partial class AuthPage : Page
    {
        AdminWindow adminWindow = new AdminWindow();
        MainWindow userWindow = new MainWindow();
        public AuthPage()
        {
            InitializeComponent();
        }

        private async void btnNext_Click(object sender, RoutedEventArgs e)
        {
            ///переделать хэширования пароля
            var password = HashPassword.hashPassword(pbPassword.Password);
            var users = await ConnectionDB.GetUsers();
            var usersAuth = users.FirstOrDefault(user => user.email == tbEmail.Text && user.password == pbPassword.Password);
            if (string.IsNullOrEmpty(tbEmail.Text))
            {
                SnackbarFour.MessageQueue.Enqueue("Введите логин");
            }
            else if (string.IsNullOrEmpty(pbPassword.Password))
            {
                SnackbarFour.MessageQueue.Enqueue("Введите пароль");
            }
            else if (usersAuth == null)
            {
                if (MessageBox.Show("Такой пользователь не зарегистрирован. Зарегистрироваться?", "Внимание", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    NavigationClass.navigate.Navigate(new RegPage());
                }
            }
            else if (usersAuth.role == "admin")
            {
                adminWindow.Show();
                userWindow.Close();
            }
            else
            {
                NavigationClass.navigate.Navigate(new MainPage());
                CheckClass.idUser = usersAuth._id.ToString();
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
