using StomperUp.Class;
using System;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Windows;
using System.Windows.Input;
using StomperUp.Pages.User;
using StomperUp.Model;
using MongoDB.Driver;

namespace StomperUp.Windows
{
    /// <summary>
    /// Логика взаимодействия для GmailVerification.xaml
    /// </summary>
    public partial class GmailVerification : Window
    {
        string randomCode;
        public GmailVerification(string user)
        {
            InitializeComponent();
            
            if(user != null)
            {
                spEmail.Visibility = Visibility.Collapsed;
                spCheck.Visibility = Visibility.Visible;
                Mail(user);
            }
            else
            {
                spEmail.Visibility = Visibility.Visible;
                spCheck.Visibility = Visibility.Collapsed;
            }
        }

        private async void btnCheckEmail_Click(object sender, RoutedEventArgs e)
        {
            var user = await ConnectionDB.GetUsers();
            var usersAuth = user.FirstOrDefault(u => u.email == tbEmail.Text);
            if (string.IsNullOrEmpty(tbEmail.Text))
                MessageBox.Show("Вы не ввели почту");
            else if (usersAuth == null)
                MessageBox.Show("Эта почта не зарегистрирована в системе");
            else
            {
                Mail(usersAuth.email);
            }
        }

        private void btnCheckCode_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(tbCode.Text))
                MessageBox.Show("Введите код отправленный вам на почту");
            else if (tbCode.Text == randomCode.ToString())
            {
                spPassword.Visibility = Visibility.Visible;
                spEmail.Visibility = Visibility.Collapsed;
                spCheck.Visibility = Visibility.Collapsed;
            }
            else
            {
                MessageBox.Show("Неверно введён код. Повторите попытку");
            }
        }

        public void Mail(string emailUser)
        {
            string to, from, pass, mail;
            Random rand = new Random();
            randomCode = (rand.Next(999999)).ToString();

            to = emailUser;
            from = "Todaydarya@gmail.com";
            mail = $"Вас приветствует компания Stomper.\n Спасибо что пользуетесь нашими услугами.\n Ваш код восстановления: " + randomCode;
            pass = "fabo rnly cfam nnld";

            MailMessage message = new MailMessage();
            message.To.Add(to);
            message.From = new MailAddress(from);
            message.Body = mail;
            message.Subject = "StomperUp";

            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");
            smtpClient.EnableSsl = true;
            smtpClient.Port = 587;
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.Credentials = new NetworkCredential(from, pass);

            try
            {
                smtpClient.Send(message);
                MessageBox.Show("Код отправлен вам на почту", "Email", MessageBoxButton.OK, MessageBoxImage.Information);
                spCheck.Visibility = Visibility.Visible;
                spEmail.Visibility = Visibility.Collapsed;
            }
            catch (SmtpFailedRecipientsException)
            {
                MessageBox.Show("Адрес почты не найден. Возможно, адрес недействителен или почтовый сервер недоступен.", "Email", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Не удалось отправить код." + ex.Message, "Email", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void btnPassword_Click(object sender, RoutedEventArgs e)
        {
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(tbPassword.Text);

            var user = await ConnectionDB.GetUsers();
            var userToUpdate = user.FirstOrDefault(u => u._id.ToString() == CheckClass.idUser);
            var userToUpdateEmail = user.FirstOrDefault(u => u.email == tbEmail.Text);

            if (user.FirstOrDefault(u => u.email == tbEmail.Text) == null)
            {
                if (userToUpdate != null)
                {
                    UserModel updatedUser = new UserModel
                    {
                        password = hashedPassword
                    };
                    await ConnectionDB.UpdateUserPassword(CheckClass.idUser, updatedUser);
                    MessageBox.Show("Пользователь успешно обновлен!");
                    CheckClass.idUser = userToUpdate._id.ToString();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Пользователь не найден.");
                }
            }
            else
            {
                UserModel updatedUser = new UserModel
                {
                    password = hashedPassword
                };
                await ConnectionDB.UpdateUserPassword(userToUpdateEmail._id.ToString(), updatedUser);
                MessageBox.Show("Пользователь успешно обновлен!");
                CheckClass.idUser = userToUpdateEmail._id.ToString();
                this.Close();
            }
        }
    }
}
