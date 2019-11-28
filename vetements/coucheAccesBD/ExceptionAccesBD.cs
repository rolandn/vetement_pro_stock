using System;
using System.Collections.Generic;
using System.Text;

namespace vetements.coucheAccesBD
{
    [Serializable]
    class ExceptionAccesBD : Exception
    {
        private string message;
        private string v;

        public ExceptionAccesBD()
        {
        }

        public ExceptionAccesBD(string message) : base(message)
        {
        }

        public ExceptionAccesBD(string message, Exception innerException) : base(message, innerException)
        {
        }

        public ExceptionAccesBD(string v, string message)
        {
            this.v = v;
            this.message = message;
        }

        
    }
}
