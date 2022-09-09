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
using System.Data.Entity;
using TalkAbout.Models;
using TalkAbout.ViewModels;

namespace TalkAbout
{
    public partial class LogInWindow : Window
    {
        public LogInWindow()
        {
            InitializeComponent();          
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void SignUpButton_Click(object sender, RoutedEventArgs e)
        {
            SignUpWindow signUpWindow = new SignUpWindow();
            signUpWindow.Show();
        }

        private void LogInButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        public String Username {
            get { return usernameBox.Text;  }
        }

        public String Password {
            get { return passwordBox.Password; }
        }
    }
}
