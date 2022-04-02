using Gardenia.DTOs;
using Gardenia.Interfaces;
using Gardenia.Sittings;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;

namespace Gardenia.Repositories
{
    public class SmsService : ISmsService
    {
        private readonly SmsSettings _smsSettings;
        public SmsService(IOptions<SmsSettings> smsSettings)
        {
            _smsSettings = smsSettings.Value;
        }


        public async Task<string> SendSmsAsync(SmsRequest smsRequest)
        {


            
            var sendtime = DateTime.Now.ToString("yyyy-MM-dd-HH-mm");
            

            var url = "https://smsmisr.com/" +              //HttpResponseMessage
            "api/webapi/?" +
            "username=" + _smsSettings.UserName +           //  
            "&password=" + _smsSettings.Password +          //  
            "&language=" + _smsSettings.Language +          // 3 Or 2 Or 1
            "&sender=" + _smsSettings.SenderId +            //  
            "&mobile=" + smsRequest.ToPhoneNumber +         // " +                     //2012XXXXXX, 2011XXXX" +
            "&message=" + HttpUtility.UrlEncode(smsRequest.Body) +
            "&DelayUntil=" + sendtime;


            var httpRequest = (HttpWebRequest)WebRequest.Create(url);
            httpRequest.Method = "POST";

            httpRequest.ContentType = "application/json";
            httpRequest.Headers["Content-Length"] = "0";


            var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();

                var resultDetails = JObject.Parse(result);
                /*Console.WriteLine(httpResponse.StatusCode);
                Debug.AutoFlush = true;
                Debug.WriteLine("#####################################################################################################################");
                Debug.WriteLine(resultDetails["code"] + "     " + smsRequest.Body);  //
                Debug.WriteLine("#####################################################################################################################");*/
                return resultDetails["code"].ToString();
            }
        }
    }
}
