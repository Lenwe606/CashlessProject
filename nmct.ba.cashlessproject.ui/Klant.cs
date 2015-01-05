using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib
{
    public class Klant
    {
        private int _id;
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _klantNaam;
        public string KlantNaam
        {
            get { return _klantNaam; }
            set { _klantNaam = value; }
        }

        private string _adres;
        public string Adres
        {
            get { return _adres; }
            set { _adres = value; }
        }

        private object _foto;
        public object Foto
        {
            get { return _foto; }
            set { _foto = value; }
        }

        private double _saldo;
        public double Saldo
        {
            get { return _saldo; }
            set { _saldo = value; }
        }
        
        public Klant()
        {

        }
        
        
        
    }
}
