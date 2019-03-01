using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeChatOfficialAccountDataCollect
{
    class Program
    {
        static void Main(string[] args)
        {
            string token = "";

            //Console.WriteLine(Limits.Clear("", token));
            //Console.ReadKey();

            DateTime fromdate = new DateTime(2018, 1, 1);
            DateTime enddate = new DateTime(2018, 12, 31);

            List<ArticleTotal.Article> articles = new List<ArticleTotal.Article>();
            List<UserCumulate> userCumulates = new List<UserCumulate>();
            List<UserSummary> userSummaries = new List<UserSummary>();
            try
            {
                for (DateTime date = fromdate; date <= enddate; date = date.AddDays(1))
                {
                    var at = ArticleTotal.GetData(date, token);
                    articles.AddRange(at);

                    var uc = UserCumulate.GetData(date, token);
                    userCumulates.Add(uc);

                    var us = UserSummary.GetData(date, token);
                    userSummaries.AddRange(us);

                    Console.WriteLine($"{date:MM/dd/yyyy} finish");
                }
            }
            catch
            { }
 
            using (var writer = new StreamWriter("articles.csv", true))
            using (var csvWriter = new CsvHelper.CsvWriter(writer))
            {
                writer.AutoFlush = true;
                csvWriter.WriteRecords(articles);
            }
            using (var writer = new StreamWriter("userCumulates.csv", true))
            using (var csvWriter = new CsvHelper.CsvWriter(writer))
            {
                writer.AutoFlush = true;
                csvWriter.WriteRecords(userCumulates);
            }
            using (var writer = new StreamWriter("userSummaries.csv", true))
            using (var csvWriter = new CsvHelper.CsvWriter(writer))
            {
                writer.AutoFlush = true;
                csvWriter.WriteRecords(userSummaries);
            }

            Console.WriteLine("All finish!");
            Console.ReadKey();
        }
    }
}
