using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TalkAbout.Models
{
    [Table("Comment")]
    public class Comment
    {
        [Key]
        public int Comment_Id { get; set; }
        public int Sent_by_User_Id { get; set; }
        public int Post_Id { get; set; }
        public String Text { get; set; }
        public DateTime Create_Time { get; set; }
        public int Number_of_Likes { get; set; }
    }
}
