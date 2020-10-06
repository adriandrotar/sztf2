using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Feleves_Feladat_BCXFMD
{
    class HirdetesKezelo
    {
        public LancoltLista<Megrendelo, int> megrendelok = new LancoltLista<Megrendelo, int>();
        public LancoltLista<Felulet, int> feluletek = new LancoltLista<Felulet, int>();
        public HirdetesKezelo()
        {
            Inicializalas();
        }
        private void Inicializalas()
        {
            Console.WriteLine("Adja meg a file nevét a kiterjesztés nélkül: ");
            StreamReader sr = new StreamReader(Console.ReadLine() + ".txt");

            int db = int.Parse(sr.ReadLine()); // FELÜLETEK FELTÖLTÉSE LÁNCOLT LISTÁBA
            while (db != 0)
            {
                string[] sor = sr.ReadLine().Split(':');

                Felulet temp = null;
                if (sor[0] == "KozlekedesiEszkoz")
                {
                    temp = new KozlekedesiEszkoz();
                }
                else if (sor[0] == "HirdetoOszlop")
                {
                    temp = new HirdetoOszlop();
                }
                else if (sor[0] == "Fenyujsag")
                {
                    temp = new Fenyujsag();
                }

                if (sor[1] == "Sima")
                {
                    temp.FeluletTipus = FeluletTipus.Sima;
                }
                else if (sor[1] == "Forgotabla")
                {
                    temp.FeluletTipus = FeluletTipus.Forgotabla;
                }
                else
                {
                    throw new OlvasasiHibaException("Nincs ilyen felület tipus!");
                }
                temp.Magassag = int.Parse(sor[2]);
                temp.Szelesseg = int.Parse(sor[3]);
                feluletek.RendezettBeszuras(temp, temp.Terulet);
                db--;
            }
            db = int.Parse(sr.ReadLine());
            while (db != 0)
            {
                string[] sor = sr.ReadLine().Split(':');
                int id = int.Parse(sor[0]);
                if (!(megrendelok.VaneElem(id)))
                {
                    Megrendelo uj = new Megrendelo(id);
                    megrendelok.RendezettBeszuras(uj, uj.ID);
                }
                Hirdetes temp = null;
                if (sor[1] == "Butorzat")
                {
                    temp = new Butorzat();
                }
                else if (sor[1] == "Auto")
                {
                    temp = new Auto();
                }
                else if (sor[1] == "Ingatlan")
                {
                    temp = new Ingatlan();
                }
                else
                {
                    throw new OlvasasiHibaException("Nincs ilyen hirdetés tipus!");
                }
                temp.Cim = sor[2];
                temp.Magassag = int.Parse(sor[3]);
                temp.Szelesseg = int.Parse(sor[4]);
                megrendelok.TartalomLekerese(id).Hirdetesek.RendezettBeszuras(temp, temp.Terulet);
                db--;
            }
            sr.Close();
        }

        private string PrintOut(LancoltLista<Hirdetes[,], int> feluletMatrixok)
        {
            string s = "";
            foreach (Hirdetes[,] feluletRacs in feluletMatrixok)
            {
                for (int i = 0; i < feluletRacs.GetLength(0); i++)
                {
                    for (int j = 0; j < feluletRacs.GetLength(1); j++)
                    {
                        if (feluletRacs[i, j] == null)
                        {
                            s += "_";
                        }
                        else
                        {
                            s += feluletRacs[i, j].Karakter;
                        }
                        
                    }
                s += "\n";
                }
                s += "\n\n";
            }
            StreamWriter sw = new StreamWriter("eredmeny.txt");
            sw.Write(s);
            sw.Close();

            return s;
        }
        public void Run()
        {
            LancoltLista<Hirdetes[,], int> feluletMatrixok = new LancoltLista<Hirdetes[,], int>(); //Felületmátrixok feltöltése
            int counter = feluletek.Count();
            foreach (Felulet felulet in feluletek)
            {
                Hirdetes[,] feluletRacs = new Hirdetes[felulet.Magassag, felulet.Szelesseg];
                feluletMatrixok.RendezettBeszuras(feluletRacs, counter--);
            }

            LancoltLista<Hirdetes, int> osszesHirdetes;

                osszesHirdetes = new LancoltLista<Hirdetes, int>();
                foreach (Megrendelo megrendelo in megrendelok)
                {
                    foreach (Hirdetes hirdetes in megrendelo.Hirdetesek)
                    {
                        osszesHirdetes.RendezettBeszuras(hirdetes, hirdetes.Terulet);
                    }
                }

                Szetoszt(feluletMatrixok,osszesHirdetes);
                Console.WriteLine(PrintOut(feluletMatrixok)); 
                

        }

        private void Szetoszt(LancoltLista<Hirdetes[,], int> feluletMatrixok, LancoltLista<Hirdetes, int> osszesHirdetes)
        {
            foreach (Hirdetes[,] feluletmatrix in feluletMatrixok)
            {
                for (int i = 0; i < feluletmatrix.GetLength(0); i++)
                {
                    for (int j = 0; j < feluletmatrix.GetLength(1); j++)
                    {
                        if (feluletmatrix[i, j] == null)
                        {
                            foreach (Hirdetes hirdetes in osszesHirdetes)
                            {
                                bool szabad = true;
                                for (int k = 0; k < hirdetes.Magassag; k++)
                                {
                                    for (int l = 0; l < hirdetes.Szelesseg; l++)
                                    {
                                        if (((i + k) >= feluletmatrix.GetLength(0)) || (j + l) >= feluletmatrix.GetLength(1))
                                        {
                                            szabad = false;
                                        }
                                        else if (!(feluletmatrix[i + k, j + l] == null))
                                        {
                                                szabad = false;
                                        }
                                    }
                                }

                                if (szabad)
                                {
                                    for (int k = 0; k < hirdetes.Magassag; k++)
                                    {
                                        for (int l = 0; l < hirdetes.Szelesseg; l++)
                                        {
                                            feluletmatrix[i + k, j + l] = hirdetes;
                                        }
                                    }

                                    osszesHirdetes.Torles(hirdetes.Terulet); // le kell kezelni hogy ne töröljön ki olyan hirdetést aminek megegyezik a területe
                                }
                                

                            }
                        }
                    }
                }
            }
        }
    }
}