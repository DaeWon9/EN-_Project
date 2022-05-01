using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Xml;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace Library.Controller
{
    class NaverBook
    {
        private string clientId = Constant.CLIENT_ID;
        private string clientSecert = Constant.CLIENT_SECRET;
        public void SearchBookByNaver()
        {
            // title -> d_titl

            string query = "";
            string url = "https://openapi.naver.com/v1/search/book.json?query=이것이&display=5";

            //request 
            WebRequest request = WebRequest.Create(url);
            request.Method = "GET";
            request.Headers.Add("X-Naver-Client-Id", clientId);
            request.Headers.Add("X-Naver-Client-Secret", clientSecert);

            WebResponse response = request.GetResponse();
            Stream responseStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(responseStream);
            JObject jsonresult = JObject.Parse(reader.ReadToEnd());


            for (int repeat = 0; repeat < 5; repeat++)
            {
                Console.WriteLine(jsonresult["items"][repeat]["title"]);
            }
            Console.ReadKey();

        }
    }
}
