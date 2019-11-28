using System;
using vetements.classesMetier;
using vetements.coucheAccesBD;
using vetements.couchepresentation;

namespace vetements
{
    class Program
    {
        static void Main(string[] args)
        {
            Presentation application = new Presentation();
            application.MenuPrincipal();
        }
    }
}
