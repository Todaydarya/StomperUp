using StomperUp.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace StomperUp.Windows
{
    /// <summary>
    /// Логика взаимодействия для GmailVerification.xaml
    /// </summary>
    public partial class GmailVerification : Window
    {
        string randomCode;
        /*private NetworkChecker networkChecker;*/
        /*private bool IsInternetAvailable()
        {
            *//*return networkChecker.IsInternetAvailable();*//*
        }*/
        public GmailVerification()
        {
            InitializeComponent();
            /*networkChecker = new NetworkChecker();*/
        }

        private void btnCheckEmail_Click(object sender, RoutedEventArgs e)
        {
        }

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void btnCheckCode_Click(object sender, RoutedEventArgs e)
        {

        }
        /*var userID = GetDBContext.GetContext().Users.FirstOrDefault();
if (string.IsNullOrEmpty(tbEmail.Text))
MessageBox.Show("Вы не ввели почту");
else if (userID.email != tbEmail.Text)
MessageBox.Show("Эта почта не зарегистрирована в системе");
else if (!IsInternetAvailable())
MessageBox.Show("Отсутствует подключение к интернету. Проверьте соединение.");
else
{
CheckClass.userId = userID.idUser;
string to, from, pass, mail;
Random rand = new Random();
randomCode = (rand.Next(999999)).ToString();

to = tbEmail.Text;
from = "Todaydarya@gmail.com";
mail = "Ваш код для восстановления: " + randomCode;
pass = "fabo rnly cfam nnld";

MailMessage message = new MailMessage();
message.To.Add(to);
message.From = new MailAddress(from);
message.Body = mail;
message.Subject = "Ваш помощник для восстановления пароля";

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
}

private void btnCheckCode_Click(object sender, RoutedEventArgs e)
{
if (string.IsNullOrEmpty(tbCode.Text))
MessageBox.Show("Введите код отправленный вам на почту");
else if (tbCode.Text == randomCode.ToString())
{
this.Close();
NavigationClass.navigate.Navigate(new MainPage());
}
else
{
MessageBox.Show("Неверно введён код. Повторите попытку");
}
}

private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
{
if (MessageBox.Show("Если код не пришёл, то проверьте правильность введённой почты. Попробовать снова?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes) == MessageBoxResult.Yes)
{
spCheck.Visibility = Visibility.Collapsed;
spEmail.Visibility = Visibility.Visible;
}
else
this.Close();
}*/
    }
}
