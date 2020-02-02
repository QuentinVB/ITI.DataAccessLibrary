using ITI.DataAccessLibrary.Model;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.DataAccessLibrary
{
    public class HarborQueries : Queries
    {
        /// <summary>
        /// Gets all harbors.
        /// </summary>
        /// <returns>A list of Harbors</returns>
        public List<Harbor> GetAllHarbor()
        {
            string query = "SELECT * FROM HARBOR";

            List<Harbor> result = new List<Harbor>();

            using (_connexion = new SQLiteConnection(_connString))
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

                            Harbor h = new Harbor
                            {
                                Country = country,
                                Id = id,
                                Latitude = lati,
                                Longitude = longi,
                                Name = name
                            };

                            result.Add(h);
                        }
                    }
                }
                _connexion.Close();
            }

            return result;
        }

        /// <summary>
        /// Get all harbors name by country
        /// </summary>
        /// <returns></returns>
        public List<Harbor> GetHarborByCountry()
        {
            string query = "SELECT COUNTRY, NAME FROM HARBOR ORDER BY COUNTRY";

            List<Harbor> result = new List<Harbor>();

            using (_connexion = new SQLiteConnection(_connString))
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

        public Harbor GetHarborById(int id)
        {
            string query = "SELECT *" +
                "FROM HARBOR H " +
                $"WHERE H.ID = {id}";

            Harbor result = new Harbor();

            using (_connexion = new SQLiteConnection(_connString))
            {
                _connexion.Open();
                using (SQLiteCommand command = _connexion.CreateCommand())
                {
                    command.CommandText = query;
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Id = id;
                            result.Name = reader.GetString(1);
                            result.Country = reader.GetString(2);
                            result.Latitude = reader.GetDouble(3);
                            result.Longitude = reader.GetDouble(4);
                        }
                    }
                }
                _connexion.Close();
            }

            return result;
        }

        /// <summary>
        /// Insert a harbor into the database
        /// </summary>
        /// <param name="harbor"></param>
        public void InsertHarbor( Harbor harbor )
        {
            string query = "INSERT INTO HARBOR VALUES(" +
                $"{harbor.Id}, " +
                $"{harbor.Name}, " +
                $"{harbor.Country}, " +
                $"{harbor.Latitude}, " +
                $"{harbor.Longitude}, " +
                ")";

            using (_connexion = new SQLiteConnection(_connString))
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

        public void DeleteHarbor( Harbor harbor )
        {
            string query = $"DELETE FROM HARBOR" +
                $"WHERE {harbor.Id}";

            using (_connexion = new SQLiteConnection(_connString))
            {
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

        public void UpdateHarbor( int id, string country )
        {
            string query = $"UPDATE HARBOR" +
                $"SET COUNTRY = '{country}'" +
                $"WHERE ID = {id}";

            using (_connexion = new SQLiteConnection(_connString))
            {
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
