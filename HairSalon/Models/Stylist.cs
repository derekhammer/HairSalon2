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
      public int GetId()
      {
        return _id;
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
        public static Stylist Find(int id)
        {
          MySqlConnection conn = DB.Connection();
          conn.Open();
          var cmd = conn.CreateCommand() as MySqlCommand;
          cmd.CommandText = @"SELECT * FROM stylists WHERE id = (@searchId);";

          MySqlParameter searchId = new MySqlParameter();
          searchId.ParameterName = "@searchId";
          searchId.Value = id;
          cmd.Parameters.Add(searchId);

          var rdr = cmd.ExecuteReader() as MySqlDataReader;
          int Id =0;
          string Name = "";
          string Details = "";

          while(rdr.Read())
          {
            Name = rdr.GetString(0);
            Details = rdr.GetString(1);
            Id = rdr.GetInt32(2);
          }
          Stylist newStylist = new Stylist(Name, Details, Id);
          conn.Close();
          if (conn != null)
          {
            conn.Dispose();
          }
          return newStylist;
          }

          public void EditStylist(string editName)
          {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"UPDATE stylists SET name = @editName WHERE id = @searchId;";

            cmd.Parameters.Add(new MySqlParameter("@searchId", _id));
            cmd.Parameters.Add(new MySqlParameter("@editName", editName));

            cmd.ExecuteNonQuery();
            _name = editName;

            conn.Close();
            if (conn != null)
            {
              conn.Dispose();
            }
          }

          public void Delete()
          {
          MySqlConnection conn = DB.Connection();
          conn.Open();
          var cmd = conn.CreateCommand() as MySqlCommand;
          cmd.CommandText = @"DELETE FROM stylists WHERE id = @thisId;";
          cmd.Parameters.Add(new MySqlParameter("@thisId", _id));
          cmd.ExecuteNonQuery();
          conn.Close();
          if (conn != null)
          {
            conn.Dispose();
          }
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
