using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib
{
    public class Medewerker
    {
        private int _id;
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _medewerkerNaam;
        public string MedewerkerNaam
        {
            get { return _medewerkerNaam; }
            set { _medewerkerNaam = value; }
        }

        private string _adres;
        public string Adres
        {
            get { return _adres; }
            set { _adres = value; }
        }

        private string _email;
        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        private string _telefoon;
        public string Telefoon
        {
            get { return _telefoon; }
            set { _telefoon = value; }
        }

        private string _wachtwoord;
        public string Wachtwoord
        {
            get { return _wachtwoord; }
            set { _wachtwoord = value; }
        }

        public Medewerker()
        {

        }
        
        
        
        
        
    }
}
