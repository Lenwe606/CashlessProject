using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nmct.ba.cashlessproject.model.IT_bedrijf
{
    public class Kassa
    {
        private int Id;
        public int _id
        {
            get { return Id; }
            set { Id = value; }
        }

        private string KassaNaam;
        public string _kassaNaam
        {
            get { return KassaNaam; }
            set { KassaNaam = value; }
        }

        private string Toestel;
        public string _toestel
        {
            get { return Toestel; }
            set { Toestel = value; }
        }

        private DateTime AankoopDatum;
        public DateTime _aankoopDatum
        {
            get { return AankoopDatum; }
            set { AankoopDatum = value; }
        }

        private DateTime VervalDatum;
        public DateTime _vervalDatum
        {
            get { return VervalDatum; }
            set { VervalDatum = value; }
        }

        public Kassa()
        {

        }
        
        
        
        
        
    }
}
