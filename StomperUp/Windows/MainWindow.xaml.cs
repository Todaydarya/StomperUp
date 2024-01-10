using System;
using System.Collections.Generic;
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
using StomperUp.Pages.User;
using StomperUp.Class;
using StomperUp.Pages.AuthReg;
using System.Globalization;

namespace StomperUp.Windows
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            mainFrame.Navigate(new MainPage());
            NavigationClass.navigate = mainFrame;
        }

        private void btnMain_Click(object sender, RoutedEventArgs e)
        {
            NavigationClass.navigate.Navigate(new MainPage());
        }

        private void btnCurriculum_Click(object sender, RoutedEventArgs e)
        {
            NavigationClass.navigate.Navigate(new CurriculumPage());
        }

        private void btnAchievements_Click(object sender, RoutedEventArgs e)
        {
            NavigationClass.navigate.Navigate(new AchievementsPage());
        }

        private void btnTask_Click(object sender, RoutedEventArgs e)
        {
            NavigationClass.navigate.Navigate(new TaskPage());
        }

        private void btnSettings_Click(object sender, RoutedEventArgs e)
        {
            NavigationClass.navigate.Navigate(new SettingsPage());
        }

        private void btnProfile_Click(object sender, RoutedEventArgs e)
        {
            if (CheckClass.idUser == null)
            {
                if (MessageBox.Show("Вы не Авторизированы. Войти в аккаунт?", "Внимание", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    AuthRegWindow authReg = new AuthRegWindow();
                    authReg.Show();
                }
            }
            else
            {
                NavigationClass.navigate.Navigate(new ProfilePage());
            }
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            /*if (e.NewSize.Height < 600)
            {
                menuWrapPanel.Height = 72;

                *//*spMenu.Width = e.NewSize.Width;
                spMenu.Height = double.NaN;*//* // Используем NaN для автоматической высоты

            }
            else
            {
                menuWrapPanel.Height = double.NaN;
*//*                menuWrapPanel.Orientation = Orientation.Vertical;
                menuWrapPanel.HorizontalAlignment = HorizontalAlignment.Left;

                spMenu.VerticalAlignment = VerticalAlignment.Center;
                spMenu.Width = double.NaN;*//*
            }*/
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            if(MessageBox.Show("Выйти из приложения?", "Внимание", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }
        
    }
}
