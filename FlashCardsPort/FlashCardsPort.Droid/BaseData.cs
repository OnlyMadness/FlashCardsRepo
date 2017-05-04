using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;

namespace FlashCardsPort
{   
    class BaseData
    {
        public int i = 0;
        public List<String> items_deck,items_deck_cost, items_card_title, items_card_translate;       
        public string[] decks = new string[10];
        public MySqlConnection con = new MySqlConnection("Server=sql11.freemysqlhosting.net;port=3306;database=sql11172481;User Id=sql11172481;Password = MpcHhtCag1;charset=utf8");
        public void Delete_card(String deck, String word)
        {
            try
            {
                if (con.State == System.Data.ConnectionState.Closed)
                {
                    con.Open();
                    MySqlCommand cmd = new MySqlCommand("DELETE FROM cards WHERE (deck=@deck && word=@word)", con);
                    cmd.Parameters.AddWithValue("@deck", deck);
                    cmd.Parameters.AddWithValue("@word", word);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    //MySqlCommand cmd = new MySqlCommand();
                }
            }
            catch (MySqlException ex)
            {

            }
            finally
            {
                con.Close();
            }
        }
        public void User_Registration(String email, String pass)
        {
            try
            {
                if (con.State == System.Data.ConnectionState.Closed)
                {
                    con.Open();
                    MySqlCommand cmd = new MySqlCommand("Insert INTO users(email,password) VALUES (@email,@pass)", con);
                    cmd.Parameters.AddWithValue("@email",email);
                    cmd.Parameters.AddWithValue("@pass", pass);
                    cmd.ExecuteNonQuery();                  
                }
            }
            catch (MySqlException ex)
            {
                
            }
            finally
            {
                con.Close();
            }
        }
        public void Add_deck(String title, String cost)
        {
            try
            {
                if (con.State == System.Data.ConnectionState.Closed)
                {
                    con.Open();
                    MySqlCommand cmd = new MySqlCommand("Insert INTO decks(deck,Cost) VALUES (@title,@cost)", con);
                    cmd.Parameters.AddWithValue("@title", title);
                    cmd.Parameters.AddWithValue("@cost", cost);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (MySqlException ex)
            {

            }
            finally
            {
                con.Close();
            }
        }
        public void Add_deck_cards(String deck, List<FlashCardsPort.Droid.Card> list)
        {
            try
            {
                if (con.State == System.Data.ConnectionState.Closed)
                {
                    con.Open();
                    for (int i = 0; i < list.Count; i++)
                    {
                        MySqlCommand cmd = new MySqlCommand("Insert INTO cards(deck,word,translate) VALUES (@deck,@word,@translate)", con);
                        cmd.Parameters.AddWithValue("@deck", deck);
                        cmd.Parameters.AddWithValue("@word", list[i].Word);
                        cmd.Parameters.AddWithValue("@translate", list[i].Translate);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (MySqlException ex)
            {

            }
            finally
            {
                con.Close();
            }
        }
        public void Decks_list()
        {
            items_deck = new List<String>();
            items_deck_cost = new List<String>();
            try
            {
                if (con.State == System.Data.ConnectionState.Closed)
                {
                    con.Open();
                    MySqlCommand cmd = new MySqlCommand("Select deck, cost FROM decks", con);
                    using (MySqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                items_deck.Add(dr.GetString(0));
                                items_deck_cost.Add(dr.GetString(1));
                            }
                            dr.NextResult();
                        }
                    }
                    con.Close();
                }
            }
            catch (MySqlException ex)
            {

            }
            finally
            {
                con.Close();
            }
        }
        public void delete_item_list(String title_delete)
        {
            try
            {
                if (con.State == System.Data.ConnectionState.Closed)
                {
                    con.Open();
                    MySqlCommand cmd = new MySqlCommand("DELETE FROM cards WHERE deck=@deck", con);
                    cmd.Parameters.AddWithValue("@deck", title_delete);
                    cmd.ExecuteNonQuery();
                    MySqlCommand cmd1 = new MySqlCommand("DELETE FROM `user-deck` WHERE deck=@deck", con);
                    cmd1.Parameters.AddWithValue("@deck", title_delete);
                    cmd1.ExecuteNonQuery();
                    MySqlCommand cmd2 = new MySqlCommand("DELETE FROM decks WHERE deck=@deck", con);
                    cmd2.Parameters.AddWithValue("@deck", title_delete);
                    cmd2.ExecuteNonQuery();
                    con.Close();
                    //MySqlCommand cmd = new MySqlCommand();
                }
            }
            catch (MySqlException ex)
            {

            }
            finally
            {
                con.Close();
            }
        }
        public void Cards_list(String deck_title)
        {
            items_card_title= new List<String>();
            items_card_translate= new List<String>();
            try
            {
                if (con.State == System.Data.ConnectionState.Closed)
                {
                    con.Open();
                    MySqlCommand cmd = new MySqlCommand("Select word,translate FROM cards WHERE deck=@deck", con);
                    cmd.Parameters.AddWithValue("@deck", deck_title);
                    using (MySqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                items_card_title.Add(dr.GetString(0));
                                items_card_translate.Add(dr.GetString(1));
                            }
                            dr.NextResult();
                        }
                    }
                    con.Close();
                }
            }
            catch (MySqlException ex)
            {

            }
            finally
            {
                con.Close();
            }
        }
        public void Update_cards(String deck, String old_word, String new_word, String old_translate, String new_translate)
        {
            try
            {
                if (con.State == System.Data.ConnectionState.Closed)
                {
                    con.Open();
                    MySqlCommand cmd = new MySqlCommand("Update cards SET word=@new_word, translate=@new_translate WHERE deck=@deck AND word=@old_word AND translate=@old_translate", con);
                    cmd.Parameters.AddWithValue("@new_word", new_word);
                    cmd.Parameters.AddWithValue("@new_translate", new_translate);
                    cmd.Parameters.AddWithValue("@old_word", old_word);
                    cmd.Parameters.AddWithValue("@deck", deck);                              
                    cmd.Parameters.AddWithValue("@old_translate", old_translate);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (MySqlException ex)
            {

            }
            finally
            {
                con.Close();
            }
        }
    }   
}