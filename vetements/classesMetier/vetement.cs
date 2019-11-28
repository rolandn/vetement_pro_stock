using System;
using System.Collections.Generic;
using System.Text;
using vetements.coucheAccesBD;

namespace vetements.classesMetier
    //  !!! Le vêtement est unique !!! 1 vêtement (IDV) est vendu à 1 client
{
    class vetement
    {
        public int IDV { get; set; }
        public string Taille { get; set; }
        public decimal Prix { get; set; }
        public bool Est_vendu { get; set; }
        // Lien vers les autres objets
        public int PatronID { get; set; }  // id du patron
        public int Magasin_id { get; set; }





        public vetement()
        { }
        public vetement(vetement Vetement)
        {
            IDV = Vetement.IDV;
            Taille = Vetement.Taille;
            Prix = Vetement.Prix;
            Est_vendu = Vetement.Est_vendu;
            PatronID = Vetement.PatronID;
            Magasin_id = Vetement.Magasin_id;
            

        }
        public vetement(int iDV, string taille, decimal prix, bool est_vendu, int patronID,  int magasin_id)
        {
            IDV = iDV;
            Taille = taille;
            Prix = prix;
            Est_vendu = est_vendu;
            PatronID = patronID;
            Magasin_id = magasin_id;
            

        }
    }
}
