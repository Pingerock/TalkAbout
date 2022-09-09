using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace TalkAbout.Models
{
    public class DbOperations
    {
        private TalkAboutModel db;

        public DbOperations() {
            db = new TalkAboutModel();
        }

        public ObservableCollection<User> GetAdminUsers() {
            var u = db.Users.Select(c => c)
                .Where(c => c.Is_Admin == true).ToList();
            return new ObservableCollection<User>(u);
        }

        public int AddUser(User u) {
            db.Users.Add(u);
            db.SaveChanges();
            return u.User_Id;
        }

        public int AddPost(Post p) {
            db.Posts.Add(p);
            db.SaveChanges();
            return p.Post_Id;
        }

        public List<int> GetUsersSubscribedToUser(int userId) {
            return db.Subscribes
                .Where(s => s.User_Id == userId)
                .Select(s => s.Subscribed_to_User_Id)
                .ToList();
        }

        public int AddComment(Comment c) {
            db.Comments.Add(c);
            Post p = db.Posts.Where(i => i.Post_Id == c.Post_Id).FirstOrDefault();
            p.Number_of_Comments++;
            db.SaveChanges();
            return c.Comment_Id;
        }

        public ObservableCollection<User> GetAllUsers() {
            var r = db.Users.Select(u => u).ToList();
            return new ObservableCollection<User>(r);
        }

        public ObservableCollection<User> GetBannedUsers() {
            var b = db.Users.Select(u => u)
                .Where(u => u.Is_Banned == true).ToList();
            return new ObservableCollection<User>(b);
        }

        public ObservableCollection<User> GetNormalUsers() {
            var n = db.Users.Select(u => u).Where(u => u.Is_Admin == false && !u.Is_Banned).ToList();
            return new ObservableCollection<User>(n);
        }

        public void DeletePost(int Post_Id) {
            Post pp = db.Posts.Where(i => i.Post_Id == Post_Id).FirstOrDefault();
            foreach (LikedPost lp in db.LikedPosts) 
            {
                if (lp.Post_Id == pp.Post_Id) 
                {
                    db.LikedPosts.Remove(lp);
                }
            }
            foreach (Comment c in db.Comments) 
            {
                if (c.Post_Id == pp.Post_Id) 
                {
                    db.Comments.Remove(c);
                }
            }
            db.Posts.Remove(pp);
            db.SaveChanges();
        }

        public void UnsubscribeToUser(User user, User sub) {
            Subscribe u = db.Subscribes.Where(i => i.Subscribed_to_User_Id == user.User_Id && i.User_Id == sub.User_Id).FirstOrDefault();
            db.Subscribes.Remove(u);
            db.SaveChanges();
        }

        public void DeleteComment(int CId) {
            Comment cc = db.Comments.Where(i => i.Comment_Id == CId).FirstOrDefault();
            foreach (LikedComment lc in db.LikedComments)
            {
                if (lc.Comment_Id == cc.Comment_Id)
                {
                    db.LikedComments.Remove(lc);
                }
            }
            db.Comments.Remove(cc);
            db.SaveChanges();
        }

        public List<Post> GetPostsByPeriod(DateTime period)
        {
            return db.Posts
                .Where(i => i.Create_Time.Month == period.Month &&
                i.Create_Time.Year == period.Year).ToList();

        }

        public bool AuthentificationUser(String password, String username) {
            if (db.Users.Any(i => i.Name == username && i.Password == password))
            {
                return true;
            } else {
                return false;
            }
        }

        public bool CheckFreeUsername(String username) {
            if (db.Users.Any(i => i.Name == username))
            {
                return true;
            }
            else {
                return false;
            }
        }

        public User GetUserByUsername(String username) {
            return db.Users.Where(i => i.Name == username).FirstOrDefault();
        }

        public int GetUserIdByUsername(String username) {
            User u = db.Users.Where(i => i.Name == username).FirstOrDefault();
            return u.User_Id;
        }

        public List<User> GetUserByPostId(int Post_Id) {
            return db.Users.Where(i => i.User_Id == Post_Id).ToList();
        }

        public ObservableCollection<AvailablePost> GetAvailablePosts(int UId)
        {
            List<AvailablePost> postList = new List<AvailablePost>();

            foreach (Subscribe s in db.Subscribes)
            {
                if (s.User_Id == UId)
                {
                    foreach (Post p in db.Posts)
                    {
                        if (p.Sent_By_User_Id == s.Subscribed_to_User_Id)
                        {
                            AvailablePost pp = new AvailablePost()
                            {
                                Post_Id = p.Post_Id,
                                Create_Time = p.Create_Time,
                                Sent_By_User_Id = p.Sent_By_User_Id,
                                Number_of_Likes = p.Number_of_Likes,
                                Number_of_Comments = p.Number_of_Comments,
                                Title = p.Title,
                                Text = p.Text
                            };
                            foreach (User user in GetUserByPostId(p.Sent_By_User_Id))
                            {
                                pp.User = user;
                            }
                            postList.Add(pp);
                        }
                    }
                }
            }
            foreach (Post p in db.Posts)
            {
                if (p.Sent_By_User_Id == UId)
                {
                    AvailablePost pp = new AvailablePost()
                    {
                        Post_Id = p.Post_Id,
                        Create_Time = p.Create_Time,
                        Sent_By_User_Id = p.Sent_By_User_Id,
                        Number_of_Likes = p.Number_of_Likes,
                        Number_of_Comments = p.Number_of_Comments,
                        Title = p.Title,
                        Text = p.Text
                    };
                    foreach (User user in GetUserByPostId(p.Sent_By_User_Id))
                    {
                        pp.User = user;
                    }
                    postList.Add(pp);
                }
            }
            postList = postList.OrderByDescending(x => x.Create_Time).ToList();

            return new ObservableCollection<AvailablePost>(postList);
        }

        public int GetUserCount()
        {
            return db.Users.Count();
        }

        public String GetUsernameByPostId(int Sent_By_User_Id)
        {
            User u = db.Users.Where(i => i.User_Id == Sent_By_User_Id).FirstOrDefault();
            return u.Name;
        }

        public ObservableCollection<AvailablePost> GetMyPosts(int UId)
        {
            List<AvailablePost> postList = new List<AvailablePost>();

            foreach (Post p in db.Posts)
            {
                if (p.Sent_By_User_Id == UId)
                {
                    AvailablePost pp = new AvailablePost()
                    {
                        Post_Id = p.Post_Id,
                        Create_Time = p.Create_Time,
                        Sent_By_User_Id = p.Sent_By_User_Id,
                        Number_of_Likes = p.Number_of_Likes,
                        Number_of_Comments = p.Number_of_Comments,
                        Title = p.Title,
                        Text = p.Text
                    };
                    foreach (User user in GetUserByPostId(p.Sent_By_User_Id))
                    {
                        pp.User = user;
                    }
                    postList.Add(pp);
                }
            }

            postList = postList.OrderByDescending(x => x.Create_Time).ToList();

            return new ObservableCollection<AvailablePost>(postList);
        }

        public ObservableCollection<Comment> GetMyComments(int UId) 
        {
            List<Comment> commList = new List<Comment>();

            foreach (Comment c in db.Comments)
            {
                if (c.Sent_by_User_Id == UId)
                {
                    commList.Add(c);
                }
            }

            commList = commList.OrderByDescending(x => x.Create_Time).ToList();

            return new ObservableCollection<Comment>(commList);
        }

        public bool IsItLiked(int Post_Id, String Username)
        {
            int user_Id = GetUserIdByUsername(Username);
            if (db.LikedPosts.Any(i => i.Post_Id == Post_Id && i.User_Id == user_Id))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void DeleteLikedPost(int Post_Id, String Username)
        {
            int user_Id = GetUserIdByUsername(Username);
            LikedPost lp = db.LikedPosts.Where(i => i.Post_Id == Post_Id && i.User_Id == user_Id).FirstOrDefault();
            db.LikedPosts.Remove(lp);
            db.SaveChanges();
        }

        public int AddLikedPost(LikedPost p)
        {
            db.LikedPosts.Add(p);
            db.SaveChanges();
            return p.Id;
        }

        public void UpdateNumberOfLikesInPost(int Post_Id, int PlusOrMinus)
        {
            switch (PlusOrMinus)
            {
                case 0:
                    Post p = db.Posts.Where(i => i.Post_Id == Post_Id).FirstOrDefault();
                    p.Number_of_Likes--;
                    db.SaveChanges();
                    break;
                case 1:
                    Post pp = db.Posts.Where(i => i.Post_Id == Post_Id).FirstOrDefault();
                    pp.Number_of_Likes++;
                    db.SaveChanges();
                    break;
            }
        }

        public ObservableCollection<Comment> GetPostComments(int Post_Id)
        {
            List<Comment> commList = new List<Comment>();

            foreach (Comment c in db.Comments)
            {
                if (c.Post_Id == Post_Id)
                {
                    commList.Add(c);
                }
            }

            commList = commList.OrderByDescending(x => x.Create_Time).ToList();

            return new ObservableCollection<Comment>(commList);
        }

        public bool UserExist(String username) 
        {
            if (db.Users.Any(i => i.Name == username))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsHeAlreadySubscribed(String username, String searchUsername) 
        {
            int u = GetUserIdByUsername(username);
            int s = GetUserIdByUsername(searchUsername);
            if (db.Subscribes.Any(i => i.User_Id == u && i.Subscribed_to_User_Id == s))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void AddSubscriber(String username, String searchUsername) 
        {
            User u = GetUserByUsername(username);
            User s = GetUserByUsername(searchUsername);

            Subscribe ss = new Subscribe()
            {
                User_Id = u.User_Id,
                Subscribed_to_User_Id = s.User_Id
            };

            u.Number_of_Subscribes++;
            s.Number_of_Subscribes++;

            db.Subscribes.Add(ss);
            db.SaveChanges();
        }

        public int GetSubscribesCount(User u) 
        {
            u.Number_of_Subscribes = 0;
            foreach (Subscribe s in db.Subscribes) 
            {
                if (s.User_Id == u.User_Id) 
                {
                    u.Number_of_Subscribes++;
                }
            }
            return u.Number_of_Subscribes;
        }

        public int GetSubscriberCount(User u)
        {
            u.Number_of_Followers = 0;
            foreach (Subscribe s in db.Subscribes)
            {
                if (s.Subscribed_to_User_Id == u.User_Id)
                {
                    u.Number_of_Followers++;
                }
            }
            return u.Number_of_Followers;
        }
    }
}
