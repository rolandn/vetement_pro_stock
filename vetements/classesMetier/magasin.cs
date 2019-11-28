using System;
using System.Collections.Generic;
using System.Text;
using vetements.coucheAccesBD;

namespace vetements.classesMetier
{
    class magasin
    {
        public int Code;
        public int CP; // code postal
        public string Localite;
        public string Rue;
        public int Numero;

        public magasin()
        { }

        public magasin(magasin Magasin)
        {
            Code = Magasin.Code;
            CP = Magasin.CP;
            Localite = Magasin.Localite;
            Rue = Magasin.Rue;
            Numero = Magasin.Numero;
        }

        public magasin(int code, int cp, string localite, string rue, int numero)
        {
            Code = code;
            CP = cp;
            Localite = localite;
            Rue = rue;
            Numero = numero;  
        }
    }
}
