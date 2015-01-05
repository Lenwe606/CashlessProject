using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib
{
    public class Errorlog
    {
        private int _kassaId;
        public int KassaId
        {
            get { return _kassaId; }
            set { _kassaId = value; }
        }

        private DateTime _timeStamp;

        public DateTime TimeStamp
        {
            get { return _timeStamp; }
            set { _timeStamp = value; }
        }

        private string _message;
        public string Message
        {
            get { return _message; }
            set { _message = value; }
        }

        private string _stackTrace;
        public string StackTrace
        {
            get { return _stackTrace; }
            set { _stackTrace = value; }
        }
        
        public Errorlog()
        {

        }
        
        
        
    }
}
