using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feleves_Feladat_BCXFMD
{
    abstract class Hirdetes : IHirdetes, IComparable
    {

        public string Cim { get; set; }

        virtual public char Karakter
        {
            get { return Karakter; }
        }

        public int Magassag { get; set; }
        public int Szelesseg { get; set; }
        public int Terulet { get { return Magassag * Szelesseg; } }

        public int CompareTo(object obj)
        {
            if (Terulet > (obj as Hirdetes).Terulet)
            {
                return 1;
            }
            else if (Terulet < (obj as Hirdetes).Terulet)
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }


        
    }
}
