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
    public class PlaatsorderDA
    {
        private static ConnectionStringSettings CreateConnectionString(IEnumerable<Claim> claims)
        {
            string dblogin = claims.FirstOrDefault(c => c.Type == "dblogin").Value;
            string dbpass = claims.FirstOrDefault(c => c.Type == "dbpass").Value;
            string dbname = claims.FirstOrDefault(c => c.Type == "dbname").Value;

            return Database.CreateConnectionString("System.Data.SqlClient", "METALHEAD", Cryptography.Decrypt(dbname), Cryptography.Decrypt(dblogin), Cryptography.Decrypt(dbpass));
        }

        public static int PlaatsOrder(List<string> lijst, IEnumerable<Claim> claims)
        {
                string sql = "INSERT INTO Verkoop (KlantID,KassaID,ProductID,Aantal,TotaalPrijs) VALUES(@KlantId,@KassaId,@ProductId,@Aantal,@Totaal)";
                DbParameter par1 = Database.AddParameter("ncmt.ba.cashlessproject.api.Properties.Settings.AdminDB", "KlantId", lijst[0]);
                DbParameter par2 = Database.AddParameter("ncmt.ba.cashlessproject.api.Properties.Settings.AdminDB", "KassaId", lijst[1]);
                DbParameter par3 = Database.AddParameter("ncmt.ba.cashlessproject.api.Properties.Settings.AdminDB", "ProductId", lijst[2]);
                DbParameter par4 = Database.AddParameter("ncmt.ba.cashlessproject.api.Properties.Settings.AdminDB", "Aantal", lijst[3]);
                DbParameter par5 = Database.AddParameter("ncmt.ba.cashlessproject.api.Properties.Settings.AdminDB", "Totaal", lijst[4]);
                return Database.InsertData(Database.GetConnection(CreateConnectionString(claims)), sql, par1, par2, par3,par4,par5);
        }

        public static void ChangeSaldo(List<double> lijst, IEnumerable<Claim> claims)
        {
            double Saldo = GetSaldo(lijst[0], claims);
            Saldo = Saldo - lijst[1];
            string sql = "UPDATE Klant SET Saldo=@Saldo WHERE ID=@id";
            DbParameter par1 = Database.AddParameter("ncmt.ba.cashlessproject.api.Properties.Settings.AdminDB", "Saldo", Saldo);
            DbParameter par2 = Database.AddParameter("ncmt.ba.cashlessproject.api.Properties.Settings.AdminDB", "ID", lijst[0]);
            Database.ModifyData(Database.GetConnection(CreateConnectionString(claims)), sql, par1,par2);
        }

        public static double GetSaldo(double id, IEnumerable<Claim> claims)
        {
            string sql = "SELECT * FROM Klant WHERE ID=@id";
            double saldo = 0;
            DbParameter par1 = Database.AddParameter("ncmt.ba.cashlessproject.api.Properties.Settings.AdminDB", "id", id);
            DbDataReader reader = Database.GetData(Database.GetConnection(CreateConnectionString(claims)), sql, par1);
            while (reader.Read())
            {
                saldo = Convert.ToDouble(reader["Saldo"]);
            }

            return saldo;
        }
    }
}