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
    public partial class MainWindow : Window
    {
        private User u;
        private MainWindowViewModel mainWindowViewModel;

        public MainWindow() {
            InitializeComponent();

            DataContext = new MainWindowViewModel();
        }

        public MainWindow(User u)
        {
            InitializeComponent();
            this.u = u;
            DataContext = new MainWindowViewModel(u);
            mainWindowViewModel = new MainWindowViewModel(u);
            titleBox.MaxLength = 50; 
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                if (Window.WindowState == WindowState.Maximized)
                {
                    Window.WindowState = WindowState.Normal;
                }
                else { 
                    Window.WindowState = WindowState.Maximized; 
                }
            }
        }

        private void postTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Delete && e.Key != Key.Back && e.Key != Key.Enter)
            {
                var range = new TextRange(postTextBox.Document.ContentStart, postTextBox.Document.ContentEnd);
                if (range.Text.Length > 500)
                {
                    e.Handled = true;
                    return;
                }
            }
        }

        /*private void commentTextBox_KeyDown(object sender, KeyEventArgs e)
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
        }*/

        private void LikeButton_Click(object sender, RoutedEventArgs e)
        {
            AvailablePost ap = (AvailablePost)((ListBoxItem)ListBox1.ContainerFromElement((Button)sender)).Content;
            mainWindowViewModel.Like(ap);
        }

        private void CommentButton_Click(object sender, RoutedEventArgs e)
        {
            AvailablePost ap = (AvailablePost)((ListBoxItem)ListBox1.ContainerFromElement((Button)sender)).Content;
            CommentWindow cm = new CommentWindow(ap, u);
            cm.Show();
        }

        private void DeletePostButton_Click(object sender, RoutedEventArgs e)
        {
            AvailablePost ap = (AvailablePost)((ListBoxItem)ListBox2.ContainerFromElement((Button)sender)).Content;
            mainWindowViewModel.DeletePost(ap);
        }

        private void DeleteCommentButton_Click(object sender, RoutedEventArgs e)
        {
            Comment c = (Comment)((ListBoxItem)ListBox3.ContainerFromElement((Button)sender)).Content;
            mainWindowViewModel.DeleteComment(c);
        }
    }
}
