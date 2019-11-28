using System;
using System.Collections.Generic;
using System.Text;
using vetements.classesMetier;
using vetements.coucheAccesBD;

namespace vetements.couchepresentation
{
    class AccesConsole
    {
        static public void CreerEcran(string s)
        {
            Console.Clear();
            Console.WriteLine("\n" + s);
            Console.WriteLine(new String('-', s.Length) + "\n");
        }
        static public string SaisirChaine(string s)
        {
            Console.Write(s);
            return Console.In.ReadLine();
        }
        static public int SaisirInt(string s)
        {
            Console.Write(s);
            return Convert.ToInt32(Console.In.ReadLine());
        }

        static public DateTime SaisirDate(string s)
        {
            Console.Write(s);
            return Convert.ToDateTime(Console.In.ReadLine());
        }

        static public decimal SaisirDecimal(string s)
        {
            Console.Write(s);
            return Convert.ToDecimal(Console.In.ReadLine());
        }

        static public void Attendre()
        {
            Console.Write("\nPressez une touche pour continuer...");
            Console.ReadKey();
            Console.WriteLine("\n");
        }
        static public string AttendreChoix()
        {
            Console.Write("\nVoullez-vous continuer? O/N :");
            return Console.ReadLine();
        }
    }
}
