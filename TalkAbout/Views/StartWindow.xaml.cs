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
using TalkAbout.Models;
using TalkAbout.ViewModels;

namespace TalkAbout
{
    public partial class StartWindow : Window
    {
        public StartWindow()
        {
            InitializeComponent();

            DataContext = new StartWindowViewModel();
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void SignUpButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
