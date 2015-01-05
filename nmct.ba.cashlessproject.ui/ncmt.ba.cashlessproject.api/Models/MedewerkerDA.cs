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
    public class MedewerkerDA
    {
        private static ConnectionStringSettings CreateConnectionString(IEnumerable<Claim> claims)
        {
            string dblogin = claims.FirstOrDefault(c => c.Type == "dblogin").Value;
            string dbpass = claims.FirstOrDefault(c => c.Type == "dbpass").Value;
            string dbname = claims.FirstOrDefault(c => c.Type == "dbname").Value;

            return Database.CreateConnectionString("System.Data.SqlClient", "METALHEAD", Cryptography.Decrypt(dbname), Cryptography.Decrypt(dblogin), Cryptography.Decrypt(dbpass));
        }

        public static List<Medewerker> GetMedewerkers(IEnumerable<Claim> claims)
        {


            List<Medewerker> list = new List<Medewerker>();
            string sql = "SELECT * FROM Medewerker WHERE Zichtbaar=1";
            DbDataReader reader = Database.GetData(Database.GetConnection(CreateConnectionString(claims)), sql);
            while (reader.Read())
            {
                Medewerker c = new Medewerker();
                c.Id = Convert.ToInt32(reader["ID"]);
                c.MedewerkerNaam = reader["MedewerkerNaam"].ToString();
                c.Adres = reader["Adres"].ToString();
                c.Email = reader["Email"].ToString();
                c.Telefoon = reader["Telefoon"].ToString();
                c.Wachtwoord = reader["Wachtwoord"].ToString();

                list.Add(c);
            }

            return list;
        }

        public static int InsertMedewerker(Medewerker c, IEnumerable<Claim> claims)
        {
            string sql = "INSERT INTO Medewerker (MedewerkerNaam,Adres,Email,Telefoon,Zichtbaar) VALUES(@MedewerkerNaam,@Adres,@Email,@Telefoon,@Bit)";
            DbParameter par1 = Database.AddParameter("ncmt.ba.cashlessproject.api.Properties.Settings.AdminDB", "MedewerkerNaam", c.MedewerkerNaam);
            DbParameter par2 = Database.AddParameter("ncmt.ba.cashlessproject.api.Properties.Settings.AdminDB", "Adres", c.Adres);
            DbParameter par3 = Database.AddParameter("ncmt.ba.cashlessproject.api.Properties.Settings.AdminDB", "Email", c.Email);
            DbParameter par4 = Database.AddParameter("ncmt.ba.cashlessproject.api.Properties.Settings.AdminDB", "Telefoon", c.Telefoon);
            DbParameter par5 = Database.AddParameter("ncmt.ba.cashlessproject.api.Properties.Settings.AdminDB", "Bit", '1');
            return Database.InsertData(Database.GetConnection(CreateConnectionString(claims)), sql, par1, par2, par3, par4, par5);
        }

        public static void UpdateMedewerker(Medewerker c, IEnumerable<Claim> claims)
        {
            string sql = "UPDATE Medewerker SET MedewerkerNaam=@MedewerkerNaam, Adres=@Adres, Email=@Email, Telefoon=@Telefoon, Zichtbaar=1 WHERE ID=@ID";
            DbParameter par1 = Database.AddParameter("ncmt.ba.cashlessproject.api.Properties.Settings.AdminDB", "MedewerkerNaam", c.MedewerkerNaam);
            DbParameter par2 = Database.AddParameter("ncmt.ba.cashlessproject.api.Properties.Settings.AdminDB", "Adres", c.Adres);
            DbParameter par3 = Database.AddParameter("ncmt.ba.cashlessproject.api.Properties.Settings.AdminDB", "Email", c.Email);
            DbParameter par4 = Database.AddParameter("ncmt.ba.cashlessproject.api.Properties.Settings.AdminDB", "Telefoon", c.Telefoon);
            DbParameter par5 = Database.AddParameter("ncmt.ba.cashlessproject.api.Properties.Settings.AdminDB", "ID", c.Id);
            Database.ModifyData(Database.GetConnection(CreateConnectionString(claims)), sql, par1, par2, par3, par4, par5);
        }

        public static void DeleteMedewerker(int id, IEnumerable<Claim> claims)
        {
            string sql = "UPDATE Medewerker SET Zichtbaar=0 WHERE ID=@ID";
            DbParameter par1 = Database.AddParameter("ncmt.ba.cashlessproject.api.Properties.Settings.AdminDB", "@ID", id);
            DbConnection con = Database.GetConnection(CreateConnectionString(claims));
            Database.ModifyData(con, sql, par1);
        }
    }
}