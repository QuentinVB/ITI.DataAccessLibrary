﻿using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITI.DataAccessLibrary.Model;

namespace ITI.DataAccessLibrary
{
    public class ShipQueries : Queries
    {
        internal HarborQueries harborQueries = new HarborQueries();

        /// <summary>
        /// Return all ships in the database
        /// </summary>
        /// <returns></returns>
        public List<ContainerShip> GetAllShips()
        {
            string query = "SELECT * FROM SHIP";

            List<ContainerShip> result = new List<ContainerShip>();

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
                            ContainerShip ship = new ContainerShip
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Name = reader["Name"].ToString(),
                                ATISCode = reader["ATISCode"].ToString(),
                                Origin = harborQueries.GetHarborById(Convert.ToInt32(reader["Origin"])),
                                Destination = harborQueries.GetHarborById(Convert.ToInt32(reader["Destination"])),
                                DepartureTime = DateTime.Parse(reader["DepartureTime"].ToString()),
                                ArrivalTime = DateTime.Parse(reader["ArrivalTime"].ToString()),
                                Crew = Convert.ToInt32(reader["Crew"]),
                                MaxWeight = Convert.ToInt32(reader["MaxWeight"]),
                                MaxSpeed = Convert.ToDouble(reader["MaxSpeed"]),
                                MaxWidth = Convert.ToInt32(reader["MaxWidth"]),
                                MaxHeight = Convert.ToInt32(reader["MaxHeight"]),
                                MaxLength = Convert.ToInt32(reader["MaxLength"])
                            };
                            result.Add(ship);
                        }
                        
                    }
                }
                _connexion.Close();
            }

            return result;
        }

        /// <summary>
        /// Return the name of ships ordered by crew size
        /// </summary>
        /// <returns></returns>
        public List<ContainerShip> GetShipsByCrew()
        {
            string query = "SELECT * " +
                "FROM SHIP " +
                "ORDER BY CREW";

            List<ContainerShip> result = new List<ContainerShip>();

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
                            ContainerShip ship = new ContainerShip
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Name = reader["Name"].ToString(),
                                ATISCode = reader["ATISCode"].ToString(),
                                Origin = harborQueries.GetHarborById(Convert.ToInt32(reader["Origin"])),
                                Destination = harborQueries.GetHarborById(Convert.ToInt32(reader["Destination"])),
                                DepartureTime = DateTime.Parse(reader["DepartureTime"].ToString()),
                                ArrivalTime = DateTime.Parse(reader["ArrivalTime"].ToString()),
                                Crew = Convert.ToInt32(reader["Crew"]),
                                MaxWeight = Convert.ToInt32(reader["MaxWeight"]),
                                MaxSpeed = Convert.ToDouble(reader["MaxSpeed"]),
                                MaxWidth = Convert.ToInt32(reader["MaxWidth"]),
                                MaxHeight = Convert.ToInt32(reader["MaxHeight"]),
                                MaxLength = Convert.ToInt32(reader["MaxLength"])
                            };
                            result.Add(ship);
                        }

                    }
                }
                _connexion.Close();
            }

            return result;
        }

        /// <summary>
        /// Return a ship by it's Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ContainerShip GetShipById(int id)
        {
            string query = "SELECT *" +
                "FROM SHIP S " +
                $"WHERE S.ID = {id}";

            ContainerShip result=null;

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
                            result = new ContainerShip();
                            result.Id = Convert.ToInt32(reader["Id"]);
                            result.ATISCode = reader["ATISCode"].ToString();
                            result.Name = reader["Name"].ToString();
                            result.Origin = harborQueries.GetHarborById(Convert.ToInt32(reader["Origin"]));
                            result.Destination = harborQueries.GetHarborById(Convert.ToInt32(reader["Destination"]));
                            result.DepartureTime = DateTime.Parse(reader["DepartureTime"].ToString());
                            result.ArrivalTime = DateTime.Parse(reader["ArrivalTime"].ToString());
                            result.Crew = Convert.ToInt32(reader["Crew"]);
                            result.MaxWeight = Convert.ToInt32(reader["MaxWeight"]);
                            result.MaxSpeed = Convert.ToDouble(reader["MaxSpeed"]);
                            result.MaxWidth = Convert.ToInt32(reader["MaxWidth"]);
                            result.MaxHeight = Convert.ToInt32(reader["MaxHeight"]);
                            result.MaxLength = Convert.ToInt32(reader["MaxLength"]);
                        }
                    }
                }
                _connexion.Close();
            }

            return result;
        }

        /// <summary>
        /// Return the ship name ordered by departure time
        /// </summary>
        /// <returns></returns>
        public List<ContainerShip> GetShipByDepartureTime()
        {
            string query = "SELECT NAME, DEPARTURETIME " +
                "FROM SHIP " +
                "ORDER BY DEPARTURETIME";

            return null;
        }

        //public List<ContainerShip> GetShipWIthHarborInCountry()
        //{
        //    string query = "SELECT SH.NAME AS SHIPNAME, H.NAME AS HARBORNAME " +
        //        "FROM SHIP" +
        //        "INNER JOIN HARBOR ON SHIP.ATIS = HARBOR.CURRENTSHIP" +
        //        "WHERE HARBOR.COUNTRY = 'FR'";
        //    return null;
        //}

        /// <summary>
        /// Return all ships with an empty cargo
        /// </summary>
        /// <returns></returns>
        public List<ContainerShip> GetAllEmptyShips()
        {
            string query = "SELECT * " +
                "FROM SHIP S " +
                "WHERE S.ATIS NOT IN ( " +
                    "SELECT DISTINCT SH.NAME, C.REFERENCE " +
                    "FROM SHIP SH, CONTAINER C " +
                    "WHERE SH.ATIS = C.CURRENTSHIP" +
                ")";

            return null;
        }

        /// <summary>
        /// Return all ships ordered by volume
        /// </summary>
        /// <returns></returns>
        public List<ContainerShip> GetShipByVolume()
        {
            string query = " SELECT NAME, ( MAXWIDTH * MAXLENGTH * MAXHEIGHT ) AS VOLUME " +
                "FROM SHIP" +
                "ORDER BY VOLUME";

            return null;
        }


        /// <summary>
        /// Insert shipinto the database
        /// </summary>
        /// <param name="ship"></param>
        public void InsertShip( ContainerShip ship )
        {
            string query = $"INSERT INTO SHIP"+
                $"(Id,Name,ATISCode,Origin,Destination,DepartureTime,ArrivalTime,Crew,MaxWeight,MaxSpeed,MaxWidth,MaxHeight,MaxLength) "+
                $"VALUES(" +
                $"{ship.Id}, "+
                $"'"+ship.Name+"', " +
                $"'"+ship.ATISCode + "', " +
                $"{ship.Origin.Id}, " +
                $"{ship.Destination.Id}, " +
                $"'{ship.DepartureTime.ToString("yyyy-MM-dd HH:mm:ss.fff")}', " +
                $"'{ship.ArrivalTime.ToString("yyyy-MM-dd HH:mm:ss.fff")}', " +
                $"{ship.Crew}, " +
                $"{ship.MaxWeight}, "+
                $"{ship.MaxSpeed}, " +
                $"{ship.MaxWidth}, " +
                $"{ship.MaxHeight}, " +
                $"{ship.MaxLength}" +
                $");";

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

        /// <summary>
        /// Delete the given ship
        /// </summary>
        /// <param name="ship"></param>
        public void DeleteShip( ContainerShip ship )
        {
            string query = $"DELETE FROM SHIP " +
                $"WHERE ATISCode = '{ship.ATISCode}' ";

            using (_connexion = new SQLiteConnection(_connString))
            {
                _connexion.Open();
                using (SQLiteCommand command = _connexion.CreateCommand())
                {
                    command.CommandText = query;
                    command.ExecuteNonQuery();
                }       
                
                _connexion.Close();
            }
        }

        /// <summary>
        /// Update a ship's destination and arrival date
        /// </summary>
        /// <param name="ship"></param>
        public void UpdateShipName( int id, string name )
        {
            string query = $"UPDATE SHIP" +
                $"SET NAME = '{name}'" +
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

        /// <summary>
        /// Get the all the ship redux form
        /// </summary>
        /// <returns></returns>
        public List<ContainerShipRedux> GetAllShipsRedux()
        {
            string query = @"SELECT Id,ATISCode,Name,Crew, c.ContainerCount as ContainerCount, c.TotalWeight as TotalWeight
                FROM (
                    SELECT CurrentShip, COUNT(Id) as ContainerCount, SUM(Weight) as TotalWeight
                    FROM Container 
                    GROUP BY CurrentShip) c , 
                    Ship s 
                WHERE c.CurrentShip = s.Id"; 

            List<ContainerShipRedux> result = new List<ContainerShipRedux>();

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
                            ContainerShipRedux ship = new ContainerShipRedux
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Name = reader["Name"].ToString(),
                                ATISCode = reader["ATISCode"].ToString(),
                                Crew = Convert.ToInt32(reader["Crew"]),
                                ContainerCount = Convert.ToInt32(reader["ContainerCount"]),
                                TotalWeight = Convert.ToInt32(reader["TotalWeight"])
                            };
                            result.Add(ship);
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
