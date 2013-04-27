using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zad4 {
    class Program {
        static void Main(string[] args) {
            SqlConnection connection = new SqlConnection("Data Source=.;Initial Catalog=real;Integrated Security=true");
            connection.Open();
            zad1.Pracownik.connection = connection;
            Stopwatch watchz = new Stopwatch();
            Stopwatch watchb = new Stopwatch();
            Stopwatch watchc = new Stopwatch();
            Stopwatch watchd = new Stopwatch();
            watchz.Start();
            for(int i = 0; i < 1000; ++i) {
                zad1.Pracownik.FindAll();
            }
            watchz.Stop();
            connection.Close();
            zad2and3.Pracownik.Init();
            watchb.Start();
            for(int i = 0; i < 1000; ++i) {
                zad2and3.Pracownik.FindAll();
            }
            watchb.Stop();
            connection.Open();
            watchd.Start();
            for(int i = 0; i < 1000; ++i) {
                zad1.Pracownik.FindAll("imie like '%a%'");
            }
            watchd.Stop();
            connection.Close();
            watchc.Start();
            for(int i = 0; i < 1000; ++i) {
                zad2and3.Pracownik.FindAll("imie like '%a%'");
                //zad2and3.Pracownik.FindAll2("imie like '%a%'");
            }
            watchc.Stop();
            Console.WriteLine(string.Format("FindAll time: (z {0}, b {1}, c {2}, d {3})", watchz.Elapsed, watchb.Elapsed, watchc.Elapsed, watchd.Elapsed));
            //watchz.Reset();
            //watchb.Reset();
            //connection.Open();
            //watchz.Start();
            //for(int i = 0; i < 1000; ++i) {
            //    zad1.Pracownik p = new zad1.Pracownik();
            //    p.imie = "a";
            //    p.nazwisko = "a";
            //    p.rola = 1;
            //    p.login = "a";
            //    p.haslo = "a";
            //    p.Save();
            //}
            //watchz.Stop();
            //connection.Close();
            //watchb.Start();
            //for(int i = 0; i < 1000; ++i) {
            //    zad2and3.Pracownik p = new zad2and3.Pracownik();
            //    //p["imie"] = "a";
            //    //p["nazwisko"] = "a";
            //    //p["rola"] = 1;
            //    //p["login"] = "a";
            //    //p["haslo"] = "a";
            //    //p.SaveOrUpdate();
            //}
            //watchb.Stop();
            //Console.WriteLine(string.Format("Save time: (z {0}, b {1})", watchz.Elapsed, watchb.Elapsed));
            Console.ReadKey();
        }
    }
}
