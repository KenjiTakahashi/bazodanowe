using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zad1.Models;

namespace zad1 {
    class Program {
        static void Main(string[] args) {
            using(realContext db = new realContext()) {
                //var q1 = db.Klients;
                //foreach(Klient k in q1)
                //    foreach(Pojazd p in k.Pojazds)
                //        Console.WriteLine(p.id);
                //var q3 = db.Przeglads;
                //foreach(Przeglad p in q3)
                //    Console.WriteLine(p.Pracownik.imie);

                Stopwatch w = new Stopwatch();
                w.Start();
                for(int i = 0; i < 100; ++i) {
                    DbSet<Klient> q5 = db.Klients;
                    foreach(Klient k in q5) {
                        Klient kk = k;
                    }
                }
                w.Stop();
                Console.WriteLine(string.Format("select: {0}", w.Elapsed));

                w.Reset();
                IList<Klient> kl = new List<Klient>();

                w.Start();
                for(int i = 0; i < 100; ++i) {
                    Klient k = new Klient() {
                        imie = (i + 3).ToString(),
                        nazwisko = (i % 15).ToString(),
                        email = "aaa",
                        datarejestracji = new DateTime(1754, 01, 01).AddMonths(i),
                        pesel = "123984",
                        telefon = i.ToString()
                    };
                    kl.Add(k);
                    db.Klients.Add(k);
                }
                db.SaveChanges();
                w.Stop();
                Console.WriteLine(string.Format("insert: {0}", w.Elapsed));

                w.Reset();

                w.Start();
                for(int i = 0; i < 100; ++i) {
                    db.Klients.Remove(kl[i]);
                }
                db.SaveChanges();
                w.Stop();
                Console.WriteLine(string.Format("delete: {0}", w.Elapsed));
            }
            Console.ReadKey();
        }
    }
}
