using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feleves_Feladat_BCXFMD
{
    sealed class Auto : Hirdetes
    {
        public override char Karakter
        {
            get
            {
                return 'A';
            }
        }
        public override string ToString()
        {
            string s = "";
            for (int i = 0; i < Magassag; i++)
            {
                for (int j = 0; j < Szelesseg; j++)
                {
                    s += "A";
                }
                s += "\n";
            }
            return s;
        }
    }
}
