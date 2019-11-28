using System;
using System.Collections.Generic;
using System.Text;
using vetements.coucheAccesBD;

namespace vetements.classesMetier
{
    class client
    {
        public int Num_carte { set; get; }
        public string Nom { set; get; }
        public string Prenom { set; get; }
        public string Email { set; get; }

        public client()
        { }

        public client(client Client)
        {
            Num_carte = Client.Num_carte;
            Nom = Client.Nom;
            Prenom = Client.Prenom;
            Email = Client.Email;
        }

        public client(int num_carte, string nom, string prenom, string email)
        {
            Num_carte = num_carte;
            Nom = nom;
            Prenom = prenom;
            Email = email;
        }
    }
}
