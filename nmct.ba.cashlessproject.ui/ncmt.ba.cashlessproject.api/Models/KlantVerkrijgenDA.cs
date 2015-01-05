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
    public class KlantVerkrijgenDA
    {
        private static ConnectionStringSettings CreateConnectionString(IEnumerable<Claim> claims)
        {
            string dblogin = claims.FirstOrDefault(c => c.Type == "dblogin").Value;
            string dbpass = claims.FirstOrDefault(c => c.Type == "dbpass").Value;
            string dbname = claims.FirstOrDefault(c => c.Type == "dbname").Value;

            return Database.CreateConnectionString("System.Data.SqlClient", "METALHEAD", Cryptography.Decrypt(dbname), Cryptography.Decrypt(dblogin), Cryptography.Decrypt(dbpass));
        }

        public static Klant GetKlant(IEnumerable<Claim> claims, int code)
        {


            Klant c = new Klant();
            string sql = "SELECT * FROM Klant WHERE Zichtbaar=1 AND KaartCode ='"+code+"'";
            DbDataReader reader = Database.GetData(Database.GetConnection(CreateConnectionString(claims)), sql);
            while (reader.Read())
            {
                c.Id = Convert.ToInt32(reader["ID"]);
                c.KlantNaam = reader["KlantNaam"].ToString();
                c.Adres = reader["Adres"].ToString();
                c.Saldo = Convert.ToDouble(reader["Saldo"]);
                c.KaartCode = Convert.ToInt32(reader["KaartCode"]);
            }

            return c;
        }

        public static void UpdateSaldo(List<double> lijst, IEnumerable<Claim> claims)
        {
            double klantid = lijst[0];
            double bedrag = lijst[1];

            string sql = "UPDATE Klant SET Saldo=@saldo WHERE ID=@ID";
            DbParameter par1 = Database.AddParameter("ncmt.ba.cashlessproject.api.Properties.Settings.AdminDB", "@Saldo", bedrag);
            DbParameter par2 = Database.AddParameter("ncmt.ba.cashlessproject.api.Properties.Settings.AdminDB", "@ID", klantid);
            Database.ModifyData(Database.GetConnection(CreateConnectionString(claims)), sql, par1, par2);

        }

        public static int InsertKlant(Klant c, IEnumerable<Claim> claims)
        {
            string sql = "INSERT INTO Klant (KlantNaam,Adres,Saldo,Zichtbaar,KaartCode) VALUES(@KlantNaam,@Adres,@Saldo,@Bit,@KaartCode)";
            DbParameter par1 = Database.AddParameter("ncmt.ba.cashlessproject.api.Properties.Settings.AdminDB", "KlantNaam", c.KlantNaam);
            DbParameter par2 = Database.AddParameter("ncmt.ba.cashlessproject.api.Properties.Settings.AdminDB", "Adres", c.Adres);
            DbParameter par4 = Database.AddParameter("ncmt.ba.cashlessproject.api.Properties.Settings.AdminDB", "Saldo", c.Saldo);
            DbParameter par5 = Database.AddParameter("ncmt.ba.cashlessproject.api.Properties.Settings.AdminDB", "Bit", '1');
            DbParameter par6 = Database.AddParameter("ncmt.ba.cashlessproject.api.Properties.Settings.AdminDB", "KaartCode", c.KaartCode);
            return Database.InsertData(Database.GetConnection(CreateConnectionString(claims)), sql, par1, par2, par4, par5,par6);
        }
    }
}