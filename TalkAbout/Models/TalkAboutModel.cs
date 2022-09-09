using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace TalkAbout.Models
{
    public class TalkAboutModel : DbContext
    {
        public TalkAboutModel() : base("TalkAboutModelka") 
        {

        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Subscribe> Subscribes { get; set; }
        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<LikedComment> LikedComments { get; set; }
        public virtual DbSet<LikedPost> LikedPosts { get; set; }
    }
}
