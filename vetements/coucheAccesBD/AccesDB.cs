using System;
using System.Collections.Generic;
using System.Text;
using vetements.classesMetier;
using vetements.coucheAccesBD;
using System.Data.SqlClient;
using System.Data;
using Npgsql;


namespace vetements.coucheAccesBD
{
    class AccesDB


    {
        private NpgsqlConnection conn; 
        public AccesDB()
        {
            try
            {
                this.conn = new NpgsqlConnection("Server=localhost;" +
                    "port=5432;" +
                    "Database=vetements;" +
                     "UserID=roland;" +
                     "Password= 0000");
                this.conn.Open(); 
            }

            catch (Exception e)
            {
                throw new ExceptionAccesBD("Connexion  la BD : ", e.Message);
            }
        }

        // //////////////////// LES CLIENTS  /////////////////////////////////////////////////////////

            // 1) Lister les clients  ////////////////////////////////

        public List<client> ListerClients()
        {
            List<client> liste = null;
            NpgsqlCommand conn = null;

            try
            {
                conn = new NpgsqlCommand("select * from listerClients()", this.conn);

                Console.WriteLine("\n lister client \n");
                NpgsqlDataReader sqlreader = conn.ExecuteReader();

                if (sqlreader.Read())
                {
                    Console.WriteLine("\n construit la liste");
                    liste = new List<client>();

                    do
                    {
                        liste.Add(new client(
                            Convert.ToInt32(sqlreader["num_client"]),
                            Convert.ToString(sqlreader["nom"]),
                            Convert.ToString(sqlreader["prenom"]),
                            Convert.ToString(sqlreader["email"])));
                    } while (sqlreader.Read());
                } sqlreader.Close();
            }

            catch (Exception e)
            {
                throw new ExceptionAccesBD("Connexion  la BD : ", e.Message);
            }

            return liste;
        }

        // 2) Ajouter un client /////////////////////////////////////

        public int AjoutClient(client client)
        {
            NpgsqlCommand SqlCmd = null;
            try
            {
                SqlCmd = new NpgsqlCommand("select * from ajoutClient( :num_client, :nom, :prenom, :email)"
                    ,this.conn);

                // Ajout des paramètres

                SqlCmd.Parameters.Add(new NpgsqlParameter("num_client",
                    NpgsqlTypes.NpgsqlDbType.Integer));
                SqlCmd.Parameters.Add(new NpgsqlParameter("nom",
                    NpgsqlTypes.NpgsqlDbType.Varchar));
                SqlCmd.Parameters.Add(new NpgsqlParameter("prenom",
                    NpgsqlTypes.NpgsqlDbType.Varchar));
                SqlCmd.Parameters.Add(new NpgsqlParameter("email",
                    NpgsqlTypes.NpgsqlDbType.Varchar));

                // préparer la commande

                SqlCmd.Prepare();

                // ajouter les valeurs aux paramètres

                SqlCmd.Parameters[0].Value = client.Num_carte;
                SqlCmd.Parameters[1].Value = client.Nom;
                SqlCmd.Parameters[2].Value = client.Prenom;
                SqlCmd.Parameters[3].Value = client.Email;

                // Exécuter la commande

                return (SqlCmd.ExecuteNonQuery());

            }
            catch (Exception e)
            {
                Console.WriteLine("\n erreur lors avec ajout client {0}, {1}, {2}, {3}",
                    SqlCmd.Parameters[0].Value,
                    SqlCmd.Parameters[1].Value,
                    SqlCmd.Parameters[2].Value,
                    SqlCmd.Parameters[3].Value
                    );
                throw new ExceptionAccesBD(SqlCmd.CommandText, e.Message);
            }
        }

        // 3 Lister les clients ayant fait un achat depuis une certaine date /////////////////////////////////////

        public List<client> ListerClientsAcheteur(DateTime date_introduite)
        {
            List<client> liste = null;
            NpgsqlCommand conn = null;
            NpgsqlCommand SqlCmd = null;
            

            try
            {
                SqlCmd = new NpgsqlCommand("select * from listerClientsAcheteur( :achat.date)", this.conn);


                // Ajout du paramètre date

                SqlCmd.Parameters.Add(new NpgsqlParameter("achat.date", NpgsqlTypes.NpgsqlDbType.Date));

                // Prépare la commande

                SqlCmd.Prepare();

                // Ajout paramètre

                SqlCmd.Parameters[0].Value = date_introduite;

                NpgsqlDataReader sqlreader = SqlCmd.ExecuteReader();

                if (sqlreader.Read())
                {
                    Console.WriteLine("\n construit la liste");
                    liste = new List<client>();

                    do
                    {
                        liste.Add(new client(
                            Convert.ToInt32(sqlreader["num_client"]),
                            Convert.ToString(sqlreader["nom"]),
                            Convert.ToString(sqlreader["prenom"]),
                            Convert.ToString(sqlreader["email"])));
                    } while (sqlreader.Read());
                }
                sqlreader.Close();
            }

            catch (Exception e)
            {
                throw new ExceptionAccesBD("Connexion  la BD : ", e.Message);
                
            }

            return liste;
        }

        // 4 Lister les clients N'ayant PAS fait un achat depuis une certaine date /////////////////////////////////////

        public List<client> ListerClientsPasAcheteur(DateTime date_introduite)
        {
            List<client> liste = null;
            NpgsqlCommand conn = null;
            NpgsqlCommand SqlCmd = null;


            try
            {
                SqlCmd = new NpgsqlCommand("select * from listerClientsPasAcheteur( :achat.date)", this.conn);


                // Ajout du paramètre date

                SqlCmd.Parameters.Add(new NpgsqlParameter("achat.date", NpgsqlTypes.NpgsqlDbType.Date));

                // Prépare la commande

                SqlCmd.Prepare();

                // Ajout paramètre

                SqlCmd.Parameters[0].Value = date_introduite;

                NpgsqlDataReader sqlreader = SqlCmd.ExecuteReader();

                if (sqlreader.Read())
                {
                    Console.WriteLine("\n construit la liste");
                    liste = new List<client>();

                    do
                    {
                        liste.Add(new client(
                            Convert.ToInt32(sqlreader["num_client"]),
                            Convert.ToString(sqlreader["nom"]),
                            Convert.ToString(sqlreader["prenom"]),
                            Convert.ToString(sqlreader["email"])));
                    } while (sqlreader.Read());
                }
                sqlreader.Close();
            }

            catch (Exception e)
            {
                throw new ExceptionAccesBD("Connexion  la BD : ", e.Message);
            }

            return liste;
        }

        
        /// /////////////////////////// LES VETEMENTS  ////////////////////////////////:
       

        // 5 Lister les vêtements tous magasins confondus

        public List<vetement> ListerTousVetements()
        {
            List<vetement> liste_vet = null;
            NpgsqlCommand conn = null;

            try
            {
                conn = new NpgsqlCommand("select * from listerVetement()", this.conn);

                Console.WriteLine("\n lister vetements \n");
                NpgsqlDataReader sqlreader = conn.ExecuteReader();

                if (sqlreader.Read())
                {
                   // Console.WriteLine("\n construit la liste");
                    liste_vet = new List<vetement>();

                    do
                    {
                        liste_vet.Add(new vetement(
                            Convert.ToInt32(sqlreader["idv"]),
                            Convert.ToString(sqlreader["taille"]),
                            Convert.ToDecimal(sqlreader["prix"]),
                            Convert.ToBoolean(sqlreader["est_vendu"]),
                            Convert.ToInt32(sqlreader["patron_id"]),
                            Convert.ToInt32(sqlreader["magasin_id"])));
                    } while (sqlreader.Read());
                }
                sqlreader.Close();
            }

            catch (Exception e)
            {
                throw new ExceptionAccesBD("Connexion  la BD : ", e.Message);
            }

            return liste_vet;
        }

        // ////////////////  6 Lister les vêtements non vendus   ///////////////////////////////////////////////////

        public List<vetement> ListerTousVetementsNonVendus()
        {
            List<vetement> liste_vet = null;
            NpgsqlCommand conn = null;

            try
            {
                conn = new NpgsqlCommand("select * from listertousvetementsnonvendus()", this.conn);

             //   conn = new NpgsqlCommand("select idv, taille, prix, est_vendu, patron_id, magasin_id from VETEMENT " +
             //       " where est_vendu = FALSE " +
             //       "order by magasin_id, taille", this.conn);

                Console.WriteLine("\n lister vetements \n");
                NpgsqlDataReader sqlreader = conn.ExecuteReader();

                if (sqlreader.Read())
                {
                    Console.WriteLine("\n construit la liste");
                    liste_vet = new List<vetement>();

                    do
                    {
                        liste_vet.Add(new vetement(
                            Convert.ToInt32(sqlreader["idv"]),
                            Convert.ToString(sqlreader["taille"]),
                            Convert.ToDecimal(sqlreader["prix"]),
                            Convert.ToBoolean(sqlreader["est_vendu"]),
                            Convert.ToInt32(sqlreader["patron_id"]),
                            Convert.ToInt32(sqlreader["magasin_id"])));
                    } while (sqlreader.Read());
                }
                sqlreader.Close();
            }

            catch (Exception e)
            {
                throw new ExceptionAccesBD("Connexion  la BD : ", e.Message);
            }

            return liste_vet;
        }

        // ////////// 7 Lister les vêtements vendus à une certaine date /////////////////////////////////////

        public List<vetement> ListerVetementsVenduDate(DateTime date_introduite)
        {
            List<vetement> liste = null;
            NpgsqlCommand conn = null;
            NpgsqlCommand SqlCmd = null;


            try
            {
                SqlCmd = new NpgsqlCommand("select * from listervetementsvendudate( :achat.date)", this.conn);


                // Ajout du paramètre date

                SqlCmd.Parameters.Add(new NpgsqlParameter("achat.date", NpgsqlTypes.NpgsqlDbType.Date));

                // Prépare la commande

                SqlCmd.Prepare();

                // Ajout paramètre

                SqlCmd.Parameters[0].Value = date_introduite;

                NpgsqlDataReader sqlreader = SqlCmd.ExecuteReader();

                if (sqlreader.Read())
                {
                    Console.WriteLine("\n construit la liste");
                    liste = new List<vetement>();

                    do
                    {
                        liste.Add(new vetement(
                            Convert.ToInt32(sqlreader["idv"]),
                            Convert.ToString(sqlreader["taille"]),
                            Convert.ToDecimal(sqlreader["prix"]),
                            Convert.ToBoolean(sqlreader["est_vendu"]),
                            Convert.ToInt32(sqlreader["patron_id"]),
                            Convert.ToInt32(sqlreader["magasin_id"])));
                    } while (sqlreader.Read());
                }
                sqlreader.Close();
            }

            catch (Exception e)
            {
                throw new ExceptionAccesBD("Connexion  la BD : ", e.Message);
            }

            return liste;
        }

        // /////////////////// 8 Ajouter/créer un vêtement //////////////////////////////////////////////

        public int AjoutVetement(vetement vetement)
        {
            NpgsqlCommand SqlCmd = null;
            try
            {
                SqlCmd = new NpgsqlCommand("select * from ajoutervetement ( :idv, :taille, :prix, :patron_id, :est_vendu, :magasin_id)", this.conn);

                // Ajout des paramètres

                SqlCmd.Parameters.Add(new NpgsqlParameter("idv",
                    NpgsqlTypes.NpgsqlDbType.Integer));
                SqlCmd.Parameters.Add(new NpgsqlParameter("taille",
                    NpgsqlTypes.NpgsqlDbType.Varchar));
                SqlCmd.Parameters.Add(new NpgsqlParameter("prix",
                    NpgsqlTypes.NpgsqlDbType.Double));
                SqlCmd.Parameters.Add(new NpgsqlParameter("patron_id",
                    NpgsqlTypes.NpgsqlDbType.Integer));
                SqlCmd.Parameters.Add(new NpgsqlParameter("est_vendu",
                    NpgsqlTypes.NpgsqlDbType.Boolean));
                SqlCmd.Parameters.Add(new NpgsqlParameter("magasin_id",
                    NpgsqlTypes.NpgsqlDbType.Integer));

                // préparer la commande

                SqlCmd.Prepare();

                // ajouter les valeurs aux paramètres

                SqlCmd.Parameters[0].Value = vetement.IDV;
                SqlCmd.Parameters[1].Value = vetement.Taille;
                SqlCmd.Parameters[2].Value = vetement.Prix;
                SqlCmd.Parameters[3].Value = vetement.PatronID;
                SqlCmd.Parameters[4].Value = vetement.Est_vendu;
                SqlCmd.Parameters[5].Value = vetement.Magasin_id;

                // Exécuter la commande

                return (SqlCmd.ExecuteNonQuery());

            }
            catch (Exception e)
            {
                Console.WriteLine("\n erreur lors avec ajout vetement {0}, {1}, {2}, {3}, {4}, {5}",
                    SqlCmd.Parameters[0].Value,
                    SqlCmd.Parameters[1].Value,
                    SqlCmd.Parameters[2].Value,
                    SqlCmd.Parameters[3].Value,
                    SqlCmd.Parameters[4].Value,
                    SqlCmd.Parameters[5].Value
                    );
                throw new ExceptionAccesBD(SqlCmd.CommandText, e.Message);
            }
        }

        // ////////////// 9 Lister les vêtements acheté pour un client donné (ordonné par date d'achat)  ////////////////////////


        public List<vetement> ListerVetementsVenduClient(int id_introduit)
        {
            List<vetement> liste = null;
            NpgsqlCommand conn = null;
            NpgsqlCommand SqlCmd = null;


            try
            {
                SqlCmd = new NpgsqlCommand("select * from listervetementsvenduclient ( :achat.id_client)", this.conn);


                // Ajout du paramètre date

                SqlCmd.Parameters.Add(new NpgsqlParameter("achat.id_client", NpgsqlTypes.NpgsqlDbType.Integer));

                // Prépare la commande

                SqlCmd.Prepare();

                // Ajout paramètre

                SqlCmd.Parameters[0].Value = id_introduit;

                NpgsqlDataReader sqlreader = SqlCmd.ExecuteReader();

                if (sqlreader.Read())
                {
                    Console.WriteLine("\n construit la liste");
                    liste = new List<vetement>();

                    do
                    {
                        liste.Add(new vetement(
                            Convert.ToInt32(sqlreader["idv"]),
                            Convert.ToString(sqlreader["taille"]),
                            Convert.ToDecimal(sqlreader["prix"]),
                            Convert.ToBoolean(sqlreader["est_vendu"]),
                            Convert.ToInt32(sqlreader["patron_id"]),
                            Convert.ToInt32(sqlreader["magasin_id"])));
                    } while (sqlreader.Read());
                }
                sqlreader.Close();
            }

            catch (Exception e)
            {
                throw new ExceptionAccesBD("Connexion  la BD : ", e.Message);
            }

            return liste;
        }

        // ////////////// 10 Lister les vêtements non acheté pour un patron donné et une taille donnée ou toutes les tailles/////////


        public List<vetement> ListerVetementsNonVenduPatronTaille(int patron_id, string taille)
        {
            List<vetement> liste = null;
            NpgsqlCommand conn = null;
            NpgsqlCommand SqlCmd = null;

            if (taille == "")
            {
                try
                {
                    SqlCmd = new NpgsqlCommand("select * from listerVetementsNonVenduPatronTaille( :patron_id)", this.conn);


                    // Ajout du paramètre id_patron et id_taille

                    
                    SqlCmd.Parameters.Add(new NpgsqlParameter("patron_id", NpgsqlTypes.NpgsqlDbType.Integer));

                    // Prépare la commande

                    SqlCmd.Prepare();

                    // Ajout paramètre

                  
                    SqlCmd.Parameters[0].Value = patron_id;

                    NpgsqlDataReader sqlreader = SqlCmd.ExecuteReader();

                    if (sqlreader.Read())
                    {
                        Console.WriteLine("\n construit la liste");
                        liste = new List<vetement>();

                        do
                        {
                            liste.Add(new vetement(
                                Convert.ToInt32(sqlreader["idv"]),
                                Convert.ToString(sqlreader["taille"]),
                                Convert.ToDecimal(sqlreader["prix"]),
                                Convert.ToBoolean(sqlreader["est_vendu"]),
                                Convert.ToInt32(sqlreader["patron_id"]),
                                Convert.ToInt32(sqlreader["magasin_id"])));
                        } while (sqlreader.Read());
                    }


                    sqlreader.Close();
                }

                catch (Exception e)
                {
                    throw new ExceptionAccesBD("Connexion  la BD : ", e.Message);
                }

                return liste;
            }

            else
            {
                try
                {
                    SqlCmd = new NpgsqlCommand("select * from listerVetementsNonVenduPatronTailleFull( :patron_id, :taille)", this.conn);


                    // Ajout du paramètre id_patron et id_taille

                    SqlCmd.Parameters.Add(new NpgsqlParameter("taille", NpgsqlTypes.NpgsqlDbType.Varchar));
                    SqlCmd.Parameters.Add(new NpgsqlParameter("patron_id", NpgsqlTypes.NpgsqlDbType.Integer));

                    // Prépare la commande

                    SqlCmd.Prepare();

                    // Ajout paramètre

                    SqlCmd.Parameters[0].Value = taille;
                    SqlCmd.Parameters[1].Value = patron_id;

                    NpgsqlDataReader sqlreader = SqlCmd.ExecuteReader();

                    if (sqlreader.Read())
                    {
                        Console.WriteLine("\n construit la liste");
                        liste = new List<vetement>();

                        do
                        {
                            liste.Add(new vetement(
                                Convert.ToInt32(sqlreader["idv"]),
                                Convert.ToString(sqlreader["taille"]),
                                Convert.ToDecimal(sqlreader["prix"]),
                                Convert.ToBoolean(sqlreader["est_vendu"]),
                                Convert.ToInt32(sqlreader["patron_id"]),
                                Convert.ToInt32(sqlreader["magasin_id"])));
                        } while (sqlreader.Read());
                    }
                    sqlreader.Close();
                }

                catch (Exception e)
                {
                    throw new ExceptionAccesBD("Connexion  la BD : ", e.Message);
                }

                return liste;
            }
            
        }

        // ////////////// 11 Lister les vêtements non acheté pour une date donnée ou et un magasin donnée ou toutes les magasins/////////


        public List<vetement> ListerVetementsVenduDateMagasin(DateTime date, int id_magasin)
        {
            List<vetement> liste = null;
            NpgsqlCommand conn = null;
            NpgsqlCommand SqlCmd = null;


            try
            {
                SqlCmd = new NpgsqlCommand("select * from listerVetementsVenduDateMagasin( :achat.date, :achat.id_magasin", this.conn);


                // Ajout du paramètre date

                SqlCmd.Parameters.Add(new NpgsqlParameter("achat.date", NpgsqlTypes.NpgsqlDbType.Date));
                SqlCmd.Parameters.Add(new NpgsqlParameter("achat.id_magasin", NpgsqlTypes.NpgsqlDbType.Integer));

                // Prépare la commande

                SqlCmd.Prepare();

                // Ajout paramètre

                SqlCmd.Parameters[0].Value = date;
                SqlCmd.Parameters[1].Value = id_magasin;

                NpgsqlDataReader sqlreader = SqlCmd.ExecuteReader();

                if (sqlreader.Read())
                {
                    Console.WriteLine("\n construit la liste");
                    liste = new List<vetement>();

                    do
                    {
                        liste.Add(new vetement(
                            Convert.ToInt32(sqlreader["idv"]),
                            Convert.ToString(sqlreader["taille"]),
                            Convert.ToDecimal(sqlreader["prix"]),
                            Convert.ToBoolean(sqlreader["est_vendu"]),
                            Convert.ToInt32(sqlreader["patron_id"]),
                            Convert.ToInt32(sqlreader["magasin_id"])));
                    } while (sqlreader.Read());
                }
                sqlreader.Close();
            }

            catch (Exception e)
            {
                throw new ExceptionAccesBD("Connexion  la BD : ", e.Message);
            }

            return liste;


        }

        ///  ////////////////12 Méthode EXISTE pour voir si un VETEMENT existe dans la DB //////////////
        
        public int existe(int id_cherche)
        {
            NpgsqlCommand conn = null;
            NpgsqlCommand SqlCmd = null;

            int trouve = 0;
            try
            {
                SqlCmd = new NpgsqlCommand("select * from existe ( :idv)", this.conn);

                // Ajout du paramètre idv

                SqlCmd.Parameters.Add(new NpgsqlParameter("idv", NpgsqlTypes.NpgsqlDbType.Integer));

                // préparer la commande

                SqlCmd.Prepare();

                // Ajout des paramètres

                SqlCmd.Parameters[0].Value = id_cherche;
                NpgsqlDataReader sqlreader = SqlCmd.ExecuteReader();

                if(sqlreader.Read())
                {
                    trouve = 1;
                }
                sqlreader.Close();
                return trouve;


            }
            catch (Exception e)
            {
                
                throw new ExceptionAccesBD(SqlCmd.CommandText, e.Message);
            }
        }

        ///  ////////////////13 Méthode EXISTE pour voir si un MAGASIN existe dans la DB //////////////

        public int magasin_existe(int id_magasin_cherche)
        {
            NpgsqlCommand conn = null;
            NpgsqlCommand SqlCmd = null;

            int trouve = 0;
            try
            {
                SqlCmd = new NpgsqlCommand("select * from existeMagasin( :id)", this.conn);

                // Ajout du paramètre idv

                SqlCmd.Parameters.Add(new NpgsqlParameter("id", NpgsqlTypes.NpgsqlDbType.Integer));

                // préparer la commande

                SqlCmd.Prepare();

                // Ajout des paramètres

                SqlCmd.Parameters[0].Value = id_magasin_cherche;
                NpgsqlDataReader sqlreader = SqlCmd.ExecuteReader();

                if (sqlreader.Read())
                {
                    trouve = 1;
                }
                sqlreader.Close();
                return trouve;


            }
            catch (Exception e)
            {

                throw new ExceptionAccesBD(SqlCmd.CommandText, e.Message);
            }
        }

        // /////////////////14 Encoder le fait qu’un vêtement est vendu  ///////////////////////////////////////

        public int VetementVendu(vetement vetement)
        {

            NpgsqlCommand conn = null;
            NpgsqlCommand SqlCmd = null;

            try
            {
                SqlCmd = new NpgsqlCommand("select * from vetementVendu ( :idv)", this.conn);


                // Ajout du paramètre idv

                SqlCmd.Parameters.Add(new NpgsqlParameter("idv", NpgsqlTypes.NpgsqlDbType.Integer));


                // préparer la commande

                SqlCmd.Prepare();

                // Ajout des paramètres

                SqlCmd.Parameters[0].Value = vetement.IDV;

                NpgsqlDataReader sqlreader = SqlCmd.ExecuteReader();
                sqlreader.Close();

                // Exécuter la commande

                return (SqlCmd.ExecuteNonQuery());
                sqlreader.Close();

            }
            catch (Exception e)
            {

                Console.WriteLine("\n erreur lors de la modification du vetement {0}",
                    SqlCmd.Parameters[0].Value
                    );

                throw new ExceptionAccesBD(SqlCmd.CommandText, e.Message);
            }
        }

        // /////////////////15 Déplacer une vêtement d'un magasin à l'autre  ///////////////////////////////////////

        public int VetementDeplace(vetement vetement)
        {

            NpgsqlCommand conn = null;
            NpgsqlCommand SqlCmd = null;

            try
            {
                SqlCmd = new NpgsqlCommand("update vetement set magasin_id = :magasin_id" +
                    " where idv = :idv", this.conn);


                // Ajout du paramètre idv

                SqlCmd.Parameters.Add(new NpgsqlParameter("idv", NpgsqlTypes.NpgsqlDbType.Integer));
                SqlCmd.Parameters.Add(new NpgsqlParameter("magasin_id", NpgsqlTypes.NpgsqlDbType.Integer));


                // préparer la commande

                SqlCmd.Prepare();

                // Ajout des paramètres

                SqlCmd.Parameters[0].Value = vetement.IDV;
                SqlCmd.Parameters[1].Value = vetement.Magasin_id;

                NpgsqlDataReader sqlreader = SqlCmd.ExecuteReader();
                sqlreader.Close();

                // Exécuter la commande

                return (SqlCmd.ExecuteNonQuery());
                sqlreader.Close();

            }
            catch (Exception e)
            {

                Console.WriteLine("\n erreur lors de la modification du vetement {0}",
                    SqlCmd.Parameters[0].Value
                    );

                throw new ExceptionAccesBD(SqlCmd.CommandText, e.Message);
            }
        }

        // /////////////////16  Ajouter un patron /////////////////////////////////////

        public int AjoutPatron(patron patron)
        {
            NpgsqlCommand conn = null;
            NpgsqlCommand SqlCmd = null;

            try
            {
                SqlCmd = new NpgsqlCommand("insert into patron ( idp, label, type) values " +
                    "( :idp, :label, :Type)", this.conn);

                // Ajout des paramètres

                SqlCmd.Parameters.Add(new NpgsqlParameter("idp",
                    NpgsqlTypes.NpgsqlDbType.Integer));
                SqlCmd.Parameters.Add(new NpgsqlParameter("label",
                    NpgsqlTypes.NpgsqlDbType.Varchar));
                SqlCmd.Parameters.Add(new NpgsqlParameter("Type",
                    NpgsqlTypes.NpgsqlDbType.Varchar));
               

                // préparer la commande

                SqlCmd.Prepare();

                // ajouter les valeurs aux paramètres

                SqlCmd.Parameters[0].Value = patron.IDP;
                SqlCmd.Parameters[1].Value = patron.Label;
                SqlCmd.Parameters[2].Value = patron.Type;
               

                // Exécuter la commande

                return (SqlCmd.ExecuteNonQuery());

            }
            catch (Exception e)
            {
                Console.WriteLine("\n erreur lors avec ajout patron {0}, {1}, {2}",
                    SqlCmd.Parameters[0].Value,
                    SqlCmd.Parameters[1].Value,
                    SqlCmd.Parameters[2].Value
                 
                    );
                throw new ExceptionAccesBD(SqlCmd.CommandText, e.Message);
            }
        }

        // //////////// 17  Lister les patrons  ////////////////////////////////

        public List<patron> ListerPatrons()
        {
            List<patron> liste = null;
            NpgsqlCommand conn = null;
            NpgsqlCommand SqlCmd = null;

            try
            {
                conn = new NpgsqlCommand("select idp, label, type from patron " +
                    "order by idp", this.conn);

                Console.WriteLine("\n lister patrons \n");
                NpgsqlDataReader sqlreader = conn.ExecuteReader();

                if (sqlreader.Read())
                {
                    Console.WriteLine("\n construit la liste");
                    liste = new List<patron>();

                    do
                    {
                        liste.Add(new patron(
                            Convert.ToInt32(sqlreader["idp"]),
                            Convert.ToString(sqlreader["label"]),
                            Convert.ToString(sqlreader["Type"])));
                    } while (sqlreader.Read());
                }
                sqlreader.Close();
            }

            catch (Exception e)
            {
                throw new ExceptionAccesBD("Connexion  la BD : ", e.Message);
            }

            return liste;
        }

        // ///////////////// 18 Ajouter un patron /////////////////////////////////////

        public int AjoutPrestation(travail travail)
        {
            NpgsqlCommand conn = null;
            NpgsqlCommand SqlCmd = null;

            try
            {
                SqlCmd = new NpgsqlCommand("insert into travail ( code, reg_nat, jour) values " +
                    "( :code, :reg_nat, :jour)", this.conn);

                // Ajout des paramètres

                SqlCmd.Parameters.Add(new NpgsqlParameter("code",
                    NpgsqlTypes.NpgsqlDbType.Integer));
                SqlCmd.Parameters.Add(new NpgsqlParameter("reg_nat",
                    NpgsqlTypes.NpgsqlDbType.Integer));
                SqlCmd.Parameters.Add(new NpgsqlParameter("jour",
                    NpgsqlTypes.NpgsqlDbType.Varchar));
               
                
                // préparer la commande

                SqlCmd.Prepare();

                // ajouter les valeurs aux paramètres

                SqlCmd.Parameters[0].Value = travail.Code;
                SqlCmd.Parameters[1].Value = travail.Reg_nat;
                SqlCmd.Parameters[2].Value = travail.Jour;


                // Exécuter la commande

                return (SqlCmd.ExecuteNonQuery());

            }
            catch (Exception e)
            {
                Console.WriteLine("\n erreur lors avec ajout prestation {0}, {1}, {2}",
                    SqlCmd.Parameters[0].Value,
                    SqlCmd.Parameters[1].Value,
                    SqlCmd.Parameters[2].Value
                    );
                throw new ExceptionAccesBD(SqlCmd.CommandText, e.Message);
            }
        }
    }


}
