using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zad1 {
    public class Pracownik {
        public static SqlConnection connection { get; set; }

        public int id { get; private set; }
        public string imie { get; set; }
        public string nazwisko { get; set; }
        public int rola { get; set; }
        public string login { get; set; }
        public string haslo { get; set; }

        private static Pracownik _makePracownik(SqlDataReader reader) {
            Pracownik pracownik = new Pracownik();
            pracownik.id = (int)reader["id"];
            pracownik.imie = (string)reader["imie"];
            pracownik.nazwisko = (string)reader["nazwisko"];
            pracownik.rola = (int)reader["rola"];
            pracownik.login = (string)reader["login"];
            pracownik.haslo = (string)reader["haslo"];
            return pracownik;
        }

        public static Pracownik FindById(int id) {
            SqlCommand cmd = new SqlCommand(string.Format("select * from Pracownik where ID={0}", id), connection);
            SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.SingleRow);
            reader.Read();
            Pracownik result = Pracownik._makePracownik(reader);
            reader.Close();
            return result;
        }

        public static IList<Pracownik> FindAll() {
            SqlCommand cmd = new SqlCommand("select * from Pracownik", connection);
            SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.SingleResult);
            List<Pracownik> results = new List<Pracownik>();
            while(reader.Read()) {
                results.Add(Pracownik._makePracownik(reader));
            }
            reader.Close();
            return results;
        }

        public static IList<Pracownik> FindAll(string where) {
            SqlCommand cmd = new SqlCommand(string.Format("select * from Pracownik where {0}", where), connection);
            SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.SingleResult);
            List<Pracownik> results = new List<Pracownik>();
            while(reader.Read()) {
                results.Add(Pracownik._makePracownik(reader));
            }
            reader.Close();
            return results;
        }

        public void Save() {
            SqlCommand cmd = new SqlCommand(@"insert Pracownik(imie, nazwisko, rola, login, haslo)
            values(@imie, @nazwisko, @rola, @login, @haslo); select scope_identity()", connection);
            cmd.Parameters.Add("@imie", SqlDbType.NVarChar).Value = this.imie;
            cmd.Parameters.Add("@nazwisko", SqlDbType.NVarChar).Value = this.nazwisko;
            cmd.Parameters.Add("@rola", SqlDbType.Int).Value = this.rola;
            cmd.Parameters.Add("@login", SqlDbType.NVarChar).Value = this.login;
            cmd.Parameters.Add("@haslo", SqlDbType.NVarChar).Value = this.haslo;
            this.id = Convert.ToInt32(cmd.ExecuteScalar());
        }

        public void Update() {
            SqlCommand cmd = new SqlCommand(@"update Pracownik
            set imie=@imie, nazwisko=@nazwisko, rola=@rola, login=@login, haslo=@haslo
            where id=@id", connection);
            cmd.Parameters.Add("@imie", SqlDbType.NVarChar).Value = this.imie;
            cmd.Parameters.Add("@nazwisko", SqlDbType.NVarChar).Value = this.nazwisko;
            cmd.Parameters.Add("@rola", SqlDbType.Int).Value = this.rola;
            cmd.Parameters.Add("@login", SqlDbType.NVarChar).Value = this.login;
            cmd.Parameters.Add("@haslo", SqlDbType.NVarChar).Value = this.haslo;
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = this.id;
            cmd.ExecuteNonQuery();
        }

        public void SaveOrUpdate() {
            SqlCommand cmd = new SqlCommand(@"merge Pracownik as target
            using (select @id) as source (id)
            on target.id = source.id
            when matched then update set imie=@imie, nazwisko=@nazwisko, rola=@rola, login=@login, haslo=@haslo
            when not matched then insert (imie, nazwisko, rola, login, haslo) values(@imie, @nazwisko, @rola, @login, @haslo);
            select scope_identity()", connection);
            cmd.Parameters.Add("@imie", SqlDbType.NVarChar).Value = this.imie;
            cmd.Parameters.Add("@nazwisko", SqlDbType.NVarChar).Value = this.nazwisko;
            cmd.Parameters.Add("@rola", SqlDbType.Int).Value = this.rola;
            cmd.Parameters.Add("@login", SqlDbType.NVarChar).Value = this.login;
            cmd.Parameters.Add("@haslo", SqlDbType.NVarChar).Value = this.haslo;
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = this.id;
            this.id = Convert.ToInt32(cmd.ExecuteScalar());
        }

        public bool Equals(Pracownik op) {
            return this.id == op.id
            && this.imie == op.imie
            && this.nazwisko == op.nazwisko
            && this.rola == op.rola
            && this.login == op.login
            && this.haslo == op.haslo;
        }
    }

    class Program {
        static void Main(string[] args) {
            SqlConnection connection = new SqlConnection("Data Source=.;Initial Catalog=real;Integrated Security=true");
            connection.Open();
            Pracownik.connection = connection;
            Pracownik p1 = Pracownik.FindById(1);
            Console.WriteLine(string.Format("Single Result for ID=1: {0} {1}", p1.imie, p1.nazwisko));
            Console.WriteLine("---------------------------------");
            foreach(Pracownik p in Pracownik.FindAll()) {
                Console.WriteLine(string.Format("Result for ID={0}: {1} {2}", p.id, p.imie, p.nazwisko));
            }
            Pracownik p2 = Pracownik.FindById(1);
            Console.WriteLine(string.Format("{0} {1}=={3} {4}: {2}", p1.imie, p1.nazwisko, p1.Equals(p2), p2.imie, p2.nazwisko));
            Pracownik p3 = Pracownik.FindById(2);
            Console.WriteLine(string.Format("{0} {1}=={3} {4}: {2}", p1.imie, p1.nazwisko, p1.Equals(p3), p3.imie, p3.nazwisko));
            Pracownik np = new Pracownik();
            np.imie = "ATEST";
            np.nazwisko = "TSETA";
            np.login = "ETAST";
            np.haslo = "@#*@!!(@@*#";
            np.rola = 1;
            np.Save();
            np.imie = "NEWONE";
            np.Update();
            Pracownik np2 = new Pracownik();
            np2.imie = "nexttest";
            np2.nazwisko = "testnext";
            np2.rola = 2;
            np2.login = "mojlogin";
            np2.haslo = "@#@#(@!#M@O!@$@IR#IR@!@KD";
            np2.SaveOrUpdate();
            np2.login = "nowylogin";
            np2.SaveOrUpdate();
            Console.ReadKey();
            connection.Close();
        }
    }
}