using Dapper;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppDapper.Models;

namespace Services
{
    public class StrojServices:IStrojServices
    {
        private string connectionstring = "User ID=postgres;Password=key;Host=localhost;Port=6432;Database=crudDB;";
        public IEnumerable<Stroj> GetStrojs() 
        {
            string selectQuerry = "SELECT * FROM Strojevi";
            using IDbConnection db = new NpgsqlConnection(connectionstring);
            if (db.State == ConnectionState.Closed)
                db.Open();
            return db.Query<Stroj>(selectQuerry).ToList();
        }

        public void CreateStrojs(Stroj stroj)
        {
           
            string insertQuery = "INSERT INTO Strojevi(naziv_stroja) VALUES (@naziv_stroja)";
            using IDbConnection db = new NpgsqlConnection(connectionstring);
            if (db.State == ConnectionState.Closed)
                db.Open();
            var condition = GetStrojs().Where(p=>p.naziv_stroja==stroj.naziv_stroja);
            if (condition.Count()>0)
                {
                return;
                }
            db.Execute(insertQuery, stroj);
        }

        public ViewModel GetStrojById(int id)
        {
            string selectQuerry = "SELECT * FROM Strojevi WHERE id=@id";
            string selectAllKvarovi = "SELECT * FROM Kvarovi WHERE naziv_stroja=@naziv_stroja";
            string selectStartDates = "SELECT vrijeme_pocetka FROM(SELECT * from Kvarovi WHERE vrijeme_zavrsetka IS NOT NULL and status_kvara=true and naziv_stroja=@naziv_stroja) kp";
            string selectEndDates = "SELECT vrijeme_zavrsetka FROM(SELECT * from Kvarovi WHERE vrijeme_zavrsetka IS NOT NULL and status_kvara=true and naziv_stroja=@naziv_stroja) kz";
            using IDbConnection db = new NpgsqlConnection(connectionstring);
            if (db.State == ConnectionState.Closed)
                db.Open();
            ViewModel mymodel = new ViewModel();
           
            string naziv_stroja = db.Query<Stroj>(selectQuerry, new Stroj { Id = id }).ToList().FirstOrDefault().naziv_stroja;

            IEnumerable<DateTime> starts = db.Query<DateTime>(selectStartDates, new Kvar { naziv_stroja = naziv_stroja }).ToList();
            IEnumerable<DateTime> ends = db.Query<DateTime>(selectEndDates, new Kvar { naziv_stroja = naziv_stroja }).ToList();
            long startsAvg = 0;
            long endsAvg = 0;
            foreach (DateTime date in starts)
            {
                startsAvg = date.Ticks + startsAvg;
            }
            foreach (DateTime date in ends)
            {
                endsAvg = date.Ticks + endsAvg;
            }

            mymodel.trajanje = (endsAvg - startsAvg) / 60 / 60 / 10000000;  // duration of malfunctions in hours
            mymodel.naziv = naziv_stroja;
            List<Kvar> kvarovi = db.Query<Kvar>(selectAllKvarovi, new Kvar { naziv_stroja = naziv_stroja }).ToList();
            mymodel.Kvarovi = kvarovi;
            return mymodel;
        }

        public void UpdateKvar(int id, Stroj stroj)
        {
            string updateQuery = "UPDATE Strojevi SET naziv_stroja=@naziv_stroja WHERE id=@id";
            using IDbConnection db = new NpgsqlConnection(connectionstring);
            if (db.State == ConnectionState.Closed)
                db.Open();
            db.Query(updateQuery, stroj);
        }
        public void DeleteKvar(int id)
        {
            string deletequery = "DELETE FROM strojevi WHERE id=@id";
            using IDbConnection db = new NpgsqlConnection(connectionstring);
            if (db.State == ConnectionState.Closed)
                db.Open();
            db.Query(deletequery, new { id = id });
        }

        public Stroj GetByIdUpdate(int id)
        {
            string selectQuery = "SELECT * FROM Strojevi WHERE id=@id";
            using IDbConnection db = new NpgsqlConnection(connectionstring);
            if (db.State == ConnectionState.Closed)
                db.Open();
            return db.Query<Stroj>(selectQuery, new Stroj { Id = id }).FirstOrDefault();
        }
    }
}
