using StomperUp.Class;
using System.Windows;
using StomperUp.Pages.AuthReg;

namespace StomperUp.Windows
{
    /// <summary>
    /// Логика взаимодействия для AuthRegWindow.xaml
    /// </summary>
    public partial class AuthRegWindow : Window
    {
        public AuthRegWindow()
        {
            InitializeComponent();
            mainFrame.Navigate(new AuthPage());
            NavigationClass.navigate = mainFrame;
        }
    }
}
