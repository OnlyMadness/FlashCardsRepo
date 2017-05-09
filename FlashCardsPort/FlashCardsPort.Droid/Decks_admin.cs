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
using Android.Webkit;
using System.Net;

namespace FlashCardsPort.Droid
{
    [Activity(Label = "������")]
    public class Decks_admin : Activity
    {
        static BaseData bd = new BaseData();
        TextView email;
        ListView list_deck;
        Button ok,edit_item,delete_item;
        EditText input;
        public string delete_title, deck_cost;
        public string title_text;
        public ArrayAdapter<string> adapter, adapter_deck_cost;
        EditText cost;
        EditText title;
        LayoutInflater inflater;
        public Dialog dialog;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            bd.connection();
            
            SetContentView(Resource.Layout.decks);
            list_deck = FindViewById<ListView>(Resource.Id.list);
            list_deck.ItemLongClick += delete_edit_item;
            list_deck.ItemClick += action_item;
            ActionBar actionBar = ActionBar;
            actionBar.SetDisplayHomeAsUpEnabled(true);
            List_deck();
            // email.Text = "1";
            //  }
        }
        private void action_item(object sender, AdapterView.ItemClickEventArgs e)
        {
            Intent intent = new Intent(this, typeof(Cards_deck));
            intent.PutExtra("title_deck", adapter.GetItem(e.Position));
            StartActivity(intent);
        }
        void delete_edit_item(object sender, AdapterView.ItemLongClickEventArgs e)
        {            
            delete_title = adapter.GetItem(e.Position);
            deck_cost = adapter_deck_cost.GetItem(e.Position);
            LayoutInflater layoutInflater = LayoutInflater.From(this);
            AlertDialog.Builder alert = new AlertDialog.Builder(this);
            var view = layoutInflater.Inflate(Resource.Layout.edit_delete_item, null);
            //  input = new EditText(this);
            edit_item = (Button)view.FindViewById(Resource.Id.edit_item);
            delete_item = (Button)view.FindViewById(Resource.Id.delete_item);
            delete_item.Click += delete_item_click;
            edit_item.Click += edit_item_click;
            alert.SetView(view);
            dialog = alert.Create();
            dialog.Show();
        }

        private void edit_item_click(object sender, EventArgs e)
        {
            LayoutInflater layoutInflater = LayoutInflater.From(this);
            AlertDialog.Builder alert = new AlertDialog.Builder(this);
            var view = layoutInflater.Inflate(Resource.Layout.dialog_add_deck_admin, null);
            title = (EditText)view.FindViewById(Resource.Id.title_deck);
            cost = (EditText)view.FindViewById(Resource.Id.Cost_deck);
            title.Text = delete_title ;
            cost.Text = deck_cost;
            alert.SetPositiveButton("�����", HandlePositiveButtonClick);
            alert.SetNegativeButton("������", HandleNegativeButtonClick);
            alert.SetView(view);
            dialog = alert.Create();
            dialog.Show();
        }

        private void delete_item_click(object sender, EventArgs e)
        {
            bd.delete_item_list(delete_title);        
            dialog.Hide();
            List_deck();
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
                    var intent = new Intent(this, typeof(Main_menu_admin));
                    StartActivity(intent);
                    break;
                case Resource.Id.item1:
                    LayoutInflater layoutInflater = LayoutInflater.From(this);
                    AlertDialog.Builder alert = new AlertDialog.Builder(this);
                    var view = layoutInflater.Inflate(Resource.Layout.dialog_add_deck_admin, null);
                    //  input = new EditText(this);
                    title = (EditText)view.FindViewById(Resource.Id.title_deck);
                    cost = (EditText)view.FindViewById(Resource.Id.Cost_deck);
                    alert.SetPositiveButton("�����", HandlePositiveButtonClick);
                    alert.SetNegativeButton("������", HandleNegativeButtonClick);
                    alert.SetView(view);
                    dialog = alert.Create();
                    dialog.Show();

                    //var intent2 = new Intent(this, typeof(Add_deck_admin));
                    //StartActivity(intent2);


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
            //bd.Add_deck(title.Text, cost.Text);
            //List_deck();
            Add_card_admin add_card = new Add_card_admin();
            add_card.title = title.Text;
            add_card.cost = cost.Text;
            Intent intent = new Intent(this, typeof(Add_card_admin));
            // ��������� ������ ���������� ����, � ������ ��������
            // �� ����� �� ����� �������� �������� � Intent
            intent.PutExtra("function", "Edit");
            intent.PutExtra("title_old", delete_title);
            intent.PutExtra("title", title.Text);
            intent.PutExtra("cost_old", deck_cost);
            intent.PutExtra("cost", cost.Text);
            // ���������� ����� Activity
            StartActivity(intent);
        }
        //public bool onContextItemSelected(IMenuItem item)
        //{

        //    //if (item.ItemId() == CM_DELETE_ID)
        //    //{
        //    //    // �������� ���� � ������ ������
        //    //    AdapterContextMenuInfo acmi = (AdapterContextMenuInfo)item.getMenuInfo();
        //    //    // ������� Map �� ���������, ��������� ������� ������ � ������
        //    //    data.remove(acmi.position);
        //    //    // ����������, ��� ������ ����������
        //    //    sAdapter.notifyDataSetChanged();
        //    //    return true;
        //    //}
        //    //return super.onContextItemSelected(item);
        //}
        public void List_deck()
        {
            bd.Decks_list();
            adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, bd.items_deck);
            adapter_deck_cost = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, bd.items_deck_cost);
            list_deck.Adapter = adapter;
        }
    }
}
        
    

