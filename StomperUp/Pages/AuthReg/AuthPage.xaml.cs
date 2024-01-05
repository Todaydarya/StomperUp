using StomperUp.Class;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using StomperUp.Pages.AuthReg;
using System.Linq;
using StomperUp.Windows;

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
            /*var password = hashPasswordClass.hashPassword(pbPassword.Password);*/
            var users = await ConnectionDB.GetUsers();
            var usersAuth = users.FirstOrDefault(user => user.email == tbLogin.Text && user.password == pbPassword.Password);
            if (string.IsNullOrEmpty(tbLogin.Text))
            {
                MessageBox.Show("Введите логин");
            }
            else if (string.IsNullOrEmpty(pbPassword.Password))
            {
                MessageBox.Show("Введите пароль");
            }
            else if (usersAuth.email == null)
            {
                if(MessageBox.Show("Такой пользователь не зарегистрирован. Зарегистрироваться?", "Внимание", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    NavigationClass.navigate.Navigate(new RegPage());
                }
            }
            else if(usersAuth.role == "admin")
            {
                ///открытие страницы админа
            }
            else
            {
                ///открытие страниц пользователя
                /*NavigationClass.navigate.Navigate(new MainPage());*/
                /*CheckClass.userId = usersAuth._id;*/
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
