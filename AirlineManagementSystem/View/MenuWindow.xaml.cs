﻿using AirportManagerSystem.Model;
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
using System.Windows.Shapes;

namespace AirportManagerSystem.View
{
    /// <summary>
    /// Interaction logic for MenuWindow.xaml
    /// </summary>
    public partial class MenuWindow : Window
    {
        public User User { get; internal set; }
        public MenuWindow()
        {
            InitializeComponent();
            imgLogo.Source = new BitmapImage(new Uri(@"/AirportManagerSystem;component/Images/WSC2017_TP09_color@4x.png", UriKind.Relative));
            this.Closing += MenuWindow_Closing;
        }

        private void MenuWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var log = Db.Context.LoginHistories.Where(t => t.UserId == User.ID).ToList().Last();
            log.LogoutTime = DateTime.Now;
            Db.Context.SaveChanges();
        }

        private void MnUsers_Click(object sender, RoutedEventArgs e)
        {
            UserManagementWindow wUsers = new UserManagementWindow();
            wUsers.ShowDialog();
        }

        private void mnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
