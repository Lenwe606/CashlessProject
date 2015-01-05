using ncmt.ba.cashlessproject.api.Helper;
using nmct.ba.cashlessproject.api.helper;
using nmct.ba.cashlessproject.model.IT_bedrijf;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace ncmt.ba.cashlessproject.api.Models
{
    public class VerenigingDA
    {
        public static Vereniging CheckCredentials(string username, string password)
        {
            string sql = "SELECT * FROM Vereniging WHERE Login=@Login AND Password=@Password";
            DbParameter par1 = Database.AddParameter("ncmt.ba.cashlessproject.api.Properties.Settings.AdminDB", "Login", Cryptography.Encrypt(username));
            DbParameter par2 = Database.AddParameter("ncmt.ba.cashlessproject.api.Properties.Settings.AdminDB", "Password", Cryptography.Encrypt(password));
            try
            {
                DbDataReader reader = Database.GetData(Database.GetConnection("ncmt.ba.cashlessproject.api.Properties.Settings.AdminDB"), sql, par1, par2);
                reader.Read();
                return new Vereniging()
                {
                    _id = Int32.Parse(reader["ID"].ToString()),
                    _login = reader["Login"].ToString(),
                    _password = reader["Password"].ToString(),
                    _dbNaam = reader["DbNaam"].ToString(),
                    _dbLogin = reader["DbLogin"].ToString(),
                    _dbPaswoord = reader["DbPassword"].ToString(),
                    _verenigingNaam = reader["VerenigingNaam"].ToString(),
                    _adres = reader["Adres"].ToString(),
                    _email = reader["Email"].ToString(),
                    _telefoon = reader["Telefoon"].ToString()
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }

        }

    }
}