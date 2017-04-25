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

namespace FlashCardsPort.Droid
{
    [Activity(Label = "Добавление карт",Icon = null)]
    public class Add_card_admin : Activity
    {
        public string title;
        public string cost;
        static BaseData bd = new BaseData();
        private List<Card> cards;
        private CustomAdapter adapter;
        EditText word_card, translate_card;
        ListView list_card;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.dialog_add_cards_admin);
            list_card = FindViewById<ListView>(Resource.Id.list_card);
            Decks_admin da = new Decks_admin();


            // Console.WriteLine("FFFFFFFFFFFF"+ Intent.GetStringExtra("title"));
            this.Title = Intent.GetStringExtra("title");
            
            ActionBar actionBar = ActionBar;
            actionBar.SetDisplayShowHomeEnabled(false);
            actionBar.SetDisplayHomeAsUpEnabled(true);
            cards = new List<Card>();
            List_deck();
            // Create your application here
        }
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            var inflater = MenuInflater;
            inflater.Inflate(Resource.Menu.deck_admin_actionbar, menu);
            return true;
        }
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    var intent = new Intent(this, typeof(Decks_admin));
                    StartActivity(intent);
                    break;
                case Resource.Id.item1:
                    LayoutInflater layoutInflater = LayoutInflater.From(this);
                    AlertDialog.Builder alert = new AlertDialog.Builder(this);
                    var view = layoutInflater.Inflate(Resource.Layout.add_card_admin, null);
                    word_card = (EditText) view.FindViewById(Resource.Id.word_card);
                    translate_card = (EditText) view.FindViewById(Resource.Id.translate_card);
                    alert.SetPositiveButton("Добавить", HandlePositiveButtonClick);
                    alert.SetNegativeButton("Отмена", HandleNegativeButtonClick);
                    alert.SetView(view);
                    Dialog dialog = alert.Create();
                    dialog.Show();
                    break;
                case Resource.Id.done:
                    bd.Add_deck(Intent.GetStringExtra("title"), Intent.GetStringExtra("cost"));
                    bd.Add_deck_cards(Intent.GetStringExtra("title"), cards);
                    StartActivity(typeof(Decks_admin));
                    break;
            }
            return base.OnOptionsItemSelected(item);
        }

        private void HandleNegativeButtonClick(object sender, DialogClickEventArgs e)
        {
            
        }

        private void HandlePositiveButtonClick(object sender, DialogClickEventArgs e)
        {
            cards.Add(new Card(word_card.Text, translate_card.Text));
            adapter = new CustomAdapter(this, Resource.Layout.Custom_layout, cards);
            list_card.Adapter = adapter;
        }
        public void List_deck()
        {
            //ArrayAdapter<string> adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, bd.items_deck);
            //list_card.Adapter = adapter;
        }
    }
}