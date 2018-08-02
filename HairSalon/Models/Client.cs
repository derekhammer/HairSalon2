using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;

namespace Salon.Models
{
    public class Client
    {
      private int _id;
      private string _name;
      private int _stylistId;

      public Client (string Name, int StylistId, int Id = 0)
      {
        _name = Name;
        _stylistId = StylistId;
        _id = Id;
      }
      public string GetName()
      {
        return _name;
      }
      public int GetId()
      {
        return _id;
      }
      public int GetStylistId()
      {
        return _stylistId;
      }
      public void Save()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO clients (name, stylist_id) VALUES (@name, @stylist_id);";

            MySqlParameter name = new MySqlParameter();
            name.ParameterName = "@name";
            name.Value = _name;
            cmd.Parameters.Add(name);

            MySqlParameter stylist_id = new MySqlParameter();
            stylist_id.ParameterName = "@stylist_id";
            stylist_id.Value = _stylistId;
            cmd.Parameters.Add(stylist_id);

            cmd.ExecuteNonQuery();
            _id = (int) cmd.LastInsertedId;
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }

        }
        public static List<Client> GetAll()
        {
            List<Client> allClients = new List<Client> {};
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM clients;";
            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            while(rdr.Read())
            {
              string ClientName = rdr.GetString(1);
              int StylistId = rdr.GetInt32(2);
              int ClientId = rdr.GetInt32(0);
              Client newClient = new Client(ClientName, StylistId, ClientId);
              allClients.Add(newClient);
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return allClients;
        }
        public static List<Client> GetClientId(int id)
        {
          List<Client> allClients = new List<Client> {};
          MySqlConnection conn = DB.Connection();
          conn.Open();
          var cmd = conn.CreateCommand() as MySqlCommand;
          cmd.CommandText = @"SELECT * FROM clients WHERE stylist_id = @stylist_id;";
          cmd.Parameters.Add(new MySqlParameter("@stylist_id", id));
          var rdr = cmd.ExecuteReader() as MySqlDataReader;
          while(rdr.Read())
          {
            string ClientName = rdr.GetString(1);
            int StylistId = rdr.GetInt32(2);
            int ClientId = rdr.GetInt32(0);
            Client newClient = new Client(ClientName, StylistId, ClientId);
            allClients.Add(newClient);
          }
          conn.Close();
          if (conn != null)
          {
              conn.Dispose();
          }
          return allClients;

        }
        public static Client FindClient(int id)
        {
          MySqlConnection conn = DB.Connection();
          conn.Open();
          var cmd = conn.CreateCommand() as MySqlCommand;
          cmd.CommandText = @"SELECT * FROM clients WHERE Id = (@searchId);";
          MySqlParameter searchId = new MySqlParameter();
          searchId.ParameterName = "@searchId";
          searchId.Value = id;
          cmd.Parameters.Add(searchId);
          var rdr = cmd.ExecuteReader() as MySqlDataReader;
          int Id = 0;
          string Name = "";
          int StylistId = 0;
          while(rdr.Read())
          {
            Id = rdr.GetInt32(0);
            Name = rdr.GetString(1);
            StylistId = rdr.GetInt32(2);
          }
          Client newClient = new Client(Name, StylistId, Id);
          conn.Close();
          if (conn != null)
          {
            conn.Dispose();
          }
          return newClient;
        }



        public void EditClient(string editName)
        {
          MySqlConnection conn = DB.Connection();
          conn.Open();
          var cmd = conn.CreateCommand() as MySqlCommand;
          cmd.CommandText = @"UPDATE clients SET name = @editName WHERE id = @searchId;";

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
        cmd.CommandText = @"DELETE FROM clients WHERE id = @thisId;";
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
         cmd.CommandText = @"DELETE FROM clients;";
         cmd.ExecuteNonQuery();
         conn.Close();
         if (conn != null)
         {
           conn.Dispose();
         }
       }
      }
    }
