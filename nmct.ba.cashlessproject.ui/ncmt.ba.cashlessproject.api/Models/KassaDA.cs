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
    public class KassaDA
    {
        private static ConnectionStringSettings CreateConnectionString(IEnumerable<Claim> claims)
        {
            string dblogin = claims.FirstOrDefault(c => c.Type == "dblogin").Value;
            string dbpass = claims.FirstOrDefault(c => c.Type == "dbpass").Value;
            string dbname = claims.FirstOrDefault(c => c.Type == "dbname").Value;

            return Database.CreateConnectionString("System.Data.SqlClient", "METALHEAD", Cryptography.Decrypt(dbname), Cryptography.Decrypt(dblogin), Cryptography.Decrypt(dbpass));
        }

        public static List<Kassa> GetKassas(IEnumerable<Claim> claims)
        {


            List<Kassa> list = new List<Kassa>();
            string sql = "SELECT * FROM Kassa WHERE Zichtbaar=1";
            DbDataReader reader = Database.GetData(Database.GetConnection(CreateConnectionString(claims)), sql);
            while (reader.Read())
            {
                Kassa c = new Kassa();
                c.Id = Convert.ToInt32(reader["ID"]);
                c.KassaNaam = reader["KassaNaam"].ToString();
                c.Toestel = reader["Toestel"].ToString();

                list.Add(c);
            }

            return list;
        }

        public static int InsertKassa(Kassa c, IEnumerable<Claim> claims)
        {
            string sql = "INSERT INTO Kassa (KassaNaam,Toestel,Zichtbaar) VALUES(@KassaNaam,@Toestel,@Bit)";
            DbParameter par1 = Database.AddParameter("ncmt.ba.cashlessproject.api.Properties.Settings.AdminDB", "MedewerkerNaam", c.KassaNaam);
            DbParameter par2 = Database.AddParameter("ncmt.ba.cashlessproject.api.Properties.Settings.AdminDB", "Adres", c.Toestel);
            DbParameter par3 = Database.AddParameter("ncmt.ba.cashlessproject.api.Properties.Settings.AdminDB", "Bit", '1');
            return Database.InsertData(Database.GetConnection(CreateConnectionString(claims)), sql, par1, par2, par3);
        }

        public static void UpdateKassa(Kassa c, IEnumerable<Claim> claims)
        {
            string sql = "UPDATE Kassa SET KassaNaam=@KassaNaam, Toestel=@Toestel, Zichtbaar=1 WHERE ID=@ID";
            DbParameter par1 = Database.AddParameter("ncmt.ba.cashlessproject.api.Properties.Settings.AdminDB", "KassaNaam", c.KassaNaam);
            DbParameter par2 = Database.AddParameter("ncmt.ba.cashlessproject.api.Properties.Settings.AdminDB", "Toestel", c.Toestel);
            DbParameter par3 = Database.AddParameter("ncmt.ba.cashlessproject.api.Properties.Settings.AdminDB", "Bit", '1');
            DbParameter par4 = Database.AddParameter("ncmt.ba.cashlessproject.api.Properties.Settings.AdminDB", "ID", c.Id);
            Database.ModifyData(Database.GetConnection(CreateConnectionString(claims)), sql, par1, par2, par3, par4);
        }

        public static void DeleteKassa(int id, IEnumerable<Claim> claims)
        {
            string sql = "UPDATE Kassa SET Zichtbaar=0 WHERE ID=@ID";
            DbParameter par1 = Database.AddParameter("ncmt.ba.cashlessproject.api.Properties.Settings.AdminDB", "@ID", id);
            DbConnection con = Database.GetConnection(CreateConnectionString(claims));
            Database.ModifyData(con, sql, par1);
        }
    }
}