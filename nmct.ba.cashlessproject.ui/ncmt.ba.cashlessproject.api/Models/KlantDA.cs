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
    public class KlantDA
    {
        private static ConnectionStringSettings CreateConnectionString(IEnumerable<Claim> claims)
        {
            string dblogin = claims.FirstOrDefault(c => c.Type == "dblogin").Value;
            string dbpass = claims.FirstOrDefault(c => c.Type == "dbpass").Value;
            string dbname = claims.FirstOrDefault(c => c.Type == "dbname").Value;

            return Database.CreateConnectionString("System.Data.SqlClient", "METALHEAD", Cryptography.Decrypt(dbname), Cryptography.Decrypt(dblogin), Cryptography.Decrypt(dbpass));
        }

        public static List<Klant> GetKlanten(IEnumerable<Claim> claims)
        {


            List<Klant> list = new List<Klant>();
            string sql = "SELECT * FROM Klant WHERE Zichtbaar=1";
            DbDataReader reader = Database.GetData(Database.GetConnection(CreateConnectionString(claims)), sql);
            while (reader.Read())
            {
                Klant c = new Klant();
                c.Id = Convert.ToInt32(reader["ID"]);
                c.KlantNaam = reader["KlantNaam"].ToString();
                c.Adres = reader["Adres"].ToString();
                c.Saldo = Convert.ToInt32(reader["Saldo"]);

                list.Add(c);
            }

            return list;
        }

        public static void UpdateKlant(Klant c, IEnumerable<Claim> claims)
        {
            string sql = "UPDATE Klant SET KlantNaam=@KlantNaam, Adres=@Adres, Saldo=@Saldo, Zichtbaar=1 WHERE ID=@ID";
            DbParameter par1 = Database.AddParameter("ncmt.ba.cashlessproject.api.Properties.Settings.AdminDB", "KlantNaam", c.KlantNaam);
            DbParameter par2 = Database.AddParameter("ncmt.ba.cashlessproject.api.Properties.Settings.AdminDB", "Adres", c.Adres);
            DbParameter par3 = Database.AddParameter("ncmt.ba.cashlessproject.api.Properties.Settings.AdminDB", "Saldo", c.Saldo);
            DbParameter par4 = Database.AddParameter("ncmt.ba.cashlessproject.api.Properties.Settings.AdminDB", "Bit", '1');
            DbParameter par5 = Database.AddParameter("ncmt.ba.cashlessproject.api.Properties.Settings.AdminDB", "ID", c.Id);
            Database.ModifyData(Database.GetConnection(CreateConnectionString(claims)), sql, par1, par2, par3, par4, par5);
        }

        public static void DeleteKlant(int id, IEnumerable<Claim> claims)
        {
            string sql = "UPDATE Klant SET Zichtbaar=0 WHERE ID=@ID";
            DbParameter par1 = Database.AddParameter("ncmt.ba.cashlessproject.api.Properties.Settings.AdminDB", "@ID", id);
            DbConnection con = Database.GetConnection(CreateConnectionString(claims));
            Database.ModifyData(con, sql, par1);
        }
    }
}