using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feleves_Feladat_BCXFMD
{
    enum FeluletTipus { Sima,Forgotabla}
    abstract class Felulet : IComparable
    {
        
        public  FeluletTipus FeluletTipus { get; set; }

        public  int Szelesseg { get; set; }

        public  int Magassag { get; set; }

        public int Terulet { get
            {
                return Szelesseg * Magassag;
            }
        }

        public int CompareTo(object obj)
        {
            if(Terulet>(obj as Felulet).Terulet)
            {
                return 1;
            }
            else if(Terulet<(obj as Felulet).Terulet)
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
