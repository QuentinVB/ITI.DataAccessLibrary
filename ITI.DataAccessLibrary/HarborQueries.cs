using System;
using System.Collections.Generic;
using System.Data.SQLite;
using ITI.DataAccessLibrary.Model;

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
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get all harbors name by country
        /// </summary>
        /// <returns></returns>
        public List<Harbor> GetHarborByCountry()
        {
            throw new NotImplementedException();
        }

        public Harbor GetHarborById(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Insert a harbor into the database
        /// </summary>
        /// <param name="harbor"></param>
        public void InsertHarbor( Harbor harbor )
        {
            throw new NotImplementedException();
        }
    }
}
