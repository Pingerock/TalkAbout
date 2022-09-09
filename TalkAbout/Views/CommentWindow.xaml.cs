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
    public partial class CommentWindow : Window
    {

        public CommentWindow(AvailablePost ap, User u)
        {
            InitializeComponent();

            DataContext = new CommentWindowViewModel(ap, u);
        }
        private void commentTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Delete && e.Key != Key.Back && e.Key != Key.Enter)
            {
                var range = new TextRange(commentTextBox.Document.ContentStart, commentTextBox.Document.ContentEnd);
                if (range.Text.Length > 500)
                {
                    e.Handled = true;
                    return;
                }
            }
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                if (Window.WindowState == WindowState.Maximized)
                {
                    Window.WindowState = WindowState.Normal;
                }
                else
                {
                    Window.WindowState = WindowState.Maximized;
                }
            }
        }
    }
}
