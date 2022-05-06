using System;
using System.IO;
using System.Net;
using Newtonsoft.Json.Linq;


namespace Library.Model
{
    class NaverBook //api는 보통 모델에 넣지않음 
    {
        private string CientId = DataBase.GetDataBase().GetSelectedElement("client_id", Constant.TABLE_NAME_ADMINISTRATOR, Constant.TEXT_NONE);
        private string ClientSecert = DataBase.GetDataBase().GetSelectedElement("client_secret", Constant.TABLE_NAME_ADMINISTRATOR, Constant.TEXT_NONE);
        public JObject GetSearchBookInformationByNaver(string query, int display)
        {
            string url = String.Format(Constant.NAVER_SEARCH_QUERY, query, display);

            //request 
            WebRequest request = WebRequest.Create(url);
            request.Method = "GET";
            request.Headers.Add("X-Naver-Client-Id", CientId);
            request.Headers.Add("X-Naver-Client-Secret", ClientSecert);

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
