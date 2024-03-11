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
using MaterialDesignThemes.Wpf;
using MongoDB.Bson;

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
        private void CheckAuthorizationAndNavigate(Page page)
        {
            string idUserString = Properties.Settings.Default.idUser;
            CheckClass.idUser = ObjectId.Empty;
            if (!string.IsNullOrEmpty(idUserString) && ObjectId.TryParse(idUserString, out ObjectId userId))
            {
                CheckClass.idUser = userId;
            }

            if (CheckClass.idUser != ObjectId.Empty)
            {
                NavigationClass.navigate.Navigate(page);
            }
            else if (MessageBox.Show("Вы не Авторизированы. Войти в аккаунт?", "Внимание", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                AuthRegWindow authReg = new AuthRegWindow();
                authReg.ShowDialog();
            }
        }

        private void btnCurriculum_Click(object sender, RoutedEventArgs e)
        {
            CheckAuthorizationAndNavigate(new CurriculumPage());
        }

        private void btnAchievements_Click(object sender, RoutedEventArgs e)
        {
            CheckAuthorizationAndNavigate(new AchievementsPage());
        }

        private void btnTask_Click(object sender, RoutedEventArgs e)
        {
            CheckAuthorizationAndNavigate(new TaskPage());
        }

        private void btnSettings_Click(object sender, RoutedEventArgs e)
        {
            CheckAuthorizationAndNavigate(new SettingsPage());
        }

        private void btnProfile_Click(object sender, RoutedEventArgs e)
        {
            CheckAuthorizationAndNavigate(new ProfilePage());
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

        private void Window_Activated(object sender, EventArgs e)
        {
            if(CheckClass.isAuthPage == false) 
                NavigationClass.navigate = mainFrame;
        }
    }
}
