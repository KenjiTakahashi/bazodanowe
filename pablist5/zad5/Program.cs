using FluentNHibernate.Cfg;
using Iesi.Collections.Generic;
using NHibernate;
using System;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using zad5.Mapping;
using zad5.Models;

namespace zad5 {
    class Program {
        static ISessionFactory CreateSessionFactory() {
            return Fluently.Configure()
                .Database(FluentNHibernate.Cfg.Db.MsSqlConfiguration.MsSql2008.ConnectionString("Data Source=.;initial catalog=yol;integrated security=true"))
                .Mappings(m=>m.FluentMappings.AddFromAssemblyOf<T3Map>())
                .BuildSessionFactory();
        }

        static void Main(string[] args) {
            ISessionFactory sf = CreateSessionFactory();
            using(ISession s = sf.OpenSession()) {
                using(ITransaction t = s.BeginTransaction()) {
                    ISet<T3> t3 = new HashedSet<T3>() {
                        new T3() {str="a"},
                        new T3() {str="b"},
                        new T3() {str="c"}
                    };
                    foreach(T3 tt3 in t3)
                        s.Save(tt3);
                    T1 t1 = new T1() {
                        str = "t1",
                        t3 = t3
                    };
                    T2 t2 = new T2() {
                        str = "t2",
                        t3 = t3
                    };
                    s.Save(t1);
                    s.Save(t2);
                    t.Commit();
                    //IList<T1> k = s.QueryOver<T1>().List();
                    //Console.WriteLine(k[0].id);
                    //s.Delete(k[0]);
                    //t.Commit();
                }
            }
            Console.ReadKey();
        }
    }
}
