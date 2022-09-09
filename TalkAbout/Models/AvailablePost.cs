using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TalkAbout.Models
{
    public class AvailablePost
    {
        public int Post_Id { get; set; }
        public DateTime Create_Time { get; set; }
        public int Sent_By_User_Id { get; set; }
        public String Title { get; set; }
        public String Text { get; set; }
        public int Number_of_Likes { get; set; }
        public int Number_of_Comments { get; set; }
        public virtual User User { get; set; }
    }
}
