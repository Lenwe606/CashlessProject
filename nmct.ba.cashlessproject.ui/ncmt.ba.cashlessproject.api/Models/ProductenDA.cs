using Lib;
using ncmt.ba.cashlessproject.api.Helper;
using nmct.ba.cashlessproject.api.helper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace ncmt.ba.cashlessproject.api.Models
{
    public class ProductenDA
    {
        private static ConnectionStringSettings CreateConnectionString(IEnumerable<Claim> claims)
        {
            string dblogin = claims.FirstOrDefault(c => c.Type == "dblogin").Value;
            string dbpass = claims.FirstOrDefault(c => c.Type == "dbpass").Value;
            string dbname = claims.FirstOrDefault(c => c.Type == "dbname").Value;

            return Database.CreateConnectionString("System.Data.SqlClient", "METALHEAD", Cryptography.Decrypt(dbname), Cryptography.Decrypt(dblogin), Cryptography.Decrypt(dbpass));
        }

        public static List<Product> GetDranken(IEnumerable<Claim> claims)
        {
            List<Product> list = new List<Product>();
            string sql = "SELECT * FROM Product WHERE Zichtbaar=1 AND CategorieID=1";
            DbDataReader reader = Database.GetData(Database.GetConnection(CreateConnectionString(claims)), sql);
            while (reader.Read())
            {
                Product c = new Product();
                c.Id = Convert.ToInt32(reader["ID"]);
                c.ProductNaam = reader["ProductNaam"].ToString();
                c.Prijs = Convert.ToInt32(reader["Prijs"]);

                list.Add(c);
            }

            return list;
        }

        public static List<Product> GetEten(IEnumerable<Claim> claims)
        {
            List<Product> list = new List<Product>();
            string sql = "SELECT * FROM Product WHERE Zichtbaar=1 AND CategorieID=2";
            DbDataReader reader = Database.GetData(Database.GetConnection(CreateConnectionString(claims)), sql);
            while (reader.Read())
            {
                Product c = new Product();
                c.Id = Convert.ToInt32(reader["ID"]);
                c.ProductNaam = reader["ProductNaam"].ToString();
                c.Prijs = Convert.ToInt32(reader["Prijs"]);

                list.Add(c);
            }

            return list;
        }

        public static List<Product> GetAndere(IEnumerable<Claim> claims)
        {
            List<Product> list = new List<Product>();
            string sql = "SELECT * FROM Product WHERE Zichtbaar=1 AND CategorieID=3";
            DbDataReader reader = Database.GetData(Database.GetConnection(CreateConnectionString(claims)), sql);
            while (reader.Read())
            {
                Product c = new Product();
                c.Id = Convert.ToInt32(reader["ID"]);
                c.ProductNaam = reader["ProductNaam"].ToString();
                c.Prijs = Convert.ToInt32(reader["Prijs"]);

                list.Add(c);
            }

            return list;
        }
    }
}