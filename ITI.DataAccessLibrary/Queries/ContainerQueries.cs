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
            throw new NotImplementedException();
        }

        public List<Container> GetContainersFromShip(int shipId)
        {
            throw new NotImplementedException();

        }

        public List<MerchandiseSummary> GetMerchandisesSummary()
        {
            throw new NotImplementedException();
        }
    }
}
