using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feleves_Feladat_BCXFMD
{
    class HirdetoOszlop : Felulet
    {       
        public override string ToString()
        {
            string s = "";
            for (int i = 0; i < Magassag; i++)
            {
                for (int j = 0; j < Szelesseg; j++)
                {
                    s += "O";
                }
                s += "\n";
            }
            return s;
        }
    }
}
