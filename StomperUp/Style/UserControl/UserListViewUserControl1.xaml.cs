using StomperUp.Model;
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

namespace StomperUp.Style.UserControl
{
    /// <summary>
    /// Логика взаимодействия для UserListViewUserControl1.xaml
    /// </summary>
    public partial class UserListViewUserControl1
    {
        public UserListViewUserControl1()
        {
            InitializeComponent();
        }

        private void items_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DependencyObject depObj = (DependencyObject)e.OriginalSource;
            while ((depObj != null) && !(depObj is Border))
            {
                depObj = VisualTreeHelper.GetParent(depObj);
            }

            if (depObj is Border clickedBorder)
            {
                UserModel selectedItem = (UserModel)clickedBorder.DataContext;
                editUser.DataContext = new List<UserModel> { selectedItem };
                editUser.Visibility = Visibility.Visible;
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SaveUser_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
