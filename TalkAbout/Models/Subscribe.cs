using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TalkAbout.Models
{
    [Table("Subscribe")]
    public class Subscribe
    {
        [Key]
        public int Id { get; set; }
        public int User_Id { get; set; }
        public int Subscribed_to_User_Id { get; set; }
    }
}
