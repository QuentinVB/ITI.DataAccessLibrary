using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITI.DataAccessLibrary.Model;

namespace ITI.DataAccessLibrary
{
    public class ContainerQueries : Queries
    {

        internal ShipQueries shipQueries = new ShipQueries();
        internal HarborQueries harborQueries = new HarborQueries();
        /// <summary>
        /// Return all container in the database
        /// </summary>
        /// <returns></returns>
        public List<Container> GetAllContainers()
        {
            string query = "SELECT * FROM Container";

            List<Container> result = new List<Container>();

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
                            Container container = new Container
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Reference = reader["Reference"].ToString(),
                                Content = reader["Content"].ToString(),
                                CurrentShip = shipQueries.GetShipById(Convert.ToInt32(reader["CurrentShip"])),
                                Origin = harborQueries.GetHarborById(Convert.ToInt32(reader["Origin"])),
                                Destination = harborQueries.GetHarborById(Convert.ToInt32(reader["Destination"])),
                                IsOpenTop = Convert.ToBoolean(reader["IsOpenTop"]),
                                EmptyWeigth = Convert.ToInt32(reader["EmptyWeigth"]),
                                Weight = Convert.ToInt32(reader["Weight"]),
                                X = Convert.ToInt32(reader["X"]),
                                Y = Convert.ToInt32(reader["Y"]),
                                Z = Convert.ToInt32(reader["Z"])
                            };

                            result.Add(container);
                        }

                    }
                }
                _connexion.Close();
            }

            return result;
        }

        public List<Container> GetContainersFromShip(int shipId)
        {
            string query = "SELECT *" +
                "FROM Container " +
                $"WHERE CurrentShip = {shipId}";

            List<Container> result = new List<Container>();

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
                            Container container = new Container
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Reference = reader["Reference"].ToString(),
                                Content = reader["Content"].ToString(),
                                CurrentShip = shipQueries.GetShipById(Convert.ToInt32(reader["CurrentShip"])),
                                Origin = harborQueries.GetHarborById(Convert.ToInt32(reader["Origin"])),
                                Destination = harborQueries.GetHarborById(Convert.ToInt32(reader["Destination"])),
                                IsOpenTop = Convert.ToBoolean(reader["IsOpenTop"]),
                                EmptyWeigth = Convert.ToInt32(reader["EmptyWeigth"]),
                                Weight = Convert.ToInt32(reader["Weight"]),
                                X = Convert.ToInt32(reader["X"]),
                                Y = Convert.ToInt32(reader["Y"]),
                                Z = Convert.ToInt32(reader["Z"])
                            };

                            result.Add(container);
                        }
                    }
                }
                _connexion.Close();
            }
            return result;
        }

        public List<MerchandiseSummary> GetMerchandisesSummary()
        {
            string query = @"SELECT Content, COUNT(Id) as TotalContainerCount,  SUM(Weight) TotalWeight 
                FROM Container GROUP BY Content
                ORDER BY TotalWeight";

            List<MerchandiseSummary> result = new List<MerchandiseSummary>();

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
                            MerchandiseSummary merchandiseSummary = new MerchandiseSummary
                            {
                                Content = reader["Content"].ToString(),
                                TotalContainerCount = Convert.ToInt32(reader["TotalContainerCount"]),
                                TotalWeight = Convert.ToInt32(reader["TotalWeight"])
                            };
                            result.Add(merchandiseSummary);
                        }
                    }
                }
                _connexion.Close();
            }
            return result;
            //throw new NotImplementedException();
        }
    }
}
