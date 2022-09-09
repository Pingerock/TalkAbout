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

namespace TalkAbout
{
    public partial class SignUpWindow : Window
    {
        public SignUpWindow()
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

        private void AdminButton_Click(object sender, RoutedEventArgs e)
        {
            MessageWindow messageWindow = new MessageWindow("   Для получения прав администратора, обратитесь к главному \n" +                            
                                                            "администратору Игорю по номеру телефона [REDACTED].");
            messageWindow.Show();
        }

        private void ApplyButton_Click(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(usernameBox.Text) && passwordBox1.Text.Equals(passwordBox2.Text) &&
                !String.IsNullOrEmpty(passwordBox1.Text) && !String.IsNullOrEmpty(passwordBox2.Text))
            {
                this.DialogResult = true;
            }
            else {
                MessageWindow messageWindow = new MessageWindow("   Неверно введённые данные!");
                messageWindow.Show();
            }
        }

        public String Username
        {
            get { return usernameBox.Text; }
        }

        public String Password
        {
            get { return passwordBox1.Text; }
        }
    }
}
