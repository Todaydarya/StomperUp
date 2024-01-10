using MaterialDesignThemes.Wpf;
using MongoDB.Bson;
using MongoDB.Driver;
using StomperUp.Class;
using StomperUp.Model;
using StomperUp.Pages.User;
using StomperUp.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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

namespace StomperUp.Pages.AuthReg
{
    /// <summary>
    /// Логика взаимодействия для RegPage.xaml
    /// </summary>
    public partial class RegPage : Page
    {
        public RegPage()
        {
            InitializeComponent();
        }

        private void auth_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            NavigationClass.navigate.Navigate(new AuthPage());
        }

        private void tbLogin_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

        }

        private void tbEmail_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

        }

        private async void btnNext_Click(object sender, RoutedEventArgs e)
        {
            loading.Visibility = Visibility.Visible;
            if (string.IsNullOrEmpty(tbFIO.Text))
            {
                SnackbarFour.MessageQueue.Enqueue("Введите ФИО");
            }
            else if (string.IsNullOrEmpty(tbEmail.Text))
            {
                SnackbarFour.MessageQueue.Enqueue("Введите почту");
            }
            else if (string.IsNullOrEmpty(pbPassword.Password))
            {
                SnackbarFour.MessageQueue.Enqueue("Введите пароль");
            }
            else if (string.IsNullOrEmpty(pbPasswordCheck.Password))
            {
                SnackbarFour.MessageQueue.Enqueue("Повторите пароль");
            }
            else if (cbUse.IsChecked == false)
            {
                SnackbarFour.MessageQueue.Enqueue("Отметьте условия пользования");
            }
            else if(pbPassword.Password != pbPasswordCheck.Password)
            {
                SnackbarFour.MessageQueue.Enqueue("Пароли не совпадают");
            }
            else
            {
                var users = await ConnectionDB.GetUsers();
                var usersAuth = users.FirstOrDefault(user => user.email == tbEmail.Text);
                if (usersAuth != null)
                {
                    if (MessageBox.Show("Такой пользователь уже зарегистрирован. Восстановить аккаунт?", "Внимание", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        GmailVerification mail = new GmailVerification();
                        mail.ShowDialog();
                    }
                }
                else
                {
                    string hashedPassword = BCrypt.Net.BCrypt.HashPassword(pbPassword.Password);
                    string[] searchTerms = tbFIO.Text.ToLower().Split(' ');
                    UserModel newUser = new UserModel
                    {
                        firstName = searchTerms[1],
                        surName = searchTerms[0],
                        phone = "",
                        email = tbEmail.Text,
                        password = hashedPassword,
                        picturePath = "john.doe@example.com",
                        role = "",
                        driverInfo = "",
                        adminInfo = "",
                        createdAt = DateTime.Now
                    };
                    try
                    {
                        await ConnectionDB.AddUser(newUser);
                        loading.Visibility = Visibility.Collapsed;
                        MessageBox.Show($"Регистрацияя прошла успешно"); 
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
            }


            //Сделать добавление пользователей
            /*else if (usersAuth == null)
            {
                if (MessageBox.Show("Такой пользователь уже зарегистрирован. Восстановить аккаунт?", "Внимание", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    GmailVerification mail = new GmailVerification();
                    mail.ShowDialog();
                }
            }
            else
            {
                try
                {
                    using (var dbContext = new CourseEntities())
                    {
                        newUser.login = tbLogin.Text;
                        newUser.email = tbEmail.Text;
                        newUser.password = hashPasswordClass.hashPassword(tbPassword.Password);
                        dbContext.Users.Add(newUser);
                        dbContext.SaveChanges();

                        MessageBox.Show("Добро пожаловать новый пользователь!", "Удачно", MessageBoxButton.OK, MessageBoxImage.Information);
                        CheckClass.userId = newUser.idUser;
                        NavigationClass.mainFrame.Navigate(new MainPage());
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Ошибка при сохранении");
                }
            }*/
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if(MessageBox.Show("Открыть страницу условий пользования?","Внимание", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Process.Start(new ProcessStartInfo("https://www.figma.com/file/ApoNjh8f2Ba8UViZjKAaIM/%D0%94%D0%B8%D0%BF%D0%BB%D0%BE%D0%BC?type=design&node-id=32-220&mode=design&t=4cQIkCV6wc0H2xqb-0"));
            }
        }
    }
}
