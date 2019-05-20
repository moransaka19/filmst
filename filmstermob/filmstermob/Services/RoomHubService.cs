using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;
using Plugin.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace filmstermob.Services
{
    public class RoomHubService
    {
        private static HubConnection _connection;

        public RoomHubService(string uniqName, string password)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Configs.ServerHost + Configs.Requests.RoomSignIn);

                var a = JsonConvert.SerializeObject(new { UniqName = uniqName, Password = password });

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", CrossSettings.Current.GetValueOrDefault("auth", null));
                var res = client.PostAsync("", new StringContent(a, Encoding.UTF8, "application/json")).Result;
            }

            _connection = new HubConnectionBuilder()
                .WithUrl(Configs.ServerHost + Configs.HubEvents.Room, options =>
                {
                    options.AccessTokenProvider = () =>
                    {
                        return Task.FromResult(CrossSettings.Current.GetValueOrDefault("auth", null));
                    };
                })
                .Build();
        }

        public async Task InitListen()
        {
            _connection.On<IEnumerable<filmstermob.Models.Media>, string>("RequireMedia", (models, id) =>
            {
                Console.WriteLine($"Medias to update: {models.Count()}. For user: {id}");

                for (var i = 0; i < models.Count(); i++)
                {
                    Console.WriteLine($"Upload media{i + 1}");
                    _connection.SendAsync("UploadMedia", id, $"fileName{i + 1}", new List<string>(new[] { "chunk1", "chunk2" }));
                }
            });

            _connection.On<string, IEnumerable<string>>("DownloadMedia", (fileName, chunks) =>
            {
                Console.WriteLine($"Start download {fileName} from {chunks.First()}, {chunks.Last()}");

                Thread.Sleep(3000);

                _connection.SendAsync("MediaDownloaded");
            });

            _connection.On("ReadyToPlay", () => Console.WriteLine("Ready to play. Mission completed nahren!"));

            _connection.On("Play", () => Console.WriteLine("Play"));

            _connection.On("Pause", () => Console.WriteLine("Pause"));

            _connection.On("Stop", () => Console.WriteLine("Stop"));

            await _connection.StartAsync();
        }

        public void StopHub()
        {
            Task.Run(async () =>
            {
                await _connection.StopAsync();
            });
        }
    }
}
