using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace WeChatOfficialAccountDataCollect
{
    class Limits
    {
        public static string Clear(string appid ,string token)
        {
            JObject posJObject = new JObject
            {
                {"appid",appid}
            };
            string urlClear_quota = $" https://api.weixin.qq.com/cgi-bin/clear_quota?access_token={token}";
            return Requests.HttpPost(urlClear_quota, posJObject.ToString());
        }
    }
}
