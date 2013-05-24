using FluentNHibernate.Cfg;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace zad1 {
    class Program {
        static ISessionFactory CreateSessionFactory() {
            return Fluently.Configure()
                .Database(FluentNHibernate.Cfg.Db.MsSqlConfiguration.MsSql2008.ConnectionString("Data Source=.;initial catalog=real;integrated security=true"))
                .Mappings(m => m.FluentMappings.AddFromAssembly(Assembly.GetExecutingAssembly()))
                .BuildSessionFactory();
        }

        static void Main(string[] args) {
            ISessionFactory sf = CreateSessionFactory();
            using(ISession s = sf.OpenSession()) {
                IList<Klient> kl = new List<Klient>();
                Stopwatch w = new Stopwatch();
                using(ITransaction t = s.BeginTransaction()) {
                    //IList<Klient> k = s.QueryOver<Klient>().List();
                    //Console.WriteLine(k[0].id);
                    //s.Delete(k.Where(i => i.id == 5).ToList()[0]);
                    //t.Commit();

                    w.Start();
                    for(int i = 0; i < 100; ++i) {
                        IList<Klient> k = s.QueryOver<Klient>().List();
                        foreach(Klient kk in k) {
                            Klient kkk = kk;
                        }
                    }
                    w.Stop();
                    Console.WriteLine(string.Format("select: {0}", w.Elapsed));

                    w.Reset();

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
                        s.Save(k);
                    }
                    t.Commit();
                    w.Stop();
                    Console.WriteLine(string.Format("insert: {0}", w.Elapsed));
                }
                using(ITransaction t = s.BeginTransaction()) {
                    w.Reset();

                    w.Start();
                    for(int i = 0; i < 100; ++i) {
                        s.Delete(kl[i]);
                    }
                    t.Commit();
                    w.Stop();
                    Console.WriteLine(string.Format("delete: {0}", w.Elapsed));
                }
            }
            Console.ReadKey();
        }
    }
}
