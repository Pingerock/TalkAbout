using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TalkAbout.Models
{
    [Table("Post")]
    public class Post
    {
        [Key]
        public int Post_Id { get; set; }
        public DateTime Create_Time { get; set; }
        public int Sent_By_User_Id { get; set; }
        public int Number_of_Likes { get; set; }
        public String Title { get; set; }
        public String Text { get; set; }
        public int Number_of_Comments { get; set; }
    }
}
