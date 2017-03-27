using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Webkit;
using Android.Text;

namespace RemoteNotification
{
    [Activity(Label = "Activity1")]
    public class Activity1 : Activity
    {
        Toolbar toolbar;
        TextView text;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.FullNews);

            toolbar = FindViewById<Toolbar>(Resource.Id.fullNewsToolbar);
            SetActionBar(toolbar);
            
            ActionBar.Title = "";

            ActionBar.SetDisplayHomeAsUpEnabled(true);
            ActionBar.SetHomeButtonEnabled(true);

         //  text = FindViewById<TextView>(Resource.Id.flnws);
           // text.SetText(Html.FromHtml(Intent.GetStringExtra("index")),TextView.BufferType.Normal);
            WebView web = FindViewById<WebView>(Resource.Id.webView1);
           var inten= new Intent();

             web.LoadDataWithBaseURL(null,Intent.GetStringExtra("index"), "text/html", "UTF-8", "about:blank");
            // Create your application here

        }
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (item.ItemId == Android.Resource.Id.Home)
                Finish();

            return base.OnOptionsItemSelected(item);
        }
    }
}