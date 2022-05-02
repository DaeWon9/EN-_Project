using System;
using System.IO;
using System.Net;
using Newtonsoft.Json.Linq;


namespace Library.Model
{
    class NaverBook
    {
        private string clientId = Constant.CLIENT_ID;
        private string clientSecert = Constant.CLIENT_SECRET;
        public JObject GetSearchBookInformationByNaver(string query, int display)
        {
            // title -> d_titl
            //jsonStr = JsonConvert.SerializeXmlNode(doc, Newtonsoft.Json.Formatting.None, true);

            string queryString = "query=" + query;
            string displayString = "&display=" + display;
            string url = "https://openapi.naver.com/v1/search/book.json?" + queryString + displayString;
            //string url = "	https://openapi.naver.com/v1/search/book_adv.xml?d_isbn = 8954763006 9788954763004";

            //request 
            WebRequest request = WebRequest.Create(url);
            request.Method = "GET";
            request.Headers.Add("X-Naver-Client-Id", clientId);
            request.Headers.Add("X-Naver-Client-Secret", clientSecert);

            WebResponse response = request.GetResponse();
            Stream responseStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(responseStream);
            JObject jsonResult = JObject.Parse(reader.ReadToEnd());

            reader.Close();
            response.Close();
            responseStream.Close();
            return jsonResult;
        }

    }
}
