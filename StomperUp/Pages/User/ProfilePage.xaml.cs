using MaterialDesignThemes.Wpf;
using Microsoft.Win32;
using StomperUp.Class;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
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
using System.IO;
using System.ComponentModel;
using StomperUp.Model;

namespace StomperUp.Pages.User
{
    /// <summary>
    /// Логика взаимодействия для ProfilePage.xaml
    /// </summary>
    public partial class ProfilePage : Page
    {
        private BitmapImage _userImage;
        public ProfilePage()
        {
            InitializeComponent();
            UserDB();
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private BitmapImage ByteArrayToBitmapImage(byte[] byteArray)
        {
            if (byteArray == null || byteArray.Length == 0)
                return null;

            BitmapImage image = new BitmapImage();
            using (MemoryStream stream = new MemoryStream(byteArray))
            {
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.StreamSource = stream;
                image.EndInit();
            }
            return image;
        }
        public async void UserDB()
        {
            var users = await ConnectionDB.GetUsers();
            var userSelect = users.Where(p => p._id == CheckClass.idUser);
            DataContext = userSelect;
        }
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            btnEdit.Visibility = Visibility.Collapsed;
            btnSave.Visibility = Visibility.Visible;
            spEditProfile.IsEnabled = true;
            tbFullName.IsEnabled = true;
        }

        private async void btnSave_Click(object sender, RoutedEventArgs e)
        {
            btnAvatar_Click(sender, e);

            var users = await ConnectionDB.GetUsers();
            var userToUpdate = users.FirstOrDefault(u => u._id == CheckClass.idUser);
            string[] searchTerms = tbFullName.Text.ToLower().Split(' ');
            var updatedUser = new UserModel
            {
                firstName = searchTerms[1],
                surName = searchTerms[0],
                middleName = searchTerms[2],
                email = tbEmail.Text,
                phone = tbPhone.Text,
                birthday = tBirthday.Text,
                picturePaths = selectedImageData
            };
            await ConnectionDB.UpdateUser(userToUpdate._id, updatedUser);

            MessageBox.Show("Пользователь успешно обновлен");
            btnEdit.Visibility = Visibility.Visible;
            btnSave.Visibility = Visibility.Collapsed;
            spEditProfile.IsEnabled = false;
            tbFullName.IsEnabled = false;
        }


        private void TextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void GitHub_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }
        private byte[] selectedImageData;
        private async void btnAvatar_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Title = "Выберите изображение",
                Filter = "Изображения|*.jpg;*.jpeg;*.png;*.bmp;*.gif"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    string imagePath = openFileDialog.FileName;
                    using (FileStream fs = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
                    {
                        selectedImageData = new byte[fs.Length];
                        await fs.ReadAsync(selectedImageData, 0, selectedImageData.Length);
                    }
                    imgPreview.Source = LoadImage(selectedImageData);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Произошла ошибка при добавлении изображения: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        public BitmapImage LoadImage(byte[] imageData)
        {
            BitmapImage image = new BitmapImage();
            using (MemoryStream stream = new MemoryStream(imageData))
            {
                stream.Position = 0;
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.StreamSource = stream;
                image.EndInit();
            }
            return image;
        }
    }
}
