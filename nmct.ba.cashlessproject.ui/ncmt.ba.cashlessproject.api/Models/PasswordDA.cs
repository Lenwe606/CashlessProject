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
    public class PasswordDA
    {

        public static List<string> GetInfo(string user, IEnumerable<Claim> claims)
        {
            List<string> list = new List<string>();
            string sql = "SELECT * FROM Vereniging WHERE Login=@Login";
            DbParameter par1 = Database.AddParameter("ncmt.ba.cashlessproject.api.Properties.Settings.AdminDB", "Login", user);
            try
            {
                DbDataReader reader = Database.GetData(Database.GetConnection("ncmt.ba.cashlessproject.api.Properties.Settings.AdminDB"), sql, par1);
                while(reader.Read())
                {
                    list.Add(reader["Login"].ToString());
                    list.Add(reader["Password"].ToString());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }

            return list;
        }

        public static void UpdatePass(List<string> lijst, IEnumerable<Claim> claims)
        {
            string sql = "UPDATE Vereniging Set Password=@Password WHERE Login=@Login";
            DbParameter par1 = Database.AddParameter("ncmt.ba.cashlessproject.api.Properties.Settings.AdminDB", "Password", lijst[0]);
            DbParameter par2 = Database.AddParameter("ncmt.ba.cashlessproject.api.Properties.Settings.AdminDB", "Login", lijst[1]);

            Database.ModifyData(Database.GetConnection("ncmt.ba.cashlessproject.api.Properties.Settings.AdminDB"), sql, par1,par2);
        }
    }
}