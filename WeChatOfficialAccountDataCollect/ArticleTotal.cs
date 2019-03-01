using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper.Configuration.Attributes;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WeChatOfficialAccountDataCollect
{
    class ArticleTotal
    {
        public static List<Article> GetData(DateTime date, string token)
        {
            string urlGetArticleTotal = $"https://api.weixin.qq.com/datacube/getarticletotal?access_token={token}";
            JObject posJObject = new JObject
            {
                {"begin_date",date.ToString("yyyy-MM-dd")},
                {"end_date",date.ToString("yyyy-MM-dd")}
            };
            string outJson = Requests.HttpPost(urlGetArticleTotal, posJObject.ToString());
            JObject outJObject = JObject.Parse(outJson);
            JArray dataJArray = JArray.FromObject(outJObject["list"]);
            var result = new List<Article>();
            foreach (var article in dataJArray.Children())
            {
                var articles = JsonConvert.DeserializeObject<ArticleTotal>(article.ToString());
                foreach (Article art in articles.details)
                {
                    art.ref_date = articles.ref_date;
                    art.msgid = articles.msgid;
                    art.title = articles.title;
                    result.Add(art);
                }  
            }
            return result;
        }
        public DateTime ref_date { get; set; }
        public string msgid { get; set; }
        public string title { get; set; }
        public List<Article> details { get; set; }
        public class Article
        {
            [Index(0)]
            public DateTime ref_date { get; set; }
            [Index(1)]
            public string msgid { get; set; }
            [Index(2)]
            public string title { get; set; }
            [Index(3)]
            public DateTime stat_date { get; set; }
            public int target_user { get; set; }
            public int int_page_read_user { get; set; }
            public int int_page_read_count { get; set; }
            public int ori_page_read_user { get; set; }
            public int ori_page_read_count { get; set; }
            public int share_user { get; set; }
            public int share_count { get; set; }
            public int add_to_fav_user { get; set; }
            public int add_to_fav_count { get; set; }
            public int int_page_from_session_read_user { get; set; }
            public int int_page_from_session_read_count { get; set; }
            public int int_page_from_hist_msg_read_user { get; set; }
            public int int_page_from_hist_msg_read_count { get; set; }
            public int int_page_from_feed_read_user { get; set; }
            public int int_page_from_feed_read_count { get; set; }
            public int int_page_from_friends_read_user { get; set; }
            public int int_page_from_friends_read_count { get; set; }
            public int int_page_from_other_read_user { get; set; }
            public int int_page_from_other_read_count { get; set; }
            public int feed_share_from_session_user { get; set; }
            public int feed_share_from_session_cnt { get; set; }
            public int feed_share_from_feed_user { get; set; }
            public int feed_share_from_feed_cnt { get; set; }
            public int feed_share_from_other_user { get; set; }
            public int feed_share_from_other_cnt { get; set; }
        }
    }

}
