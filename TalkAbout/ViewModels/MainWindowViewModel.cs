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
    class MainWindowViewModel : INotifyPropertyChanged
    {
        private AvailablePost currentPost;

        private Visibility adminVisibility = Visibility.Hidden;
        public Visibility AdminVisibility {
            get { return adminVisibility; }
            set {
                adminVisibility = value;
                OnPropertyChanged("AdminVisibility");
            }
        }

        private Visibility emptyVisibility = Visibility.Hidden;
        public Visibility EmptyVisibility
        {
            get { return emptyVisibility; }
            set
            {
                emptyVisibility = value;
                OnPropertyChanged("EmptyVisibility");
            }
        }

        private Visibility writePostVisibility = Visibility.Hidden;
        public Visibility WritePostVisibility 
        {
            get { return writePostVisibility; }
            set 
            {
                writePostVisibility = value;
                OnPropertyChanged("WritePostVisibility");
            }
        }

        private Visibility mainVisibility = Visibility.Visible;
        public Visibility MainVisibility
        {
            get { return mainVisibility; }
            set
            {
                mainVisibility = value;
                OnPropertyChanged("MainVisibility");
            }
        }

        private Visibility myPostVisibility = Visibility.Hidden;
        public Visibility MyPostVisibility
        {
            get { return myPostVisibility; }
            set
            {
                myPostVisibility = value;
                OnPropertyChanged("MyPostVisibility");
            }
        }

        private Visibility myCommentVisibility = Visibility.Hidden;
        public Visibility MyCommentVisibility
        {
            get { return myCommentVisibility; }
            set
            {
                myCommentVisibility = value;
                OnPropertyChanged("MyCommentVisibility");
            }
        }

        private Visibility arrowBackVisibility = Visibility.Hidden;
        public Visibility ArrowBackVisibility
        {
            get { return arrowBackVisibility; }
            set
            {
                arrowBackVisibility = value;
                OnPropertyChanged("ArrowBackVisibility");
            }
        }

        private Visibility commentsVisibility = Visibility.Hidden;
        public Visibility CommentsVisibility
        {
            get { return commentsVisibility; }
            set
            {
                commentsVisibility = value;
                OnPropertyChanged("CommentsVisibility");
            }
        }

        private Visibility searchVisibility = Visibility.Hidden;
        public Visibility SearchVisibility
        {
            get { return searchVisibility; }
            set
            {
                searchVisibility = value;
                OnPropertyChanged("SearchVisibility");
            }
        }

        private Visibility searchFailure = Visibility.Hidden;
        public Visibility SearchFailure
        {
            get { return searchFailure; }
            set
            {
                searchFailure = value;
                OnPropertyChanged("SearchFailure");
            }
        }

        private Visibility searchSuccess = Visibility.Hidden;
        public Visibility SearchSuccess
        {
            get { return searchSuccess; }
            set
            {
                searchSuccess = value;
                OnPropertyChanged("SearchSuccess");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        private DateTime period = DateTime.Now;
        public DateTime Period
        {
            get { return period; }
            set
            {
                period = value;
                OnPropertyChanged("Period");
            }
        }

        private String title = "";
        public String Title 
        {
            get { return title; }
            set 
            {
                title = value;
                OnPropertyChanged("Title");
            }
        }

        private String postText = "";
        public String PostText 
        {
            get { return postText; }
            set 
            {
                postText = value;
                OnPropertyChanged("PostText");
            }
        }

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

        private String searchBoxText = "";
        public String SearchBoxText
        {
            get { return searchBoxText; }
            set
            {
                searchBoxText = value;
                OnPropertyChanged("SearchBoxText");
            }
        }

        private String searchUsername = "";
        public String SearchUsername
        {
            get { return searchUsername; }
            set
            {
                searchUsername = value;
                OnPropertyChanged("SearchUsername");
            }
        }

        private int subscriberCount;
        public int SubscriberCount
        {
            get { return subscriberCount; }
            set
            {
                subscriberCount = value;
                OnPropertyChanged("SubscriberCount");
            }
        }

        private int subsCount;
        public int SubsCount
        {
            get { return subsCount; }
            set
            {
                subsCount = value;
                OnPropertyChanged("SubsCount");
            }
        }

        private String username;
        public String Username 
        {
            get { return username; }
            set 
            {
                username = value;
                OnPropertyChanged("Username");
            }
        }

        private AvailablePost selectedPost;
        public AvailablePost SelectedPost 
        {
            get { return selectedPost; }
            set
            {
                selectedPost = value;
                OnPropertyChanged("SelectedPost");
            }
        }

        public ObservableCollection<Post> AllPosts { get; set; }
        public ObservableCollection<AvailablePost> AvailablePosts { get; set; }
        public ObservableCollection<AvailablePost> MyPosts { get; set; }
        public ObservableCollection<Comment> MyComments { get; set; }
        public ObservableCollection<Comment> PostComments { get; set; }

        private DbOperations dbo;
        private DialogService ds;
        private FileService fs;

        public MainWindowViewModel()
        {
            

                dbo = new DbOperations();
            ds = new DialogService();
            fs = new FileService();
        }

        public MainWindowViewModel(User u) {
            dbo = new DbOperations();
            ds = new DialogService();
            fs = new FileService();

            if (u.Is_Admin == true)
            {
                adminVisibility = Visibility.Visible;
            }
            else {
                adminVisibility = Visibility.Hidden;
            }

            username = u.Name;
            subsCount = dbo.GetSubscribesCount(u);
            subscriberCount = dbo.GetSubscriberCount(u);

            AllPosts = new ObservableCollection<Post>();
            PostComments = new ObservableCollection<Comment>();
            MyComments = dbo.GetMyComments(dbo.GetUserIdByUsername(username));
            AvailablePosts = dbo.GetAvailablePosts(dbo.GetUserIdByUsername(username));
            MyPosts = dbo.GetMyPosts(dbo.GetUserIdByUsername(username));
            CheckEmptyLabel(AvailablePosts);
        }

        private RelayCommand logOut;
        public RelayCommand LogOut {
            get {
                return logOut ??
                    (logOut = new RelayCommand(obj =>
                    {
                        ds.LogOut();
                    }));
            }
        }

        private RelayCommand sendPost;
        public RelayCommand SendPost {
            get
            {
                return sendPost ??
                    (sendPost = new RelayCommand(obj =>
                    {
                        if (postText != "" || title != "")
                        {
                            Post p = new Post()
                            {
                                Create_Time = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day),
                                Sent_By_User_Id = dbo.GetUserIdByUsername(username),
                                Title = title,
                                Text = postText
                            };
                            dbo.AddPost(p);
                            AllPosts.Add(p);
                            AvailablePosts = dbo.GetAvailablePosts(dbo.GetUserIdByUsername(username));
                            CheckEmptyLabel(AvailablePosts);
                            ds.ShowMessage("Мысль успешно отправлена.");
                            WritePostVisibility = Visibility.Hidden;
                            MainVisibility = Visibility.Visible;
                        }
                        else {
                            ds.ShowMessage("Заголовок или сообщение пустое.");
                        }
                        
                    }));
            }
        }

        /*private RelayCommand sendComment;
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
                                Post_Id = currentPost.Post_Id
                            };
                            dbo.AddComment(c);
                            PostComments = dbo.GetPostComments(currentPost.Post_Id);
                            ds.ShowMessage("Комментарий успешно отправлен.");
                            CommentsVisibility = Visibility.Hidden;
                            MainVisibility = Visibility.Visible;
                        } else
                        {
                            ds.ShowMessage("Текст комментария пустой.");
                        }
                    }));
             
            }         
            
        }*/

        private RelayCommand cancelPost;
        public RelayCommand CancelPost {
            get {
                return cancelPost ??
                    (cancelPost = new RelayCommand(obj =>
                    {
                        PostText = "";
                        Title = "";
                        WritePostVisibility = Visibility.Hidden;
                        MainVisibility = Visibility.Visible;
                    }));
            }
        }

        private RelayCommand cancelComment;
        public RelayCommand CancelComment {
            get {
                return cancelComment ??
                    (cancelComment = new RelayCommand(obj =>
                    {
                        CommentText = "";
                        MainVisibility = Visibility.Visible;
                        CommentsVisibility = Visibility.Hidden;
                    }));
            }
        }

        private RelayCommand commentSectionCommand;
        public RelayCommand CommentSectionCommand
        {
            get
            {
                return commentSectionCommand ??
                    (commentSectionCommand = new RelayCommand(obj =>
                    {
                        MainVisibility = Visibility.Hidden;
                        MyPostVisibility = Visibility.Hidden;
                        CommentsVisibility = Visibility.Visible;
                    }));
            }
        }

        public void Like(AvailablePost ap) 
        {
            if (dbo.IsItLiked(ap.Post_Id, username))
            {
                dbo.DeleteLikedPost(ap.Post_Id, username);
                dbo.UpdateNumberOfLikesInPost(ap.Post_Id, 0);
                AvailablePosts = dbo.GetAvailablePosts(dbo.GetUserIdByUsername(username));
            }
            else 
            {
                LikedPost p = new LikedPost()
                {
                    Post_Id = ap.Post_Id,
                    User_Id = dbo.GetUserIdByUsername(username)
                };
                dbo.AddLikedPost(p);
                dbo.UpdateNumberOfLikesInPost(ap.Post_Id, 1);
                AvailablePosts = dbo.GetAvailablePosts(dbo.GetUserIdByUsername(username));
            }
        }

        public void DeletePost(AvailablePost ap) 
        {
            dbo.DeletePost(ap.Post_Id);
            ds.ShowMessage("Пост удалён.");
        }

        public void DeleteComment(Comment c) 
        {
            dbo.DeletePost(c.Comment_Id);
            ds.ShowMessage("Комментарий удалён.");
        }

        public void CommentSection(AvailablePost ap)
        {
            PostComments = dbo.GetPostComments(ap.Post_Id);
            currentPost = ap;
        }

        private RelayCommand writePost;
        public RelayCommand WritePost {
            get
            {
                return writePost ??
                    (writePost = new RelayCommand(obj =>
                    {
                        MainVisibility = Visibility.Hidden;
                        WritePostVisibility = Visibility.Visible;
                        MyPostVisibility = Visibility.Hidden;
                        MyCommentVisibility = Visibility.Hidden;
                        SearchVisibility = Visibility.Hidden;
                        ArrowBackVisibility = Visibility.Hidden;
                        //CommentsVisibility = Visibility.Hidden;
                    }));
            }
        }

        public void CheckEmptyLabel(ObservableCollection<AvailablePost> availablePosts) 
        {
            if (availablePosts.Count > 0)
            {
                mainVisibility = Visibility.Visible;
                emptyVisibility = Visibility.Hidden;
            }
            else 
            {
                mainVisibility = Visibility.Hidden;
                emptyVisibility = Visibility.Visible;
            }
        }

        private RelayCommand exportReport;
        public RelayCommand ExportReport
        {
            get
            {
                return exportReport ??
                    (exportReport = new RelayCommand(obj =>
                    {
                        try
                        {
                            FullReportData fullReportData = PrepareReportData(period);

                            if (fullReportData != null)
                            {
                                string title = $"Отчет за {period.Month}.{period.Year} от {DateTime.Now.ToShortDateString()}";
                                if (ds.SaveFileDialog(title))
                                {
                                    fs.WriteReportData(ds.FilePath, fullReportData, title);
                                    ds.ShowMessage("Файл сохранен");
                                }
                            }
                            else ds.ShowMessage("Нет данных за выбранный месяц");
                        }
                        catch (NullReferenceException)
                        {
                            ds.ShowMessage("Период не выбран.");
                        }
                        catch
                        {
                            ds.ShowMessage("При подготовке отчета произошла ошибка. Повторите попытку.");
                        }
                    }));
            }
        }

        private FullReportData PrepareReportData(DateTime period)
        {
            FullReportData fullReportData = new FullReportData();
            List<Post> posts = dbo.GetPostsByPeriod(period);

            if (posts.Count == 0) 
            {
                return null;
            }

            int avrgLike = 0, avrgComment = 0, userCount = dbo.GetUserCount();

            fullReportData.PostData = new List<ReportPostData>();
            foreach (Post p in posts)
            {
                String username = dbo.GetUsernameByPostId(p.Sent_By_User_Id);
                avrgLike += p.Number_of_Likes;
                avrgComment += p.Number_of_Comments;

                fullReportData.PostData.Add(new ReportPostData()
                {
                    Id = p.Post_Id,
                    Username = username,
                    CreatedAt = p.Create_Time.ToString(),
                    Title = p.Title,
                    Text = p.Text,
                    Likes = p.Number_of_Likes,
                    Comments = p.Number_of_Comments
                });
            }

            avrgComment = (int)(avrgComment / posts.Count);
            avrgLike = (int)(avrgComment / posts.Count);

            fullReportData.PostCount = posts.Count;
            fullReportData.UserCount = userCount;
            fullReportData.AverageLikes = avrgLike;
            fullReportData.AverageComments = avrgComment;

            return fullReportData;
        }

        private RelayCommand myPost;
        public RelayCommand MyPost
        {
            get
            {
                return myPost ??
                    (myPost = new RelayCommand(obj =>
                    {
                        MainVisibility = Visibility.Hidden;
                        MyPostVisibility = Visibility.Visible;
                        MyCommentVisibility = Visibility.Hidden;
                        ArrowBackVisibility = Visibility.Visible;
                        SearchVisibility = Visibility.Hidden;
                    }));
            }
        }

        private RelayCommand myComment;
        public RelayCommand MyComment
        {
            get
            {
                return myComment ??
                    (myComment = new RelayCommand(obj =>
                    {
                        MainVisibility = Visibility.Hidden;
                        MyCommentVisibility = Visibility.Visible;
                        MyPostVisibility = Visibility.Hidden;
                        ArrowBackVisibility = Visibility.Visible;
                        SearchVisibility = Visibility.Hidden;
                    }));
            }
        }

        private RelayCommand searchFriend;
        public RelayCommand SearchFriend
        {
            get
            {
                return searchFriend ??
                    (searchFriend = new RelayCommand(obj =>
                    {
                        MainVisibility = Visibility.Hidden;
                        MyPostVisibility = Visibility.Hidden;
                        SearchVisibility = Visibility.Visible;
                        MyCommentVisibility = Visibility.Hidden;
                        ArrowBackVisibility = Visibility.Visible;
                    }));
            }
        }

        private RelayCommand search;
        public RelayCommand Search
        {
            get
            {
                return search ??
                    (search = new RelayCommand(obj =>
                    {
                        if (searchBoxText != "") 
                        {
                            if (dbo.UserExist(searchBoxText))
                            {
                                SearchSuccess = Visibility.Visible;
                                SearchFailure = Visibility.Hidden;
                                SearchUsername = searchBoxText;
                            }
                            else 
                            {
                                SearchFailure = Visibility.Visible;
                                SearchSuccess = Visibility.Hidden;
                                SearchUsername = searchBoxText;
                            }
                        }
                    }));
            }
        }

        private RelayCommand subscribe;
        public RelayCommand Subscribe
        {
            get
            {
                return subscribe ??
                    (subscribe = new RelayCommand(obj =>
                    {
                        if (!dbo.IsHeAlreadySubscribed(username, SearchUsername))
                        {
                            dbo.AddSubscriber(username, SearchUsername);
                            ds.ShowMessage("Подписка выполнена успешно. ");
                        }
                        else 
                        {
                            ds.ShowMessage("Вы уже подписаны на данного пользователя. ");
                        }
                        
                    }));
            }
        }

        private RelayCommand mainWindow;
        public RelayCommand MainWindow
        {
            get
            {
                return mainWindow ??
                    (mainWindow = new RelayCommand(obj =>
                    {
                        MainVisibility = Visibility.Visible;
                        MyPostVisibility = Visibility.Hidden;
                        MyCommentVisibility = Visibility.Hidden;
                        ArrowBackVisibility = Visibility.Hidden;
                        //CommentsVisibility = Visibility.Hidden;
                        SearchVisibility = Visibility.Hidden;
                        SearchUsername = "";
                        SearchFailure = Visibility.Hidden;
                        SearchSuccess = Visibility.Hidden;
                    }));
            }
        }
    }
}
