using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zad2and3 {
    public class CustomDataTable : DataTable {
        public CustomDataTable(DataSet set, string name, IList<Tuple<string, Type, bool>> values) : base(name) {
            foreach(Tuple<string, Type, bool> tuple in values) {
                this.Columns.Add(tuple.Item1, tuple.Item2);
                this.Columns[tuple.Item1].AllowDBNull = tuple.Item3;
            }
            this.Columns["id"].AutoIncrement = true;
            this.Columns["id"].AutoIncrementSeed = 1;
            this.Columns["id"].ReadOnly = true;
            this.PrimaryKey = new DataColumn[] { this.Columns["id"] };
            set.Tables.Add(this);
        }
    }

    public class PracownikCreator : CustomDataTable {
        public PracownikCreator(DataSet set) : base(set, "Pracownik", doWork()) { }

        private static IList<Tuple<string, Type, bool>> doWork() {
            return new List<Tuple<string, Type, bool>>() {
                Tuple.Create("id", typeof(Int32), false),
                Tuple.Create("imie", typeof(String), false),
                Tuple.Create("nazwisko", typeof(String), false),
                Tuple.Create("rola", typeof(Int32), false),
                Tuple.Create("login", typeof(String), false)
            };
        }
    }

    public class Klient : CustomDataTable {
        public Klient(DataSet set) : base(set, "Klient", doWork()) { }

        private static IList<Tuple<string, Type, bool>> doWork() {
            return new List<Tuple<string, Type, bool>>() {
                Tuple.Create("id", typeof(Int32), false),
                Tuple.Create("pesel", typeof(String), false),
                Tuple.Create("imie", typeof(String), false),
                Tuple.Create("nazwisko", typeof(String), false),
                Tuple.Create("email", typeof(String), false),
                Tuple.Create("datarejestracji", typeof(DateTime), false),
                Tuple.Create("telefon", typeof(String), false)
            };
        }
    }

    public class Pojazd : CustomDataTable {
        public Pojazd(DataSet set) : base(set, "Pojazd", doWork()) {
            set.Relations.Add("", set.Tables["Klient"].Columns["id"], this.Columns["id_klient"], true);
        }

        private static IList<Tuple<string, Type, bool>> doWork() {
            return new List<Tuple<string, Type, bool>>() {
                Tuple.Create("id", typeof(Int32), false),
                Tuple.Create("id_klient", typeof(Int32), false),
                Tuple.Create("NrRej", typeof(String), false),
                Tuple.Create("DataPierwszejRejestracji", typeof(DateTime), false)
            };
        }
    }

    public class Przeglad : CustomDataTable {
        public Przeglad(DataSet set) : base(set, "Przeglad", doWork()) {
            set.Relations.Add("", set.Tables["Pojazd"].Columns["id"], this.Columns["ID_Pojazd"], true);
            set.Relations.Add("", set.Tables["Pracownik"].Columns["id"], this.Columns["ID_Przyjmujacego"], true);
            set.Relations.Add("", set.Tables["Pracownik"].Columns["id"], this.Columns["ID_Wykonujacego"], true);
        }

        private static IList<Tuple<string, Type, bool>> doWork() {
            return new List<Tuple<string, Type, bool>>() {
                Tuple.Create("id", typeof(Int32), false),
                Tuple.Create("ID_Pojazd", typeof(Int32), false),
                Tuple.Create("DataPlanowana", typeof(DateTime), false),
                Tuple.Create("DataNastepnego", typeof(DateTime), true),
                Tuple.Create("Zatwierdzony", typeof(bool), true),
                Tuple.Create("ID_Przyjmujacego", typeof(Int32), false),
                Tuple.Create("ID_Wykonujacego", typeof(Int32), false)
            };
        }
    }

    public class Pracownik {
        public static SqlConnection connection { get; set; }
        public static DataSet set { get; set; }
        private static SqlDataAdapter _adapter;

        private static void _Refresh() {
            if(_adapter == null)
                _adapter = new SqlDataAdapter("select * from Pracownik", connection);
            _adapter.Fill(set, "Pracownik");
        }

        public static DataRow FindById(int id) {
            _Refresh();
            DataRow[] results = set.Tables["Pracownik"].Select(string.Format("ID={0}", id));
            if(results.Length > 0)
                return results[0];
            return null;
        }

        public static DataRow[] FindAll() {
            _Refresh();
            return set.Tables["Pracownik"].Select();
        }

        public static DataRow[] FindAll(string where) {
            _Refresh();
            return set.Tables["Pracownik"].Select(where);
        }

        public static DataRow[] FindAll2(string where) {
            SqlDataAdapter _adapter = new SqlDataAdapter(string.Format("select * from Pracownik where {0}", where), connection);
            _adapter.Fill(set, "Pracownik");
            return set.Tables["Pracownik"].Select();
        }

        public static void SaveOrUpdate(DataRow d) {
            _Refresh();
            DataRow[] nr = set.Tables["Pracownik"].Select(string.Format("id={0}", d["id"]));
            if(nr.Length == 0)
                set.Tables["Pracownik"].Rows.Add(d);
            else
                nr[0]["imie"] = d["imie"];
                nr[0]["nazwisko"] = d["nazwisko"];
                nr[0]["rola"] = d["rola"];
                nr[0]["login"] = d["login"];
                nr[0]["haslo"] = d["haslo"];
            _adapter.Update(set);
        }

        public static void Init() {
            SqlConnection connection = new SqlConnection("Data Source=.;Initial Catalog=real;Integrated Security=true");
            DataSet set = new DataSet();
            set.EnforceConstraints = true;
            new PracownikCreator(set);
            new Klient(set);
            new Pojazd(set);
            new Przeglad(set);
            Pracownik.connection = connection;
            Pracownik.set = set;
        }
    }

    class Program {
        static void Main(string[] args) {
            Pracownik.Init();
            DataRow res = Pracownik.FindById(1);
            Console.WriteLine(string.Format("{0} {1}", res["imie"], res["nazwisko"]));
            DataRow res2 = Pracownik.FindById(2);
            Console.WriteLine(string.Format("{0} {1}", res2["imie"], res2["nazwisko"]));
            foreach(DataRow d in Pracownik.FindAll()) {
                Console.WriteLine(string.Format("{0} {1}", d["imie"], d["nazwisko"]));
            }
            Console.ReadKey();
        }
    }
}