using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Net;
using FlashCardsPort;


namespace FlashCardsPort.Droid
{
	[Activity (Label = "FlashCards.Droid", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
        int count = 1;
        static BaseData bd = new BaseData();
        TextView txtlog;
        EditText txtpass, txtemail;
        Button button;
        protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);

			// Get our button from the layout resource,
			// and attach an event to it
			button = FindViewById<Button> (Resource.Id.Register);
            txtemail = FindViewById<EditText>(Resource.Id.email);
            txtpass = FindViewById<EditText>(Resource.Id.pass);
            txtlog = FindViewById<TextView>(Resource.Id.log);

            button.Click += Register_user;
		}
        
        private void Register_user(object sender, EventArgs e)
        {           
           // bd.User_Registration(txtemail.Text,txtpass.Text);
            StartActivity(typeof(Decks_admin));
        }
    }
}


