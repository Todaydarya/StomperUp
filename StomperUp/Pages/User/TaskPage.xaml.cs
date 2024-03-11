using MongoDB.Bson;
using StomperUp.Class;
using StomperUp.Model;
using StomperUp.Windows;
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

namespace StomperUp.Pages.User
{
    /// <summary>
    /// Логика взаимодействия для TaskPage.xaml
    /// </summary>
    public partial class TaskPage : Page
    {
        public TaskPage()
        {
            InitializeComponent();
            GetTask();
            tbAddTask.Focus();
        }

        private void btnAddTask_Click(object sender, RoutedEventArgs e)
        {
            AddTask();
        }

        private async void btnDeleteTask_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button deleteButton && deleteButton.DataContext is TaskModel task)
            {
                ObjectId taskId = task._id;
                await ConnectionDB.DeleteTaskById(taskId);
                GetTask();
            }
        }

        private bool isHandlingCheckBoxChange = false;

        private void cbCheck_Checked(object sender, RoutedEventArgs e)
        {
            CheckIsActive(sender);
        }

        private void cbCheck_Unchecked(object sender, RoutedEventArgs e)
        {
            CheckIsActive(sender);
        }

        public async void CheckIsActive(object sender)
        {
            if (!isHandlingCheckBoxChange)
            {
                isHandlingCheckBoxChange = true;

                if (sender is CheckBox editCheckBox && editCheckBox.DataContext is TaskModel task)
                {
                    ObjectId taskId = task._id;
                    task.isActive = !task.isActive;
                    editCheckBox.IsChecked = task.isActive;
                    await ConnectionDB.UpdateTask(taskId, task);
                    GetTask();
                }

                isHandlingCheckBoxChange = false;
            }
        }

        private void tbAddTask_KeyDown(object sender, KeyEventArgs e)
        {
            EnterCheck(e);
        }
        public void EnterCheck(KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                AddTask();
            }
        }

        public async void GetTask()
        {
            var tasks = await ConnectionDB.GetTasks();
            var taskUser = tasks.Where(p => p._idUser == CheckClass.idUser);
            DataContext = taskUser;
            lvTask.ItemsSource = taskUser.OrderByDescending(p => p.isActive);
        }

        public async void AddTask()
        {
            if (tbAddTask.Text == "")
                MessageBox.Show("Поле не заполнено");
            else
            {
                TaskModel newTask = new TaskModel
                {
                    _idUser = CheckClass.idUser,
                    taskName = tbAddTask.Text,
                    isActive = true,
                    isPriority = "",
                    remind = default,
                    replay = "",
                };
                try
                {
                    await ConnectionDB.AddTask(newTask);
                    GetTask();
                    tbAddTask.Focus();
                    tbAddTask.Text = "";
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }
    }
}
