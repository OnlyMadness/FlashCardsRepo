using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FlashCardsPort.iOS;
using MySql.Data.MySqlClient;

namespace FlashCardsPort
{   
    class BaseData
    {
        public int i = 0;
		public List<Decks_item> di;
		public List<Cards_item> ci;
		public string Title_deck;
        public string[] decks = new string[10];
         public MySqlConnection con = new MySqlConnection("Server=31.220.20.81;port=3306;database=u688865617_flash;User Id=u688865617_flash;Password = kinkston;charset=utf8");
        //public MySqlConnection con = new MySqlConnection("Server=31.220.20.8;port=3306;database=u688865617_flash;User Id=u688865617_flash;Password = kinkston;charset=utf8");
   //     public MySqlConnectionStringBuilder mysqlbuilder = new MySqlConnectionStringBuilder();
   //     public MySqlConnection con;
        public void connection()
        {
            //mysqlbuilder.Server = "sql11.freemysqlhosting.net";  // IP адоес БД
            //mysqlbuilder.Database = "sql11172481";    // Имя БД
            //mysqlbuilder.UserID = "sql11172481";        // Имя пользователя БД
            //mysqlbuilder.Password = "MpcHhtCag1";   // Пароль пользователя БД
            //mysqlbuilder.CharacterSet = "cp1251"; // Кодировка Базы Данных
            //con = new MySqlConnection(mysqlbuilder.ConnectionString);
            //mysqlbuilder.Server = "31.220.20.81";  
            //mysqlbuilder.Port = 3306;
            //mysqlbuilder.Database = "u688865617_flash";    
            //mysqlbuilder.UserID = "u688865617_flash";        
            //mysqlbuilder.Password = "kinkston";   
            //mysqlbuilder.CharacterSet = "cp1251"; 
            //con = new MySqlConnection(mysqlbuilder.ConnectionString);
        }

        public void Delete_card(String deck_title, String word)
        {
            try
            {
                if (con.State == System.Data.ConnectionState.Closed)
                {
                    con.Open();
                    MySqlCommand cmd = new MySqlCommand("DELETE FROM cards WHERE (deck=@deck && word=@word)", con);
					cmd.Parameters.AddWithValue("@deck", deck_title);
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
        public void Decks_list()
        {
			di = new List<Decks_item>();
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
								di.Add(new Decks_item { Title = dr.GetString(0) });
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
			ci = new List<Cards_item>();
			Title_deck = deck_title;
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
								ci.Add(new Cards_item { Word = dr.GetString(0), Translate = dr.GetString(1), Title_deck = Title_deck});
                                
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