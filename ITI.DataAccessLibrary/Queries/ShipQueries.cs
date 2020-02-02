using System;
using System.Collections.Generic;
using System.Data.SQLite;
using ITI.DataAccessLibrary.Model;

namespace ITI.DataAccessLibrary
{
    public class ShipQueries : Queries
    {
        /// <summary>
        /// Return all ships in the database
        /// </summary>
        /// <returns></returns>
        public List<ContainerShip> GetAllShips()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Return the name of ships ordered by crew size
        /// </summary>
        /// <returns></returns>
        public List<ContainerShip> GetShipsByCrew()
        {
            throw new NotImplementedException();
        }

       

        /// <summary>
        /// Return a ship by it's Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ContainerShip GetShipById(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Return the ship name ordered by departure time
        /// </summary>
        /// <returns></returns>
        public List<ContainerShip> GetShipByDepartureTime()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Return all ships with an empty cargo
        /// </summary>
        /// <returns></returns>
        public List<ContainerShip> GetAllEmptyShips()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Return all ships ordered by volume
        /// </summary>
        /// <returns></returns>
        public List<ContainerShip> GetShipByVolume()
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Insert shipinto the database
        /// </summary>
        /// <param name="ship"></param>
        public void InsertShip( ContainerShip ship )
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Delete the given ship
        /// </summary>
        /// <param name="ship"></param>
        public void DeleteShip( ContainerShip ship)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Get the all the ship redux form
        /// </summary>
        /// <returns></returns>
        public List<ContainerShipRedux> GetAllShipsRedux()
        {
            throw new NotImplementedException();
        }
    }
}
