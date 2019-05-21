using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using filmstermob.Models;
using filmstermob.Views;
using System.Collections.Generic;
using filmstermob.Services;
using System.Linq;
using filmstermob.Views.Settings;
using filmstermob.Views.Room;

namespace filmstermob.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        public ObservableCollection<Item> Items { get; set; }
        public Command LoadItemsCommand { get; set; }

        public ItemsViewModel()
        {
            Title = "Browse";
            Items = new ObservableCollection<Item>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            MessagingCenter.Subscribe<NewItemPage, Item>(this, "AddItem", async (obj, item) =>
            {
                var newItem = item as Item;
                Items.Add(newItem);
                await DataStore.AddItemAsync(newItem);
            });
            
            MessagingCenter.Subscribe<ChatPage, Item>(this, "AddItem", async (obj, item) =>
            {
                var newItem = item as Item;
                Items.Add(newItem);
                await DataStore.AddItemAsync(newItem);
            });

            MessagingCenter.Subscribe<RoomPage, Item>(this, "AddItem", async (obj, item) =>
            {
                var newItem = item as Item;
                Items.Add(newItem);
                await DataStore.AddItemAsync(newItem);
            });

            MessagingCenter.Subscribe<SettingsPage, ObservableCollection<Item>>(this, "AddItem", async (obj, item) =>
            {
                Items = item;
                List<Item> refreshItems = item.Select(x => x).ToList();
                await DataStore.RefreshItems(refreshItems);
            });

            MessagingCenter.Subscribe<FileEditPage, Item>(this, "DeleteItem", async (obj, item) =>
                {
                    Items.Remove(item);
                    await DataStore.DeleteItemAsync(item);
                });

            MessagingCenter.Subscribe<MessageHubService, Item>(this, "AddItem", async (obj, item) =>
            {
                var newItem = item as Item;
                Items.Add(newItem);
                await DataStore.AddItemAsync(newItem);
            });
        }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Items.Clear();
                var items = await DataStore.GetItemsAsync(true);
                foreach (var item in items)
                {
                    Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}