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
    public class ProductDA
    {
        private static ConnectionStringSettings CreateConnectionString(IEnumerable<Claim> claims)
        {
            string dblogin = claims.FirstOrDefault(c => c.Type == "dblogin").Value;
            string dbpass = claims.FirstOrDefault(c => c.Type == "dbpass").Value;
            string dbname = claims.FirstOrDefault(c => c.Type == "dbname").Value;

            return Database.CreateConnectionString("System.Data.SqlClient", "METALHEAD", Cryptography.Decrypt(dbname), Cryptography.Decrypt(dblogin), Cryptography.Decrypt(dbpass));
        }

        public static List<Product> GetProducten(IEnumerable<Claim> claims)
        {


            List<Product> list = new List<Product>();
            string sql = "SELECT * FROM Product WHERE Zichtbaar=1";
            DbDataReader reader = Database.GetData(Database.GetConnection(CreateConnectionString(claims)), sql);
            while (reader.Read())
            {
                Product c = new Product();
                c.Id = Convert.ToInt32(reader["ID"]);
                c.ProductNaam = reader["ProductNaam"].ToString();
                c.Prijs = Convert.ToInt32(reader["Prijs"]);
                c.CategorieId = Convert.ToInt32(reader["CategorieID"]);

                list.Add(c);
            }

            return list;
        }

        public static int InsertProduct(Product c, IEnumerable<Claim> claims)
        {
            string sql = "INSERT INTO Product (ProductNaam,Prijs,CategorieID,Zichtbaar) VALUES (@ProductNaam,@Prijs,@CategorieId,@Bit)";
            DbParameter par1 = Database.AddParameter("ncmt.ba.cashlessproject.api.Properties.Settings.AdminDB", "ProductNaam", c.ProductNaam);
            DbParameter par2 = Database.AddParameter("ncmt.ba.cashlessproject.api.Properties.Settings.AdminDB", "Prijs", c.Prijs);
            DbParameter par3 = Database.AddParameter("ncmt.ba.cashlessproject.api.Properties.Settings.AdminDB", "CategorieId", c.CategorieId);
            DbParameter par4 = Database.AddParameter("ncmt.ba.cashlessproject.api.Properties.Settings.AdminDB", "Bit", '1');
            return Database.InsertData(Database.GetConnection(CreateConnectionString(claims)), sql, par1, par2, par3, par4);
        }

        public static void UpdateProduct(Product c, IEnumerable<Claim> claims)
        {
            string sql = "UPDATE Product SET ProductNaam=@ProductNaam, Prijs=@Prijs, CategorieID=@CategorieId, Zichtbaar=1 WHERE ID=@ID";
            DbParameter par1 = Database.AddParameter("ncmt.ba.cashlessproject.api.Properties.Settings.AdminDB", "ProductNaam", c.ProductNaam);
            DbParameter par2 = Database.AddParameter("ncmt.ba.cashlessproject.api.Properties.Settings.AdminDB", "Prijs", c.Prijs);
            DbParameter par3 = Database.AddParameter("ncmt.ba.cashlessproject.api.Properties.Settings.AdminDB", "CategorieId", c.CategorieId);
            DbParameter par4 = Database.AddParameter("ncmt.ba.cashlessproject.api.Properties.Settings.AdminDB", "ID", c.Id);
            Database.ModifyData(Database.GetConnection(CreateConnectionString(claims)), sql, par1, par2, par3, par4);
        }

        public static void DeleteProduct(int id, IEnumerable<Claim> claims)
        {
            string sql = "UPDATE Product SET Zichtbaar=0 WHERE ID=@ID";
            DbParameter par1 = Database.AddParameter("ncmt.ba.cashlessproject.api.Properties.Settings.AdminDB", "@ID", id);
            DbConnection con = Database.GetConnection(CreateConnectionString(claims));
            Database.ModifyData(con, sql, par1);
        }
    }
}