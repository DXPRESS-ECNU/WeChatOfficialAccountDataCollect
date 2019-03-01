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
    class UserCumulate
    {
        public static UserCumulate GetData(DateTime date, string token)
        {
            JObject posJObject = new JObject
            {
                {"begin_date",date.ToString("yyyy-MM-dd")},
                {"end_date",date.ToString("yyyy-MM-dd")}
            };
            string urlGetusercumulate = $"https://api.weixin.qq.com/datacube/getusercumulate?access_token={token}";
            string outGetusercumulate = Requests.HttpPost(urlGetusercumulate, posJObject.ToString());
            JObject outGetusercumulateJObject = JObject.Parse(outGetusercumulate);
            JArray usercumulateJArray = JArray.FromObject(outGetusercumulateJObject["list"]);
            UserCumulate result = JsonConvert.DeserializeObject<UserCumulate>(usercumulateJArray.First.ToString());
            return result;
        }
        [Index(0)]
        public DateTime ref_date { get; set; }
        [Index(1)]
        public int cumulate_user { get; set; }
    }
}
