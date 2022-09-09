using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TalkAbout.Models
{
    [Table("LikedPost")]
    public class LikedPost
    {
        [Key]
        public int Id { get; set; }
        public int User_Id { get; set; }
        public int Post_Id { get; set; }
    }
}
