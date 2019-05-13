using System;
using System.Threading.Tasks;
using Android;
using Android.App;
using Android.Content;
using Android.Media;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V4.View;
using Android.Support.V4.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Plugin.FilePicker;
using Plugin.FilePicker.Abstractions;
using Plugin.MediaManager;
using Plugin.MediaManager.Abstractions.Enums;
using Plugin.MediaManager.ExoPlayer;
using Plugin.MediaManager.MediaSession;

namespace filmstmob
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity, NavigationView.IOnNavigationItemSelectedListener
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);
            Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            

            ((MediaManagerImplementation)CrossMediaManager.Current).MediaSessionManager = new MediaSessionManager(Application.Context, typeof(ExoPlayerAudioService));
            var exoPlayer = new ExoPlayerAudioImplementation(((MediaManagerImplementation)CrossMediaManager.Current).MediaSessionManager);
            CrossMediaManager.Current.AudioPlayer = exoPlayer;

            //CrossMediaManager.Current.Play("https://archive.org/download/BigBuckBunny_328/BigBuckBunny_512kb.mp4", MediaFileType.Video);

            //ViewStub stub = (ViewStub)FindViewById(Resource.Id.layout_stub);
            //stub.LayoutResource = Resource.Layout.Login;
            //stub.Inflate();

            FloatingActionButton fab = FindViewById<FloatingActionButton>(Resource.Id.fab);
            fab.Click += FabOnClick;

            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            ActionBarDrawerToggle toggle = new ActionBarDrawerToggle(this, drawer, toolbar, Resource.String.navigation_drawer_open, Resource.String.navigation_drawer_close);
            drawer.AddDrawerListener(toggle);
            toggle.SyncState();

            NavigationView navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
            navigationView.SetNavigationItemSelectedListener(this);

            //var player = MediaPlayer.Create(this, Resource.Raw.testAudio);
            //Button button = FindViewById<Button>(Resource.Id.testBut);
            //button.Click += delegate
            //{
            //    player.Start();
            //};
            //var videoView = FindViewById<VideoView>(Resource.Id.videoView1);
            //var uri = Android.Net.Uri.Parse("https://www.youtube.com/watch?v=wg-kEWsL6Xc");
            //videoView.SetVideoURI(uri);
            //videoView.Start();
        }

        public override void OnBackPressed()
        {
            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            if(drawer.IsDrawerOpen(GravityCompat.Start))
            {
                drawer.CloseDrawer(GravityCompat.Start);
            }
            else
            {
                base.OnBackPressed();
            }
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            int id = item.ItemId;
            if (id == Resource.Id.action_settings)
            {
                return true;
            }

            return base.OnOptionsItemSelected(item);
        }

        private void FabOnClick(object sender, EventArgs eventArgs)
        {
            View view = (View) sender;
            Snackbar.Make(view, "Replace with your own action", Snackbar.LengthLong)
                .SetAction("Action", (Android.Views.View.IOnClickListener)null).Show();
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            switch (requestCode)
            {
                case 9999:
                    var test =  data.Data.ToString();
                    break;
            }
        }

        public bool OnNavigationItemSelected(IMenuItem item)
        {
            int id = item.ItemId;

            if (id == Resource.Id.nav_camera)
            {
                StartActivity(typeof(LoginActivity));
                return true;
                // Handle the camera action
            }
            else if (id == Resource.Id.nav_gallery)
            {
                Intent i = new Intent(Intent.ActionOpenDocumentTree);
                
                i.AddCategory(Intent.CategoryDefault);
                StartActivityForResult(Intent.CreateChooser(i, "Choose directory"), 9999);
            }
            else if (id == Resource.Id.nav_slideshow)
            {

            }
            else if (id == Resource.Id.nav_manage)
            {

            }
            else if (id == Resource.Id.nav_share)
            {

            }
            else if (id == Resource.Id.nav_send)
            {

            }

            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            drawer.CloseDrawer(GravityCompat.Start);
            return true;
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}

