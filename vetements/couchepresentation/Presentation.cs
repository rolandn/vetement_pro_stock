using System;
using System.Collections.Generic;
using System.Text;
using vetements.classesMetier;
using vetements.coucheAccesBD;

namespace vetements.couchepresentation
{
    class Presentation
    {
        private AccesDB accesDB;

        public Presentation()
        {
            try
            {
                this.accesDB = new AccesDB();
            }
            catch (ExceptionAccesBD e)
            {
                Console.WriteLine("\n Accès à la DB impossible (" + e.Message + ")");
                AccesConsole.Attendre();
       
                System.Environment.Exit(0);
            }
        }

        // MENU PRINCIPAL

        public void MenuPrincipal()
        {
            while (true)
            {
                AccesConsole.CreerEcran("Menu principal");
                Console.WriteLine(" 1 = Afficher les clients" +
                                  "\n 2 = Ajouter un client" +
                                  "\n 3 = Lister les clients ayant fait un achat après une date donnée" +
                                  "\n 4 = Lister les clients N'ayant PAS fait un achat après une date donnée" +
                                  "\n 5 = Lister tous les vêtements (vendus ou non)" +
                                  "\n 6 = Lister tous les vêtements NON vendus (disponibles)" +
                                  "\n 7 = Lister tous les vêtements vendus à une certaine date" +
                                  "\n 8 = Ajouter/Encoder un vetement" +
                                  "\n 9 = Lister tous les vêtements achetés pour un client donné" +
                                  "\n 10 = Encoder le fait qu'un vêtement est vendu" +
                                  "\n 11 = Lister les patrons" +
                                  "\n 12 = Ajouter un patron" +
                                  "\n 13 = Lister les vêtements non vendu pour un patron donné et un taille donnée " +
                                  "\n 14 = Encoder un jour de prestation " +
                                  "\n 15 = Lister les vêtements vendus pour une date donnée et un magasin donné " +
                                  "\n 16 = Déplacer un vêtement d'un magasin à l'autre ");
                try
                {
                    switch (AccesConsole.SaisirInt("\nChoix: "))
                    {
                        case 1:
                            AccesConsole.CreerEcran("Lister les clients");
                            ListeClients();
                            break;
                        case 2:
                            AccesConsole.CreerEcran("Ajouter un client");
                            AjoutClient();
                            break;

                        case 3:
                            AccesConsole.CreerEcran("Lister les clients ayant fait un achat après une date donnée");
                            ListerAcheteur();
                            break;

                        case 4:
                            AccesConsole.CreerEcran("Lister les clients N'ayant PAS fait un achat après une date donnée");
                            ListerPASAcheteur();
                            break;

                        case 5:
                            AccesConsole.CreerEcran("Lister tous les vêtements (vendus ou non)");
                            ListerTousVetements();
                            break;

                        case 6:
                            AccesConsole.CreerEcran("Lister tous les vêtements NON vendus (disponibles)");
                            ListerTousVetementsNonVendus();
                            break;

                        case 7:
                            AccesConsole.CreerEcran("Lister tous les vêtements vendus à une certaine date");
                            ListerVetementsVenduDater();
                            break;

                        case 8:
                            AccesConsole.CreerEcran("Encoder un vetement");
                            AjoutVetement();
                            break;

                        case 9:
                            AccesConsole.CreerEcran("Lister les vêtements acheté pour un client donné");
                            ListerVetementsVenduClient();
                            break;

                        case 10:
                            AccesConsole.CreerEcran("Encoder le fait qu’un vêtement est vendu");
                            VetementVendu();
                            break;

                        case 11:
                            AccesConsole.CreerEcran("Lister les patrons");
                            ListerPatrons();
                            break;

                        case 12:
                            AccesConsole.CreerEcran("ajouter un patrons");
                            AjoutPatron();
                            break;

                        case 13:
                            AccesConsole.CreerEcran("Lister vetement non vendu par patron et taille");
                            ListerVetementsNonVenduPatronTaille();
                            break;

                        case 14:
                            AccesConsole.CreerEcran("ajouter une prestation");
                            AjoutPrestation();
                            break;

                        case 15:
                            AccesConsole.CreerEcran("Lister les vêtements vendus pour une date donnée et un magasin donné");
                            ListerVetementsVenduDateMagasin();
                            break;

                        case 16:
                            AccesConsole.CreerEcran("Lister les vêtements vendus pour une date donnée et un magasin donné");
                            VetementDeplace();
                            break;


                            




                        default:
                            Console.WriteLine("\n Saisie incorrecte");
                            AccesConsole.Attendre();
                            break;
                    }
                }


                catch (ExceptionAccesBD e)
                {
                    Console.WriteLine("\n Menu principal ( " +
                        e.Message + ")");
                    AccesConsole.Attendre();
                    break;
                }

            }
        }


        public void ListeClients()
        {
            List<client> liste = this.accesDB.ListerClients();

            Console.WriteLine("\n Liste obetnue : \n");

            if (liste == null)
            {
                Console.WriteLine("\n Il n'y a pas de client dans la BD !");
                return;
            }

            Console.WriteLine("Numéro de carte, Nom, Prénom, Email :\n");

            while (liste.Count > 0)
            {
                Console.WriteLine("{0}, {1}, {2}, {3}",
                                liste[0].Num_carte,
                                liste[0].Nom,
                                liste[0].Prenom,
                                liste[0].Email);
                liste.RemoveAt(0);
            }
            AccesConsole.Attendre();

        }

        public void AjoutClient()
        {
            client client = new client();
            client.Num_carte = AccesConsole.SaisirInt(
                "Entrez le numéro de carte - chiffres uniquement : ");
            if (accesDB.Equals(client.Num_carte)) Console.WriteLine("\n Ce numéro existe déjà !");

            else
            {
                client.Nom = AccesConsole.SaisirChaine("Entre son nom : ");
                client.Prenom = AccesConsole.SaisirChaine("Entre son prénom : ");
                client.Email = AccesConsole.SaisirChaine("Entre son email : ");

                if (accesDB.AjoutClient(client) == 0) Console.WriteLine("\n l'ajout n'a pas eu lieu !");

                else Console.WriteLine("\n L'ajout s'est bien déroulé !");
                AccesConsole.Attendre();
            }
        }

        public void ListerAcheteur()
        {
            DateTime date_introduite;

            date_introduite = AccesConsole.SaisirDate("Encoder la date sous format JJ/MM/AAAA: ");

            List<client> liste = this.accesDB.ListerClientsAcheteur(date_introduite);

            Console.WriteLine("\n Liste obtenue : \n");

            if (liste == null)
            {
                Console.WriteLine("\n Il n'y a pas de client dans la BD !");
                return;
            }

            Console.WriteLine("Numéro de carte, Nom, Prénom, Email :\n");

            while (liste.Count > 0)
            {
                Console.WriteLine("{0}, {1}, {2}, {3}",
                                liste[0].Num_carte,
                                liste[0].Nom,
                                liste[0].Prenom,
                                liste[0].Email);
                liste.RemoveAt(0);
            }
            AccesConsole.Attendre();

        }

        public void ListerPASAcheteur()
        {
            DateTime date_introduite;

            date_introduite = AccesConsole.SaisirDate("Encoder la date sous format JJ/MM/AAAA: ");

            List<client> liste = this.accesDB.ListerClientsPasAcheteur(date_introduite);

            Console.WriteLine("\n Liste obtenue : \n");

            if (liste == null)
            {
                Console.WriteLine("\n Il n'y a pas de client dans la BD !");
                return;
            }

            Console.WriteLine("Numéro de carte, Nom, Prénom, Email :\n");

            while (liste.Count > 0)
            {
                Console.WriteLine("{0}, {1}, {2}, {3}",
                                liste[0].Num_carte,
                                liste[0].Nom,
                                liste[0].Prenom,
                                liste[0].Email);
                liste.RemoveAt(0);
            }
            AccesConsole.Attendre();

        }

        public void ListerTousVetements()
        {

            List<vetement> liste = this.accesDB.ListerTousVetements();

            Console.WriteLine("\n Liste obtenue : \n");

            if (liste == null)
            {
                Console.WriteLine("\n Il n'y a pas de vetement dans la BD !");
                return;
            }

            Console.WriteLine("Numéro d'ID, Taille, Prix, A été vendu, N° du patron/modèle, Lieu où il est disponible (ID magasin) :\n");

            while (liste.Count > 0)
            {
                Console.WriteLine("{0}, {1}, {2}, {3}, {4}, {5}",
                                liste[0].IDV,
                                liste[0].Taille,
                                liste[0].Prix,
                                liste[0].Est_vendu,
                                liste[0].PatronID,
                                liste[0].Magasin_id);
                liste.RemoveAt(0);
            }
            AccesConsole.Attendre();

        }

        public void ListerTousVetementsNonVendus()
        {

            List<vetement> liste = this.accesDB.ListerTousVetementsNonVendus();

            Console.WriteLine("\n Liste obtenue : \n");

            if (liste == null)
            {
                Console.WriteLine("\n Il n'y a pas de vetement dans la BD !");
                return;
            }

            Console.WriteLine("Numéro d'ID, Taille, Prix, A été vendu, N° du patron/modèle, Lieu où il est disponible (ID magasin) :\n");

            while (liste.Count > 0)
            {
                Console.WriteLine("{0}, {1}, {2}, {3}, {4}, {5}",
                                liste[0].IDV,
                                liste[0].Taille,
                                liste[0].Prix,
                                liste[0].Est_vendu,
                                liste[0].PatronID,
                                liste[0].Magasin_id);
                liste.RemoveAt(0);
            }
            AccesConsole.Attendre();

        }


            // Lister tous les vêtement vendus à une date donnée
  

            public void ListerVetementsVenduDater()
            {

            DateTime date_introduite;

            date_introduite = AccesConsole.SaisirDate("Encoder la date sous format JJ/MM/AAAA: ");

            List<vetement> liste = this.accesDB.ListerVetementsVenduDate(date_introduite);

            Console.WriteLine("\n Liste obetnue : \n");

            if (liste == null)
            {
                Console.WriteLine("\n Il n'y a pas de vêtement vendu à cette date dans la BD !");
                return;
            }

            Console.WriteLine("Numéro d'ID, Taille, Prix, A été vendu, N° du patron/modèle, Lieu où il est disponible (ID magasin) :\n");

            while (liste.Count > 0)
            {
                Console.WriteLine("{0}, {1}, {2}, {3}, {4}, {5}",
                                liste[0].IDV,
                                liste[0].Taille,
                                liste[0].Prix,
                                liste[0].Est_vendu,
                                liste[0].PatronID,
                                liste[0].Magasin_id);
                liste.RemoveAt(0);
            }
            AccesConsole.Attendre();

            }

        // Encoder un nouveau vêtement

        public void AjoutVetement()
        {
            
            vetement vetement = new vetement();
            vetement.IDV = AccesConsole.SaisirInt(
                "Entrez le numéro du vetement : ");
            if (accesDB.Equals(vetement.IDV)) Console.WriteLine("\n Ce numéro existe déjà !");

            else
            {
                vetement.Taille = AccesConsole.SaisirChaine("Entrez sa taille : ");
                vetement.Prix = AccesConsole.SaisirDecimal("Entrez son prix : ");
                vetement.Est_vendu = false;
                vetement.PatronID = AccesConsole.SaisirInt("Entrez son N° de Modèle/patron : ");
                vetement.Magasin_id = AccesConsole.SaisirInt("Entrez son N° du magasin : ");

                if (accesDB.AjoutVetement(vetement) == 0) Console.WriteLine("\n l'ajout n'a pas eu lieu !");

                else Console.WriteLine("\n L'ajout s'est bien déroulé !");
                AccesConsole.Attendre();
            }
        }

        // Lister tous les vêtement vendus à une date donnée  ///////////////////////////////////////////////////////////////////////


        public void ListerVetementsVenduClient()
        {

            

            int id_introduit;

            id_introduit = AccesConsole.SaisirInt("Encoder le n° de carte du client dont vous recherchez les achats : ");

            List<vetement> liste = this.accesDB.ListerVetementsVenduClient(id_introduit);

            Console.WriteLine("\n Liste obtenue : \n");

            if (liste == null)
            {
                Console.WriteLine("\n Il n'y a pas de vêtement vendu à cette date dans la BD !");
                return;
            }

            Console.WriteLine("Numéro d'ID du vêtement, Taille, Prix, A été vendu, N° du patron/modèle, Lieu où il est disponible (ID magasin) :\n");

            while (liste.Count > 0)
            {
                Console.WriteLine("{0}, {1}, {2}, {3}, {4}, {5}",
                                liste[0].IDV,
                                liste[0].Taille,
                                liste[0].Prix,
                                liste[0].Est_vendu,
                                liste[0].PatronID,
                                liste[0].Magasin_id);
                liste.RemoveAt(0);
            }
            AccesConsole.Attendre();

        }

        // //////////////// Encoder le fait qu’un vêtement est vendu     ///////////////////////////

        public void VetementVendu()
        {

            vetement vetement = new vetement();
            vetement.IDV = AccesConsole.SaisirInt("Entrez l'ID du vêtement : ");

            if (accesDB.existe(vetement.IDV) != 1)
                Console.WriteLine("\n Ce numéro n'existe pas !");


            else
            {
                accesDB.VetementVendu(vetement);
                Console.WriteLine("\n Le vêtement a bien été vendu !");
                AccesConsole.Attendre();
            }

            AccesConsole.Attendre();
        }


        // //////////////// Encoder le fait qu’un vêtement est déplacé d'un magasin à l'autre     ////////

        public void VetementDeplace()
        {

            vetement vetement = new vetement();
            vetement.IDV = AccesConsole.SaisirInt("Entrez l'ID du vêtement : ");
            vetement.Magasin_id = AccesConsole.SaisirInt("Entrez l'ID du magasin où le vêtement doit aller : ");

            if (accesDB.existe(vetement.IDV) != 1 || accesDB.magasin_existe(vetement.Magasin_id) != 1)
                Console.WriteLine("\n Ce numéro n'existe pas !");


            else
            {
                accesDB.VetementDeplace(vetement);
                Console.WriteLine("\n Le vêtement a bien été déplacé !");
                
            }

            AccesConsole.Attendre();
        }


        /// <summary>
        /// /                Lister tous les patrons  ///////////////////////////////////////////////
        /// </summary>

        public void ListerPatrons()
        {

            List<patron> liste = this.accesDB.ListerPatrons();

            Console.WriteLine("\n Liste obtenue : \n");

            if (liste == null)
            {
                Console.WriteLine("\n Il n'y a pas de label dans la BD !");
                AccesConsole.Attendre();
                return;
            }

            Console.WriteLine("Numéro d'ID, Label, Type :\n");

            while (liste.Count > 0)
            {
                Console.WriteLine("{0}, {1}, {2}",
                                liste[0].IDP,
                                liste[0].Label,
                                liste[0].Type);
                                
                liste.RemoveAt(0);
            }
            AccesConsole.Attendre();

        }

        // ///////////////// Ajouter un patron ///////////////////////////////////

        public void AjoutPatron()
        {

            patron patron = new patron();
            patron.IDP = AccesConsole.SaisirInt(
                "Entrez l'ID du patron : ");
            if (accesDB.Equals(patron.IDP)) Console.WriteLine("\n Ce numéro existe déjà !");
            
                

            else
            {
                patron.Label = AccesConsole.SaisirChaine("Entrez son label : ");
                patron.Type = AccesConsole.SaisirChaine("Entrez son Type : ");

                if (accesDB.AjoutPatron(patron) == 0)
                {
                    Console.WriteLine("\n L'ajout n'a pas eu lieu !");
                    AccesConsole.Attendre();
                }

                else Console.WriteLine("\n L'ajout s'est bien déroulé !");
                AccesConsole.Attendre();
            }
            

        }

        // ////////////// Lister les vêtements non acheté pour un patron donné et une taille donnée ou toutes les tailles  /////////


        public void ListerVetementsNonVenduPatronTaille()
        {
            int patron_id;
            string taille;

            patron_id = AccesConsole.SaisirInt("Encoder le n° de patron : ");
            taille = AccesConsole.SaisirChaine("Encoder la taille (ne rien encoder pour chercher sur toutes les tailles) : ");

            List<vetement> liste = this.accesDB.ListerVetementsNonVenduPatronTaille(patron_id, taille);

            Console.WriteLine("\n Liste obtenue : \n");

            if (liste == null)
            {
                Console.WriteLine("\n Il n'y a pas de vêtement non vendu avec ces critères dans la BD !");
                AccesConsole.Attendre();
                return;
                
            }

            Console.WriteLine("Numéro d'ID du vêtement, Taille, Prix, A été vendu, N° du patron/modèle, Lieu où il est disponible (ID magasin) :\n");

            while (liste.Count > 0)
            {
                Console.WriteLine("{0}, {1}, {2}, {3}, {4}, {5}",
                                liste[0].IDV,
                                liste[0].Taille,
                                liste[0].Prix,
                                liste[0].Est_vendu,
                                liste[0].PatronID,
                                liste[0].Magasin_id);
                liste.RemoveAt(0);
            }
            AccesConsole.Attendre();

        }

        // //////    Lister tous les vêtement vendus à une date donnée et un magasin donné  /////////////////////////


        public void ListerVetementsVenduDateMagasin()
        {

            DateTime date;
            int id_magasin;

            date = AccesConsole.SaisirDate("Encoder la date sous format JJ/MM/AAAA: ");
            id_magasin = AccesConsole.SaisirInt("Encoder l'ID du magasin visé : ");

            List<vetement> liste = this.accesDB.ListerVetementsVenduDate(date);

            Console.WriteLine("\n Liste obetnue : \n");

            if (liste == null)
            {
                Console.WriteLine("\n Il n'y a pas de vêtement vendu à cette date dans la BD !");
                return;
            }

            Console.WriteLine("Numéro d'ID, Taille, Prix, A été vendu, N° du patron/modèle, Lieu où il est disponible (ID magasin) :\n");

            while (liste.Count > 0)
            {
                Console.WriteLine("{0}, {1}, {2}, {3}, {4}, {5}",
                                liste[0].IDV,
                                liste[0].Taille,
                                liste[0].Prix,
                                liste[0].Est_vendu,
                                liste[0].PatronID,
                                liste[0].Magasin_id);
                liste.RemoveAt(0);
            }
            AccesConsole.Attendre();

        }

        // ///////////////// Ajouter un prestation ///////////////////////////////////

        public void AjoutPrestation()
        {
            travail travail = new travail();
            
            
                travail.Code = AccesConsole.SaisirInt("Entrez le code magasin : ");
                travail.Reg_nat = AccesConsole.SaisirInt("Entrez le N° national de la vendeuse : ");
            travail.Jour = AccesConsole.SaisirChaine("Entrer le jour de travai : ");

                if (accesDB.AjoutPrestation(travail) == 0)
                {
                    Console.WriteLine("\n L'ajout n'a pas eu lieu !");
                    AccesConsole.Attendre();
                }

                else Console.WriteLine("\n L'ajout s'est bien déroulé !");
                AccesConsole.Attendre();
        }
            

        
    }



}
