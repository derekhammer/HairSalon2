using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;

namespace Salon.Models
{
    public class Client
    {
      private string _name;
      private int _id;
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
            name.Value = this._name;
            cmd.Parameters.Add(name);

            MySqlParameter stylist_id = new MySqlParameter();
            stylist_id.ParameterName = "@stylist_id";
            stylist_id.Value = this._stylistId;
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
              string ClientName = rdr.GetString(0);
              int StylistId = rdr.GetInt32(1);
              int ClientId = rdr.GetInt32(2);
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
      }
    }
