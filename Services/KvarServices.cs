using Dapper;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using WebAppDapper.Models;


namespace Services
{

    public class KvarServices : IKvarServices
    {
        private string connectionstring="User ID=postgres;Password=key;Host=localhost;Port=6432;Database=crudDB;";

        public IEnumerable<Kvar> GetKvars() {

            using IDbConnection db = new NpgsqlConnection(connectionstring);
            if (db.State == ConnectionState.Closed)
                db.Open();
            string selectQuerry = "SELECT * FROM Kvarovi";
            return db.Query<Kvar>(selectQuerry).ToList();
        }

        public void CreateKvar(Kvar kvar)
        {
            string insertQuery = "INSERT INTO Kvarovi(naziv_kvara, naziv_stroja, prioritet, vrijeme_pocetka, vrijeme_zavrsetka, detaljni_opis, status_kvara) " +
                    "VALUES (@naziv_kvara, @naziv_stroja, @prioritet, @vrijeme_pocetka, @vrijeme_zavrsetka, @detaljni_opis, @status_kvara)";
            using IDbConnection db = new NpgsqlConnection(connectionstring);
            if (db.State == ConnectionState.Closed)
                db.Open();
            var allOfEm= GetKvars().Where(p => p.naziv_stroja == kvar.naziv_stroja);
            var condition = allOfEm.Where(p=>p.status_kvara==false);
            if (condition.Count() > 0)
            {
                return;
            }
            db.Execute(insertQuery, kvar);
        }

        public Kvar GetKvarById(int id)
        {
            string selectQuery = "SELECT * FROM Kvarovi WHERE id=@id";
            using IDbConnection db = new NpgsqlConnection(connectionstring);
            if (db.State == ConnectionState.Closed)
                db.Open();
            Kvar kvarovi = db.Query<Kvar>(selectQuery, new Kvar { Id = id }).FirstOrDefault();
            return kvarovi;
        }
        public void UpdateKvar(int id, Kvar kvarovi)
        {
            string updateQuery = "UPDATE Kvarovi " +
                "SET naziv_kvara=@naziv_kvara, prioritet=@prioritet, " +
                "vrijeme_pocetka=@vrijeme_pocetka, vrijeme_zavrsetka=@vrijeme_zavrsetka, detaljni_opis=@detaljni_opis," +
                " status_kvara=@status_kvara WHERE id=@id";
            using IDbConnection db = new NpgsqlConnection(connectionstring);
            if (db.State == ConnectionState.Closed)
                db.Open();
            db.Query(updateQuery, kvarovi);
        }
        public void DeleteKvar(int id)
        {
            string deletequery = "DELETE FROM kvarovi WHERE id=@id";
            using IDbConnection db = new NpgsqlConnection(connectionstring);
            if (db.State == ConnectionState.Closed)
                db.Open();
            db.Query(deletequery, new { id = id });

        }
    } 
}
    
