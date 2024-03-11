using StomperUp.Class;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using StomperUp.Pages.AuthReg;
using System.Linq;
using StomperUp.Windows;
using MaterialDesignThemes;
using StomperUp.Pages.User;
using BCrypt.Net;
using StomperUp.Model;
using System.Collections.Generic;
using System;
using MongoDB.Bson;

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
            authFrame.Navigate(new AuthPage());
            NavigationClass.navigate = authFrame;
        }
    }
}
