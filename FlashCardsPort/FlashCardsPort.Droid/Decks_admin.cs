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
using MySql.Data.MySqlClient;
using FlashCardsPort;
using System.Runtime.Remoting.Contexts;

namespace FlashCardsPort.Droid
{
    [Activity(Label = "Decks")]
    public class Decks_admin : Activity
    {
        static BaseData bd = new BaseData();
        TextView email;
        ListView list_deck;
        Button ok;
        EditText input;
        EditText cost;
        EditText title;
        LayoutInflater inflater;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.decks);

            email = FindViewById<TextView>(Resource.Id.email);
            list_deck = FindViewById<ListView>(Resource.Id.list);
            ActionBar actionBar = ActionBar;
            actionBar.SetDisplayShowHomeEnabled(true);
            actionBar.SetDisplayHomeAsUpEnabled(true);
            List_deck();          
            // email.Text = "1";
            //  }
        }

        protected void onRestart()
        {
            base.OnRestart();
        }
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            var inflater = MenuInflater;
            inflater.Inflate(Resource.Menu.main, menu);
            return true;
        }
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    var intent = new Intent(this, typeof(MainActivity));
                    StartActivity(intent);
                    break;
                case Resource.Id.item1:
                    LayoutInflater layoutInflater = LayoutInflater.From(this);
                    AlertDialog.Builder alert = new AlertDialog.Builder(this);
                    var view = layoutInflater.Inflate(Resource.Layout.dialog_add_deck_admin, null);
                    //  input = new EditText(this);
                    title = (EditText) view.FindViewById(Resource.Id.title);
                    cost = (EditText)  view.FindViewById(Resource.Id.cost);
                    alert.SetPositiveButton("Добавить", HandlePositiveButtonClick);
                    alert.SetNegativeButton("Отмена", HandleNegativeButtonClick);                    
                    alert.SetView(view);
                    Dialog dialog = alert.Create();
                    dialog.Show();

                    //FragmentTransaction transaction = FragmentManager.BeginTransaction();
                    //  Add_deck_admin add_deck_admin = new Add_deck_admin();
                    // add_deck_admin.Show(transaction, "add_deck");
                    break;
            }
            return base.OnOptionsItemSelected(item);
        }

        private void HandleNegativeButtonClick(object sender, DialogClickEventArgs e)
        {
         //    
        }

        private void HandlePositiveButtonClick(object sender, EventArgs e)
        {
            bd.Add_deck(title.Text, cost.Text);
            List_deck();
        }
       
 
        public void List_deck()
        {
            bd.Decks_list();
            ArrayAdapter<string> adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, bd.items_deck);
            list_deck.Adapter = adapter;
        }
        
    }
}
