using Android.App;
using Android.Widget;
using Android.OS;
using Android.Util;
using Android.Gms.Common;
using Android.Views;
using Android.Runtime;

using System;
using Android.Content;
using ClientApp;

using System.Xml;

//using System.ServiceModel.Syndication;
using System.IO;
using Android.Webkit;
using System.Text;
//using System.Xml.Linq;
using Android.Text;
using Android.Graphics;
using System.Collections.Generic;

using Android.Support.V4.Widget;
using System.Threading.Tasks;
using static Android.Widget.AbsListView;

namespace RemoteNotification
{
    
    [Activity(Label = "RemoteNotification", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        //XNamespace nsYandex = "http://news.yandex.ru";
        //string it;
        TextView msgText;
        TextView fullNews;
        int count;
        string inputURI = "http://www.obrbank.ru/media/rss";
        string[] items;
        public string[] news;
        public int index;
        string names;
        RSSparser parser;
        Intent intent1;

        private ListView list;
        private Toolbar toolbar;
        List<Article> articles;

        SwipeRefreshLayout refresher;

        public class OnScrollListener : AbsListView.IOnScrollListener
        {
            private int currentVisibleItemCount;
            private int currentScrollState;
            private int currentFirstVisibleItem;
            private int totalItem;
            private LinearLayout lBelow;

            public IntPtr Handle
            {
                get
                {
                    throw new NotImplementedException();
                }
            }

            public void Dispose()
            {
                throw new NotImplementedException();
            }

            public void OnScroll(AbsListView view, int firstVisibleItem, int visibleItemCount, int totalItemCount)
            {
                this.currentFirstVisibleItem = firstVisibleItem;
                this.currentVisibleItemCount = visibleItemCount;
                this.totalItem = totalItemCount;
                if (totalItem - currentFirstVisibleItem == currentVisibleItemCount
                       && this.currentScrollState == (int)ScrollState.Idle)
                {
                    /** To do code here*/

               //    Android.Widget.Toast.MakeText(, "zopo", Android.Widget.ToastLength.Short).Show();


                }
            }

            public void OnScrollStateChanged(AbsListView view, [GeneratedEnum] ScrollState scrollState)
            {
                this.currentScrollState = (int)scrollState;
                //   this.isScrollCompleted();
            }

            //public async void isScrollCompleted()
            //{
            //    if (totalItem - currentFirstVisibleItem == currentVisibleItemCount
            //            && this.currentScrollState == (int)ScrollState.Idle)
            //    {
            //        /** To do code here*/
            //        await LoadNews();

            //    }
            //}
        }
        async void HandleRefresh(object sender, EventArgs e)
        {
            await LoadNews();
            refresher.Refreshing = false;
        }

        public async Task LoadNews()
        {
            if (parser.getNewArticles(inputURI))
            {
                // Android.Widget.Toast.MakeText(this, nsYandex.NamespaceName, Android.Widget.ToastLength.Short).Show();
                names = parser.channel.names;
                foreach (var item in parser.articles)
                {
                    if (count < 10)
                    {
                        items[count] = item.title;
                        news[count] = item.description;
                        articles.Add(new Article() { Title = item.title, FullNews = item.description, ImageResourceId = Resource.Drawable.Icon });
                        count++;
                    }
                }

                list.Adapter = new ArticleAdapter(this, articles);

            }
            else
            {
                Android.Widget.Toast.MakeText(this, "zopo", Android.Widget.ToastLength.Short).Show();
            }
        }
        protected override void OnCreate(Bundle bundle)
        {
           
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);

            intent1 = new Intent(this, typeof(Activity1));
            parser = new RSSparser();
            articles = new List<Article>();
            items = new string[10];
            news = new string[10];


            toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetActionBar(toolbar);
            ActionBar.Title = "";

       

            list = FindViewById<ListView>(Resource.Id.List);

            list.ItemClick += (sender, e) =>
            {
                //SyndicationItem it;
                var it = news[e.Position];
                var t = items[e.Position];
                index = e.Position;
                intent1.PutExtra("index", news[e.Position]);
                StartActivity(intent1);
            };
            
            //list.SetOnScrollListener(new OnScrollListener());

            refresher = FindViewById<SwipeRefreshLayout>(Resource.Id.refresher);
            refresher.SetColorScheme(Resource.Color.xam_dark_blue,Resource.Color.xam_purple,Resource.Color.xam_gray, Resource.Color.xam_green);
            refresher.Refresh += HandleRefresh;

       
            var intent = new Intent(this, typeof(RegistrationIntentService));
                StartService(intent);
            
            LoadNews();

        }

       

        
        

        public bool IsPlayServicesAvailable()
        {
            int resultCode = GoogleApiAvailability.Instance.IsGooglePlayServicesAvailable(this);
            if (resultCode != ConnectionResult.Success)
            {
                if (GoogleApiAvailability.Instance.IsUserResolvableError(resultCode))
                    msgText.Text = GoogleApiAvailability.Instance.GetErrorString(resultCode);
                else
                {
                    msgText.Text = "Sorry, this device is not supported1";
                    Finish();
                }
                return false;
            }
            else
            {
                msgText.Text = "Google Play Services is available1.";
                return true;
            }
        }
    }
}

