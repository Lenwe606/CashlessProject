using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nmct.ba.cashlessproject.model.IT_bedrijf
{
    public class Vereniging
    {
        private int Id;
        public int _id
        {
            get { return Id; }
            set { Id = value; }
        }

        private string Login;
        public string _login
        {
            get { return Login; }
            set { Login = value; }
        }

        private string Password;
        public string _password
        {
            get { return Password; }
            set { Password = value; }
        }

        private string DbNaam;
        public string _dbNaam
        {
            get { return DbNaam; }
            set { DbNaam = value; }
        }

        private string DbLogin;
        public string _dbLogin
        {
            get { return DbLogin; }
            set { DbLogin = value; }
        }

        private string DbPaswoord;
        public string _dbPaswoord
        {
            get { return DbPaswoord; }
            set { DbPaswoord = value; }
        }

        private string VerenigingNaam;
        public string _verenigingNaam
        {
            get { return VerenigingNaam; }
            set { VerenigingNaam = value; }
        }

        private string Adres;
        public string _adres
        {
            get { return Adres; }
            set { Adres = value; }
        }

        private string Email;
        public string _email
        {
            get { return Email; }
            set { Email = value; }
        }

        private string Telefoon;
        public string _telefoon
        {
            get { return Telefoon; }
            set { Telefoon = value; }
        }

        public Vereniging()
        {

        }
        
        
        
        
        
        
        
        
        
    }
}
