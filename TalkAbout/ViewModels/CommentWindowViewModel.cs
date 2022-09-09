using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using TalkAbout.Models;
using TalkAbout.Utils;
using System.ComponentModel;
using System.Windows;
using System.Runtime.CompilerServices;

namespace TalkAbout.ViewModels
{
    class CommentWindowViewModel : INotifyPropertyChanged
    {
        private DbOperations dbo;
        private DialogService ds;

        private AvailablePost ap;
        private User u;
        private String username;

        private String commentText = "";
        public String CommentText
        {
            get { return commentText; }
            set
            {
                commentText = value;
                OnPropertyChanged("CommentText");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        public ObservableCollection<Comment> PostComments { get; set; }

        public CommentWindowViewModel(AvailablePost ap, User u)
        {
            dbo = new DbOperations();
            ds = new DialogService();
            this.ap = ap;
            this.u = u;
            username = u.Name;
            PostComments = dbo.GetPostComments(ap.Post_Id);
        }

        private RelayCommand sendComment;
        public RelayCommand SendComment
        {
            get
            {
                return sendComment ??
                    (sendComment = new RelayCommand(obj =>
                    {
                        if (commentText != "")
                        {
                            Comment c = new Comment()
                            {
                                Create_Time = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day),
                                Text = commentText,
                                Sent_by_User_Id = dbo.GetUserIdByUsername(username),
                                Post_Id = ap.Post_Id
                            };
                            dbo.AddComment(c);
                            PostComments = dbo.GetPostComments(ap.Post_Id);
                            ds.ShowMessage("Комментарий успешно отправлен.");
                        }
                        else
                        {
                            ds.ShowMessage("Текст комментария пустой.");
                        }
                    }));

            }

        }
    }
}
