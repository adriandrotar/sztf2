using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feleves_Feladat_BCXFMD
{
    interface IHirdetes
    {
        //ADATTAGOK
        char Karakter { get; }
        //TULAJDONSAGOK
        string Cim { get; set; }

        int Szelesseg { get; set; }

        int Magassag { get; set; }

        int Terulet { get; }

        


    }
}
