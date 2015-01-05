using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nmct.ba.cashlessproject.model.IT_bedrijf
{
    public class Errorlog
    {
        private int KassaId;
        public int _kassaId
        {
            get { return KassaId; }
            set { KassaId = value; }
        }

        private DateTime TimeStamp;
        public DateTime _timeStamp
        {
            get { return TimeStamp; }
            set { TimeStamp = value; }
        }

        private string Bericht;
        public string _bericht
        {
            get { return Bericht; }
            set { Bericht = value; }
        }

        private string StackTrace;
        public string _stackTrace
        {
            get { return StackTrace; }
            set { StackTrace = value; }
        }

        public Errorlog()
        {

        }
        

        
        
        
    }
}
