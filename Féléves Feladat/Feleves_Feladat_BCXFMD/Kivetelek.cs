using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feleves_Feladat_BCXFMD
{
    class MaxhirdetesException : Exception
    {
        public MaxhirdetesException(string uzenet) : base(uzenet)
        {
            
        }
    }

    class OlvasasiHibaException : Exception
    {
        public OlvasasiHibaException(string uzenet) : base(uzenet)
        {

        }
    }

    class NincsIlyenElemException : Exception
    {
        public NincsIlyenElemException(string uzenet) : base(uzenet)
        {

        }
    }
}
