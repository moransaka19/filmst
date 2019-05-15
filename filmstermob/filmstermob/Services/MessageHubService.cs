using filmstermob.Models;
using filmstermob.Views;
using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;
using Plugin.Settings;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace filmstermob.Services
{
    public class MessageHubService
    {
        private static HubConnection _connection;

        private static List<string> _messages { get; set; }

        public MessageHubService(string uniqName, string password)
        {
            _messages = new List<string>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Configs.ServerHost+Configs.Requests.RoomSignIn);

                var a = JsonConvert.SerializeObject(new { UniqName = uniqName, Password = password });

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", CrossSettings.Current.GetValueOrDefault("auth", null));
                var res = client.PostAsync("", new StringContent(a, Encoding.UTF8, "application/json")).Result;
            }

            _connection = new HubConnectionBuilder()
                .WithUrl(Configs.ServerHost+ Configs.HubEvents.Room, options =>
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
            _connection.Closed += (x) =>
            {
                _messages.Add("Disconnected");
                return Task.CompletedTask;
            };

            _connection.On<string, string>("Receive", (s, s2) => MessagingCenter.Send(this, "AddItem", new Item() { Description = $"{s}", Text=$"{s2}" }));

            _connection.On<string>("UserConnected", s => MessagingCenter.Send(this, "AddItem", new Item() { Description = $"notification", Text = $"User {s} connected" }));

            _connection.On<string>("UserDisconnected", s => MessagingCenter.Send(this, "AddItem", new Item() { Description = $"notification", Text = $"User {s} disconnected" }));

            await _connection.StartAsync();
        }

        public async Task SendMessageAsync(string message)
        {
            await _connection.SendAsync("Message", message);
        }

        public void StopHub()
        {
            Task.Run(async () =>
            {
                await _connection.StopAsync();
            });
        }

        public List<string> GetCurrStateMessages()
        {
            return _messages;
        }
    }
}
