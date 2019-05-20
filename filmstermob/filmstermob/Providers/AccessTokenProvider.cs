using filmstermob.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace filmstermob.Providers
{
    public class AccessTokenProvider
    {

        public static async Task<string> GetToken(string userName, string password)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Configs.ServerHost+Configs.Requests.Login);

                var loginData = JsonConvert.SerializeObject(new { UserName = userName, Password = password });

                var res = await client.PostAsync(Configs.ServerHost + Configs.Requests.Login, new StringContent(loginData, Encoding.UTF8, "application/json"));

                var resoinseJson = await res.Content.ReadAsStringAsync();

                var respModel = JsonConvert.DeserializeObject<AuthModel>(resoinseJson);

                return respModel.AccessToken;
            }
        }

    }
}
