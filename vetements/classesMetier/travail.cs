using System;
using System.Collections.Generic;
using System.Text;
using vetements.coucheAccesBD;

namespace vetements.classesMetier
{
    class travail
    {
        public int Code { set; get; } // Code du magasin
        public int Reg_nat { set; get; } // n° registre national de la vendeuse
        public string Jour { set; get; }

        public travail() { }

        public travail(travail Travail)
        {
            Code = Travail.Code;
            Reg_nat = Travail.Reg_nat;
            Jour = Travail.Jour;
        }

        public travail(int code, int reg_nat, string jour)
        {
            Code = code;
            Reg_nat = reg_nat;
            Jour = jour;
        }


    }
}
