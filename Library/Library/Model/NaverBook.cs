using System;
using System.IO;
using System.Net;
using Newtonsoft.Json.Linq;


namespace Library.Model
{
    class NaverBook //api는 보통 모델에 넣지않음 
    {
        private string clientId = Constant.CLIENT_ID; //털리면 안됌 -> 민감한정보는 환경변수로 따로 관리
        private string clientSecert = Constant.CLIENT_SECRET;
        public JObject GetSearchBookInformationByNaver(string query, int display)
        {
            string queryString = "query=" + query;
            string displayString = "&display=" + display;
            string url = "https://openapi.naver.com/v1/search/book.json?" + queryString + displayString; //매직넘버

            //request 
            WebRequest request = WebRequest.Create(url);
            request.Method = "GET";
            request.Headers.Add("X-Naver-Client-Id", Constant.CLIENT_ID);
            request.Headers.Add("X-Naver-Client-Secret", Constant.CLIENT_SECRET);

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
