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
        public List<String> items_deck;
        public string[] decks = new string[10];
        public MySqlConnection con = new MySqlConnection("Server=sql11.freemysqlhosting.net;port=3306;database=sql11169890;User Id=sql11169890;Password = EVDHmN5DYG;charset=utf8");

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
            try
            {
                if (con.State == System.Data.ConnectionState.Closed)
                {
                    con.Open();
                    MySqlCommand cmd = new MySqlCommand("Select deck FROM decks", con);
                    using (MySqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                items_deck.Add(dr.GetString(0));
                                //                                Console.WriteLine(i+" "+ dr.GetString(0));
                                //decks[i] = dr.GetString(0);
                                // i++;
                                //  dr.Read();
                            }
                            dr.NextResult();
                        }
                    }
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
    }   
}