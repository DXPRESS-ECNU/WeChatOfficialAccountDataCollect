using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper.Configuration.Attributes;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WeChatOfficialAccountDataCollect
{
    class UserSummary
    {
        public static List<UserSummary> GetData(DateTime date, string token)
        {
            var result = new List<UserSummary>();
            JObject posJObject = new JObject
            {
                {"begin_date",date.ToString("yyyy-MM-dd")},
                {"end_date",date.ToString("yyyy-MM-dd")}
            };
            string urlGetusersummary = $"https://api.weixin.qq.com/datacube/getusersummary?access_token={token}";
            string outGetusersummary = Requests.HttpPost(urlGetusersummary, posJObject.ToString());
            JObject outGetusersummaryJObject = JObject.Parse(outGetusersummary);
            JArray usersummaryJArray = JArray.FromObject(outGetusersummaryJObject["list"]);
            foreach (var usJ in usersummaryJArray)
            {
                UserSummary us = JsonConvert.DeserializeObject<UserSummary>(usJ.ToString());
                result.Add(us);
            }
            return result;
        }
        [Index(0)]
        public DateTime ref_date { get; set; }
        [Index(0)]
        public int user_source { get; set; }
        [Index(0)]
        public int new_user { get; set; }
        [Index(0)]
        public int cancel_user { get; set; }
    }
}
