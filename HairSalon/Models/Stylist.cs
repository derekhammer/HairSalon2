using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;
using Salon;

namespace Salon.Models
{
    public class Stylist
    {
      private string _name;
      private int _id;

      public Stylist (string Name, int Id = 0)
