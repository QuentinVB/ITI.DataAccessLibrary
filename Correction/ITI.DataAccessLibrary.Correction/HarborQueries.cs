﻿using ITI.DataAccessLibrary.Correction.Model;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.DataAccessLibrary.Correction
{
    public class HarborQueries : Queries
    {
        public List<Harbor> GetAllHarbor()
        {
            string query = "SELECT * FROM HARBOR";

            List<Harbor> result = new List<Harbor>();

            using (_connexion = new SQLiteConnection($"Data Source={_fileName};Version=3;"))
            {
                _connexion.Open();
                using (SQLiteCommand command = _connexion.CreateCommand())
                {
                    command.CommandText = query;
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(0);
                            string name = reader.GetString(1);
                            string country = reader.GetString(2);
                            double lati = reader.GetDouble(3);
                            double longi = reader.GetDouble(4);                           

                            Harbor h = new Harbor();
                            h.Country = country;
                            h.Id = id;
                            h.Latitude = lati;
                            h.Longitude = longi;
                            h.Name = name;

                            result.Add(h);
                        }
                    }
                }
                _connexion.Close();
            }

            return result;
        }

        public List<Harbor> GetHarborByCountry()
        {
            string query = "SELECT COUNTRY, NAME FROM HARBOUR ORDER BY COUNTRY";

            List<Harbor> result = new List<Harbor>();

            using (_connexion = new SQLiteConnection($"Data Source={_fileName};Version=3;"))
            {
                _connexion.Open();
                using (SQLiteCommand command = _connexion.CreateCommand())
                {
                    command.CommandText = query;
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string country = reader.GetString(0);
                            string name = reader.GetString(1);

                            Harbor h = new Harbor();
                            h.Country = country;
                            h.Name = name;

                            result.Add(h);
                        }
                    }
                }
                _connexion.Close();
            }
                return result;
        }

        public void InsertHarbor( Harbor harbor )
        {
            string query = "INSERT INTO HARBOR VALUES(" +
                $"{harbor.Id}, " +
                $"{harbor.Name}, " +
                $"{harbor.Country}, " +
                $"{harbor.Latitude}, " +
                $"{harbor.Longitude}, " +
                ")";

            using (_connexion = new SQLiteConnection($"Data Source={_fileName};Version=3;"))
            {
                _connexion.Open();
                using (SQLiteTransaction transaction = _connexion.BeginTransaction())
                {
                    using (SQLiteCommand command = _connexion.CreateCommand())
                    {
                        command.CommandText = query;
                        command.ExecuteNonQuery();
                    }
                    transaction.Commit();
                }
                _connexion.Close();
            }
        }
    }
}
