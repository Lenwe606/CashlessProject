using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib
{
    public class Verkoop
    {
        private int _id;
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        private DateTime _timeStamp;
        public DateTime TimeStamp
        {
            get { return _timeStamp; }
            set { _timeStamp = value; }
        }

        private int _klantId;
        public int KlantId
        {
            get { return _klantId; }
            set { _klantId = value; }
        }

        private int _kassaId;
        public int KassaId
        {
            get { return _kassaId; }
            set { _kassaId = value; }
        }

        private int _productId;
        public int ProductId
        {
            get { return _productId; }
            set { _productId = value; }
        }

        private int _aantal;
        public int Aantal
        {
            get { return _aantal; }
            set { _aantal = value; }
        }

        private double _totaalPrijs;
        public double TotaalPrijs
        {
            get { return _totaalPrijs; }
            set { _totaalPrijs = value; }
        }

        public Verkoop()
        {

        }
        
        
        
        
        
        
        
    }
}
