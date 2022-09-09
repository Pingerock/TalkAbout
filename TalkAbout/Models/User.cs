using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TalkAbout.Models
{
    [Table("User")]
    public class User
    {
        [Key]
        public int User_Id { get; set; }
        public String Name { get; set; }
        public String Password { get; set; }
        public Boolean Is_Admin { get; set; }
        public int Number_of_Followers { get; set; }
        public int Number_of_Subscribes { get; set; }
        public Boolean Is_Banned { get; set; }
    }
}
