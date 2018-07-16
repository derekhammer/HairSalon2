using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;
using Salon;

namespace Salon.Models
{
    public class Stylist
    {
      private string _name;
      private string _details;
      private int _id;

      public Stylist (string Name, string Details, int Id = 0)
      {
        _name = Name;
        _id = Id;
        _details = Details;
      }
      public string GetName()
      {
        return _name;
      }
      public string GetDetail()
      {
        return _details;
      }
      public void Save()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO stylists (name, details) VALUES (@name, @details);";

            MySqlParameter name = new MySqlParameter();
            name.ParameterName = "@name";
            name.Value = this._name;
            cmd.Parameters.Add(name);

            MySqlParameter details = new MySqlParameter();
            details.ParameterName = "@details";
            details.Value = this._details;
            cmd.Parameters.Add(details);

            cmd.ExecuteNonQuery();
            _id = (int) cmd.LastInsertedId;
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }

        }
        public static List<Stylist> GetAll()
        {
            List<Stylist> allStylists = new List<Stylist> {};
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM stylists;";
            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            while(rdr.Read())
            {
              string StylistName = rdr.GetString(0);
              string StylistDetails = rdr.GetString(1);
              int StylistId = rdr.GetInt32(2);
              Stylist newStylist = new Stylist(StylistName, StylistDetails, StylistId);
              allStylists.Add(newStylist);
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return allStylists;
        }
        public static void DeleteAll()
       {
           MySqlConnection conn = DB.Connection();
           conn.Open();

           var cmd = conn.CreateCommand() as MySqlCommand;
           cmd.CommandText = @"DELETE FROM stylists;";

           cmd.ExecuteNonQuery();

           conn.Close();
           if (conn != null)
           {
               conn.Dispose();
           }
      }
      }
    }
