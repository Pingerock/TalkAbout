using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace TalkAbout.Utils
{
    class FileService
    {
        public void WriteReportData(String path, FullReportData data, String title) 
        {
            var csv = new StringBuilder();
            csv.AppendLine(title);
            csv.AppendLine("Номер поста;Создатель поста;Дата создания поста;Заголовок поста;Текст поста;Количество лайков;Количество комментариев");
            foreach (ReportPostData item in data.PostData) 
            {
                csv.AppendLine($"{item.Id};{item.Username};{item.CreatedAt};{item.Title};{item.Text};{item.Likes};{item.Comments}");
            }
            csv.AppendLine("");
            csv.AppendLine("Дополнительная информация");
            csv.AppendLine($"Количество пользователей;{data.UserCount}");
            csv.AppendLine($"Количество постов;{data.PostCount}");
            csv.AppendLine($"Среднее количество лайков в посту;{data.AverageLikes}");
            csv.AppendLine($"Среднее количество комментариев в посту;{data.AverageComments}");

            File.WriteAllText(path, csv.ToString(), Encoding.UTF8);
        }
    }

    public class ReportPostData
    {
        public int Id { get; set; }
        public String Username { get; set; }
        public String CreatedAt { get; set; }
        public String Title { get; set; }
        public String Text { get; set; }
        public int Likes { get; set; }
        public int Comments { get; set; }
    }

    public class FullReportData
    {
        public List<ReportPostData> PostData { get; set; }
        public int PostCount { get; set; }
        public int UserCount { get; set; }
        public int AverageLikes { get; set; }
        public int AverageComments { get; set; }
    }
}
