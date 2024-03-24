using Microsoft.Win32;
using StomperUp.Class;
using StomperUp.Model;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Shapes;

namespace StomperUp.Windows.Admin
{
    /// <summary>
    /// Логика взаимодействия для AddAchievementsWindow.xaml
    /// </summary>
    public partial class AddAchievementsWindow : Window
    {
        public AddAchievementsWindow()
        {
            InitializeComponent();
        }
        private byte[] selectedImageData;

        private async void btnPicturePath_Click(object sender, RoutedEventArgs e)
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
        private async void btnNext_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                AchievementsModel achievement = new AchievementsModel
                {
                    nameAchievement = tbName.Text,
                    coin = int.Parse(tbCoin.Text),
                    picturePath = selectedImageData,
                    description = tbDescription.Text
                };

                await ConnectionDB.AddAchievements(achievement);
                MessageBox.Show("Достижение добавлено");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка при сохранении данных: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}
