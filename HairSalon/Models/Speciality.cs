using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;
using Salon;

namespace Salon.Models
{
    public class Speciality
    {
      private string _details;
      private int _id;

      public Speciality (string Details, int Id = 0)
      {
        _details = Details;
        _id = Id;
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
          cmd.CommandText = @"INSERT INTO specialities (details) VALUES (@details);";

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
      public static List<Speciality> GetAll()
      {
          List<Speciality> allSpecialities = new List<Speciality> {};
          MySqlConnection conn = DB.Connection();
          conn.Open();
          var cmd = conn.CreateCommand() as MySqlCommand;
          cmd.CommandText = @"SELECT * FROM specialities;";
          var rdr = cmd.ExecuteReader() as MySqlDataReader;
          while(rdr.Read())
          {
            string SpecialityDetails = rdr.GetString(1);
            int SpecialityId = rdr.GetInt32(0);
            Speciality newSpeciality = new Speciality(SpecialityDetails, SpecialityId);
            allSpecialities.Add(newSpeciality);
          }
          conn.Close();
          if (conn != null)
          {
              conn.Dispose();
          }
          return allSpecialities;
      }
    }
  }
