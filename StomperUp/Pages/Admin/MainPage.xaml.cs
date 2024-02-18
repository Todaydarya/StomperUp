using StomperUp.Class;
using StomperUp.Model;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using StomperUp.Windows.Admin;
using System.Collections.ObjectModel;

namespace StomperUp.Pages.Admin
{
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
            UserDB();
        }

        public async void UserDB()
        {
            var users = await ConnectionDB.GetUsers();
            items.ItemsSource = users;
        }
        public async void CourseDB()
        {
            var course = await ConnectionDB.GetCourse();
            itemCourse.ItemsSource = course;
        }
        public async void LessonDB()
        {
            var lesson = await ConnectionDB.GetLesson();
        }
        private async void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (cbUserAndCourse.SelectedIndex == 1)
            {
                var users = await ConnectionDB.GetUsers();
                string[] searchTerms = tbSearch.Text.ToLower().Split(' ');
                var search = 
                items.ItemsSource = users.ToList().Where(p => searchTerms.Any(term => p.firstName.ToLower().Contains(term)
                                                                                    || p.surName.ToLower().Contains(term)
                                                                                    || p.email.ToLower().Contains(term)
                                                                                    || p.phone.ToLower().Contains(term)
                                                                                    || p.role.ToLower().Contains(term))).ToList();
            }
            else
            {
                var course = await ConnectionDB.GetCourse();
                string[] searchTerms = tbSearch.Text.ToLower().Split(' ');

                items.ItemsSource = course.ToList().Where(p => searchTerms.Any(term => p.nameCourse.ToLower().Contains(term)
                                                                                    || p.coin.ToString().Contains(term)
                                                                                    || p.countLesson.ToString().Contains(term)
                                                                                    || p.programmingLanguage.ToLower().Contains(term))).ToList();
            }
        }

        private void cbUserAndCourse_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int i = 0;
            if (i != 1)
            {
                if (cbUserAndCourse.SelectedIndex == 1)
                {
                    if (gridUser != null)
                    {
                        gridUser.Visibility = Visibility.Visible;
                        items.Visibility = Visibility.Visible;
                        gridCourse.Visibility = Visibility.Collapsed;
                        btnAddLesson.Visibility = Visibility.Collapsed;
                        itemLesson.ItemsSource = null;
                        UserDB();
                    }
                }
                else
                {
                    gridUser.Visibility = Visibility.Collapsed;
                    items.Visibility = Visibility.Collapsed;
                    gridCourse.Visibility = Visibility.Visible;
                    CourseDB();
                    LessonDB();
                }
            }
            else i++;          
        }

        private void add_Click(object sender, RoutedEventArgs e)
        {
            if (cbUserAndCourse.SelectedIndex == 0)
            {
                AddCourse addCourse = new AddCourse();
                addCourse.ShowDialog();
            }
            else
            {
                editUser.DataContext = null;
                editUser.Visibility = Visibility.Visible;
            }
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
            editUser.DataContext = null;
            editUser.Visibility = Visibility.Collapsed;
        }

        private async void SaveUser_Click(object sender, RoutedEventArgs e)
        {
            if (editUser.DataContext is List<UserModel> userList && userList.Count > 0)
            {
                UserModel selectedItem = userList[0];
                var userToUpdate = userList.FirstOrDefault(u => u._id == selectedItem._id);
                string[] searchTerms = tbFullName.Text.ToLower().Split(' ');
                var updatedUser = new UserModel
                {
                    firstName = searchTerms[1],
                    surName = searchTerms[0],
                    middleName = searchTerms[2],
                    phone = tbPhone.Text,
                    coin = int.Parse(tbCoin.Text),
                    email = tbEmail.Text,
                    role = cbRole.Text,
                    birthday = tbBirthday.Text,
                    picturePaths = null
                };
                await ConnectionDB.UpdateUser(userToUpdate._id, updatedUser);

                MessageBox.Show("Пользователь успешно обновлен");
                editUser.Visibility = Visibility.Collapsed;
                UserDB();
            }
            else
            {
                MessageBox.Show("Список пользователей пуст");
            }
        }

        private void itemCourse_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            

        }

        private async void btnInfo_Click(object sender, RoutedEventArgs e)
        {
            var selectedCourse = (sender as Button).DataContext as CourseModel;
            btnAddLesson.Visibility = Visibility.Visible;
            if (selectedCourse != null)
            {
                List<LessonsModel> allLessons = await ConnectionDB.GetLesson();
                List<LessonsModel> lessonsForSelectedCourse = allLessons.Where(lesson => lesson.idCourse == selectedCourse._id).ToList();
                itemLesson.ItemsSource = lessonsForSelectedCourse;
                CheckClass.idCourseSelect = selectedCourse._id;
            }
        }

        private void btnAddLesson_Click(object sender, RoutedEventArgs e)
        {
            var selectedCourse = CheckClass.idCourseSelect;
            AddLesson addLesson = new AddLesson(selectedCourse);
            addLesson.ShowDialog();
        }

        private void btnAddCourse_Click(object sender, RoutedEventArgs e)
        {
            AddCourse addCourse = new AddCourse();
            addCourse.ShowDialog();
        }
    }
}
