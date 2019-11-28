using System;
using System.Collections.Generic;
using System.Text;
using vetements.coucheAccesBD;

namespace vetements.classesMetier
{
    // !! On ne parle pas d'achat et de ligne d'achat -> ici ligne achat, on n'a pas besoin d'agréger
    class achat
    {
        public int Num_achat { set; get; } // Num carte client
        public int Id_client { set; get; } 
        public int Id_magasin { set; get; }
        public int Id_vetement { set; get; }  // Numéro unique du vêtement unique qui est acheté
        public decimal Prix { set; get; }
        public DateTime Date { set; get; }

        achat() { }

        public achat(achat Achat)
        {
            Num_achat = Achat.Num_achat;
            Id_vetement = Achat.Id_vetement;
            Id_client = Achat.Id_client;
            Id_magasin = Achat.Id_magasin;
            Prix = Achat.Prix;
            Date = Achat.Date;
        }

        public achat(int num_achat, int id_vetement, int id_client,int id_magasin, decimal prix, DateTime date)
        {
            Num_achat = num_achat;
            Id_vetement = id_vetement;
            Id_client = id_client;
            Id_magasin = id_magasin;
            Prix = prix;
            Date = date;
        }
    }
}
