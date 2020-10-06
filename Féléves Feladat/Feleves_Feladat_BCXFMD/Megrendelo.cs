using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feleves_Feladat_BCXFMD
{
    class Megrendelo
    {
        public int ID;
        public LancoltLista<IHirdetes, int> Hirdetesek = new LancoltLista<IHirdetes, int>();
        private int szamlalo;

        public int Szamlalo { get { return szamlalo; } }
            
            public Megrendelo(int ID)
        {
            this.ID = ID;
            this.szamlalo = 0;
        }

        private void AddItem(IHirdetes hirdetes)
        {
            if (szamlalo < 10)
            {
                Hirdetesek.RendezettBeszuras(hirdetes, hirdetes.Terulet);
                szamlalo++;
            }
            else
            {
                throw new MaxhirdetesException("A hirdetések száma maximum 10");
            }
        }

        public void Betolt(IHirdetes [] hirdetesek)
        {
            foreach (IHirdetes hirdetes in hirdetesek)
            {
                AddItem(hirdetes);
            }
        }
    }
}