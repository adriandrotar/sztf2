using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feleves_Feladat_BCXFMD
{
    class LancoltLista<T, K> : IEnumerable where K : IComparable
    {
        private ListaElem fej;


        class ListaElem
        {
            public T tartalom;
            public K kulcs; // Kulcs alapján rendezzük a listát
            public ListaElem kovetkezo;
        }

        class LancoltListaBejaro : IEnumerator<T>
        {
            ListaElem aktualis = null;
            ListaElem fej;

            public LancoltListaBejaro(ListaElem fej)
            {
                this.fej = fej;
            }
            public T Current
            {
                get
                {
                    return aktualis.tartalom;
                }
            }

            object IEnumerator.Current
            {
                get
                {
                    return this.Current;
                }
            }

            public void Dispose()
            {
            }

            public bool MoveNext()
            {
                if (aktualis == null)
                {
                    if (fej != null)
                    {
                        aktualis = fej;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    if (aktualis.kovetkezo != null)
                    {
                        aktualis = aktualis.kovetkezo;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            public void Reset()
            {
            }
        }
        public IEnumerator GetEnumerator()
        {
            return new LancoltListaBejaro(fej);
        }

        public void RendezettBeszuras(T elem, K kulcs) // Kulcs szerint csökkenősorrendbe szúrjuk be a listelemeket
        {
            ListaElem uj = new ListaElem();
            uj.tartalom = elem;
            uj.kulcs = kulcs;

            if (fej == null)
            {
                uj.kovetkezo = null;
                fej = uj;
            }
            else
            {
                if (fej.kulcs.CompareTo(kulcs) <= 0)
                {
                    uj.kovetkezo = fej;
                    fej = uj;
                }
                else
                {
                    ListaElem temp = fej;
                    ListaElem elozo = new ListaElem();
                    while ((temp != null) && (temp.kulcs.CompareTo(kulcs) >= 0))
                    {
                        elozo = temp;
                        temp = temp.kovetkezo;
                    }
                    if (temp == null)
                    {
                        uj.kovetkezo = null;
                        elozo.kovetkezo = uj;
                    }
                    else
                    {
                        uj.kovetkezo = temp;
                        elozo.kovetkezo = uj;
                    }
                }

            }
        }

        public void Bejaras()  // Bejárás, csak teszteléshez kell kiirja sorba a listát 
        {
            ListaElem p = fej; //referencia mutató
            while (p != null)
            {
                Console.WriteLine(p.tartalom);
                p = p.kovetkezo;
            }

        }
        public bool VaneElem(int ID)
        {
            ListaElem p = fej;
            while (p != null )
            {
                if (p.kulcs.CompareTo(ID) == 0)
                {
                    return true;
                }
                p = p.kovetkezo;
            }
            return false;
        }

        public T TartalomLekerese(K ID)
        {
            return Kereses(ref fej, ID).tartalom;
        }

        private ListaElem Kereses(ref ListaElem elem, K ID)
        {
            if (elem != null)
            {
                if (ID.Equals(elem.kulcs))
                {
                    return elem;
                }
                else
                {
                    return Kereses(ref elem.kovetkezo, ID);
                }
            }
            else
            {
                throw new NincsIlyenElemException("Nincs ilyen elem!");
            }
        }

        public int Count()
        {
            int counter = 0;
            ListaElem p = fej; //referencia mutató
            while (p != null)
            {
                counter++;
                p = p.kovetkezo;
            }
            return counter;

        } 

        public void Torles(int ID)
        {
            ListaElem p = fej;
            ListaElem e = null;
            while ((p != null) && (p.kulcs.CompareTo(ID) != 0))
            {
                e = p;
                p = p.kovetkezo;
            }
            if (p != null)
            {
                if (e == null)
                {
                    fej=(p.kovetkezo);
                }
                else
                {
                    e.kovetkezo = p.kovetkezo;
                }
            }            
        }
    }
}