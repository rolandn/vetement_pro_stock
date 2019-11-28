using System;
using System.Collections.Generic;
using System.Text;
using vetements.coucheAccesBD;

namespace vetements.classesMetier
{
    class vendeuse
    {
        int Reg_nat;
        string Date_naissance;
        string Nom;
        string Prenom;
        string Email;

        public vendeuse()
        { }

        public vendeuse (vendeuse Vendeuse)
        {
            Reg_nat = Vendeuse.Reg_nat;
            Date_naissance = Vendeuse.Date_naissance;
            Nom = Vendeuse.Nom;
            Prenom = Vendeuse.Prenom;
            Email = Vendeuse.Email;
        }

        public vendeuse(int reg_nat, string date_naissance, string nom, string prenom, string email)
        {
            Reg_nat = reg_nat;
            Date_naissance = date_naissance;
            Nom = nom;
            Prenom = prenom;
            Email = email;
        }
    }
}
