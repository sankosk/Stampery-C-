using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
[assembly: InternalsVisibleTo("ApiTest")]

namespace API
{

    public class Client
    {

        public string ApiKey { get; private set; }
        public string EndPoint { get; private set; }

        public Client(string apiKey, bool beta=false) {
            ApiKey = apiKey;
            if (!beta)   EndPoint = "https://api.stampery.com/v2";
            else         EndPoint = "https://stampery-api-beta.herokuapp.com/v2";
        }

        public string GetStamp(string stampHash) {
            WebRequest req = WebRequest.Create(EndPoint + "/stamps/" + stampHash);
            WebResponse resp = req.GetResponse();

            var httpResponse = (HttpWebResponse)req.GetResponse();
            bool success = ((int)httpResponse.StatusCode) >= 200 && ((int)httpResponse.StatusCode) < 300;
            if (!success) throw new WebException("Error sending the request");

            StreamReader reader = new StreamReader(resp.GetResponseStream());
            string responseFromServer = reader.ReadToEnd();
            return responseFromServer;
        }

        public string StampFile(Dictionary<string, string> data, string filePath) {
            WebClient web = new WebClient();
            web.Headers.Add("Authorization", "Basic " + GetAuth());
            NameValueCollection parameters = new NameValueCollection();
            foreach (var x in data) {
                parameters.Add(x.Key, x.Value);
            }

            web.QueryString = parameters;
            var responseBytes = web.UploadFile(EndPoint + "/stamps/", filePath);

            string response = Encoding.ASCII.GetString(responseBytes);
            return response;
        }

        public string StampData(string data) {
            if (data == "" || data.Equals("")) throw new ArgumentException("Empty data");

            var httpWebRequest = (HttpWebRequest)WebRequest.Create(EndPoint + "/stamps/");
            httpWebRequest.Headers.Add("Authorization", "Basic " + GetAuth());
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                streamWriter.Write(data);
                streamWriter.Flush();
                streamWriter.Close();
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            bool success = ((int)httpResponse.StatusCode) >= 200 && ((int)httpResponse.StatusCode) < 300;
            if (!success) throw new WebException("Error sending the request");

            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                return streamReader.ReadToEnd();
            }
        }

        internal string GetAuth() {
            return HashUtils.GetBase64(HashUtils.GetMD5(ApiKey).ToLower().Substring(0, 15) + ":" + ApiKey);
        }
    }

    public static class HashUtils
    {
        public static string GetSHA256(string toHash) {
            return default(string);
        }

        public static string GetMD5(string toHash) {
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] bytesToConvert = System.Text.Encoding.ASCII.GetBytes(toHash);
                byte[] hashedBytes = md5.ComputeHash(bytesToConvert);

                StringBuilder sb = new StringBuilder();
                foreach (byte i in hashedBytes) sb.Append(i.ToString("X2"));
                return sb.ToString();
            }
        }

        public static string GetBase64(string toHash) {
            return System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(toHash));;
        }
    }
}
