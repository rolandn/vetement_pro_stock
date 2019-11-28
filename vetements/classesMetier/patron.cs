using System;
using System.Collections.Generic;
using System.Text;
using vetements.coucheAccesBD;

namespace vetements.classesMetier
{
    class patron
    {
        public int IDP;
        public string Label;
        public string Type;

        public patron()
        { }

        public patron ( patron Patron)
        {
            IDP = Patron.IDP;
            Label = Patron.Label;
            Type = Patron.Type;
        }

        public patron(int iDP, string label, string type)
        {
            IDP = iDP;
            Label = label;
            Type = type;
        }
    }
}
