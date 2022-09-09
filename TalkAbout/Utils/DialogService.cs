using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TalkAbout.Models;
using Microsoft.Win32;

namespace TalkAbout.Utils
{
    class DialogService
    {
        public string FilePath { get; set; }
        public String Username { get; set; }
        public String Password { get; set; }

        public bool SignUpDialog() {
            SignUpWindow window = new SignUpWindow();
            if (window.ShowDialog() == true) {
                Username = window.Username;
                Password = window.Password;
                return true;
            } else {
                return false;
            }
        }

        public void ShowMessage(String message) {
            MessageWindow messageWindow = new MessageWindow(message);
            messageWindow.Show();
        }

        public bool LogInDialog() {
            LogInWindow window = new LogInWindow();
            if (window.ShowDialog() == true) {
                Username = window.Username;
                Password = window.Password;
                return true;
            } else {
                return false;
            }
        }

        public void LogInComplete(User u) {
            for (int intCounter = App.Current.Windows.Count - 1; intCounter >= 0; intCounter--)
            {
                if (intCounter == 0)
                {
                    MainWindow window = new MainWindow(u);
                    window.Show();
                }
                App.Current.Windows[intCounter].Close();
            }
        }

        public void LogOut() {
            for (int intCounter = App.Current.Windows.Count - 1; intCounter >= 0; intCounter--)
            {
                if (intCounter == 0)
                {
                    StartWindow window = new StartWindow();
                    window.Show();
                }
                App.Current.Windows[intCounter].Close();
            }
        }

        public bool SaveFileDialog(string name)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "CSV files(*.csv)|*.csv";
            saveFileDialog.FileName = name;
            if (saveFileDialog.ShowDialog() == true)
            {
                FilePath = saveFileDialog.FileName;
                return true;
            }
            return false;
        }
    }
}
