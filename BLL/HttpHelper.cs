using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;


namespace BLL
{
    static public class HttpHelper
    {


        static public string GetAsync(string url, string roomToken)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new
                        AuthenticationHeaderValue("Bearer", roomToken);
                    //var responce = await client.GetAsync(url);
                    //return await responce.Content.ReadAsStringAsync();
                    var response = client.GetStringAsync(url).Result;
                    return response;
                }


            }
            catch
            {
                
                return null;
            }
        }

        static public async Task<string> PostAsync(string url, string userToken, Dictionary<string, string> data)
        {
            if (!await CheckConnection())
                throw new Exception();

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new
                    AuthenticationHeaderValue("Bearer", userToken);

                var response = await client.PostAsync(url, new FormUrlEncodedContent(data));

                return await response.Content.ReadAsStringAsync();
            }
        }

        static public async Task<string> PostAsync(string url,string userToken, string json)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new
                        AuthenticationHeaderValue("Bearer", userToken);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

                    var response = await client.PostAsync(url, content);

                    return await response.Content.ReadAsStringAsync();
                }
            }
            catch
            {
                
                return null;
            }
        }

        static public async Task<string> DeleteAsync(string url, string roomToken)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new
                        AuthenticationHeaderValue("Bearer", roomToken);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var response = await client.DeleteAsync(url);

                    return await response.Content.ReadAsStringAsync();
                }
            }
            catch
            {
                
                return null;
            }
        }

        static public async Task<string> PutAsync(string url, string roomToken, string json)
        {
            try
            {

                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new
                        AuthenticationHeaderValue("Bearer", roomToken);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

                    var response = await client.PutAsync(url, content);

                    return await response.Content.ReadAsStringAsync();
                }
            }
            catch
            {
                
                return null;
            }
        }

        static public async Task<bool> CheckConnection()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var response = await client.GetAsync("http://online.swagger.io/validator/debug?url=http://fitnessap.azurewebsites.net:80/swagger/docs/v1");

                    if (response.StatusCode != HttpStatusCode.OK)
                        throw new Exception();
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
