using filmstermob.Models;
using Newtonsoft.Json;
using Plugin.Settings;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace filmstermob.Services
{
    public class RoomService
    {
        public RoomService()
        {

        }

        public static async Task<HttpStatusCode> RoomSignin(string uniqName, string password)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Configs.ServerHost + Configs.Requests.RoomSignIn);

                var loginData = JsonConvert.SerializeObject(new { UniqName = uniqName, Password = password });

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", CrossSettings.Current.GetValueOrDefault("auth", null));
                var res = await client.PostAsync("", new StringContent(loginData, Encoding.UTF8, "application/json"));
                return res.StatusCode;
            }
        }

        public static async Task<HttpStatusCode> RoomCreate(Room room)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Configs.ServerHost + Configs.Requests.RoomCreate);

                var loginData = JsonConvert.SerializeObject(room);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", CrossSettings.Current.GetValueOrDefault("auth", null));
                var res = await client.PostAsync("", new StringContent(loginData, Encoding.UTF8, "application/json"));
                return res.StatusCode;
            }
        }

        public static async Task<HttpStatusCode> RoomSignOut()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Configs.ServerHost + Configs.Requests.RoomSignOut);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", CrossSettings.Current.GetValueOrDefault("auth", null));
                var res = await client.GetAsync("");
                return res.StatusCode;
            }
        }
    }
}
