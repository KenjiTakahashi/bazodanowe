using FluentNHibernate.Cfg;
using NHibernate;
using NHibernate.Cfg;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace zad4 {
    class Program {
        static ISessionFactory CreateSessionFactory() {
            return Fluently.Configure()
                .Database(FluentNHibernate.Cfg.Db.MsSqlConfiguration.MsSql2008.ConnectionString("Data Source=.;Initial Catalog=hid2;Integrated Security=true"))
                .Mappings(m => m.FluentMappings.AddFromAssembly(Assembly.GetExecutingAssembly()))
                .BuildSessionFactory();
        }
        static void Main(string[] args) {
            ISessionFactory sf = CreateSessionFactory();
            Stopwatch watch = new Stopwatch();
            watch.Start();
            using(ISession s = sf.OpenSession()) {
                using(ITransaction t = s.BeginTransaction()) {
                    for(int i = 0; i < 1000; ++i) {
                        s.Save(new Person {
                            name = i.ToString(),
                            surname = (i + 1).ToString()
                        });
                        s.Save(new Worker {
                            name = i.ToString(),
                            surname = (i + 1).ToString(),
                            salary = i * 1000,
                            position = (i % 10).ToString()
                        });
                        s.Save(new OutWorker {
                            name = i.ToString(),
                            surname = (i + 1).ToString(),
                            salary = i * 1000,
                            position = (i % 10).ToString(),
                            company = (i % 110).ToString()
                        });
                    }
                    t.Commit();
                }
            }
            watch.Stop();
            Console.WriteLine(string.Format("Insert: {0}", watch.Elapsed));
            watch.Reset();
            watch.Start();
            using(ISession s = sf.OpenSession()) {
                IList<Person> p = s.QueryOver<Person>().List();
                foreach(Person e in p) {
                    Person ee = e;
                }
                IList<Worker> w = s.QueryOver<Worker>().List();
                foreach(Worker e in w) {
                    Worker ee = e;
                }
                IList<OutWorker> o = s.QueryOver<OutWorker>().List();
                foreach(OutWorker e in o) {
                    OutWorker ee = e;
                }
            }
            watch.Stop();
            Console.WriteLine(string.Format("Select: {0}", watch.Elapsed));
            using(ISession s = sf.OpenSession()) {
                s.Delete("from Person p");
                s.Flush();
            }
            Console.ReadKey();
        }
    }
}