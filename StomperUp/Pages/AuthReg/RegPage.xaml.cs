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

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            
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
