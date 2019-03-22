using Newtonsoft.Json;
using SyncPlayer.Models;
using System.IO;
using System.Net;

namespace SyncPlayer.Helpers
{
    //TODO: Make it generic type;
    //TODO: Replace it to SharedKernel
	class HttpHelper
    {

        public void HttpPost(ApplicationUser user)
        {
            string json = JsonConvert.SerializeObject(user);
            var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://url");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
            }
        }

        public ApplicationUser HttpGet()
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://url");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "GET";

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            var result = string.Empty;
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                result = streamReader.ReadToEnd();
            }
            ApplicationUser user = new ApplicationUser();
            user = JsonConvert.DeserializeObject<ApplicationUser>(result);
            return user;
        }
    }
}
