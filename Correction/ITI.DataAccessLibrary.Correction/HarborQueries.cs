using ITI.DataAccessLibrary.Correction.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.DataAccessLibrary.Correction
{
    public class HarborQueries
    {
        public List<Harbor> GetAllHarbor()
        {
            string query = "SELECT * FROM HARBOR";

            return null;
        }

        public List<Harbor> GetHarborByCountry()
        {
            string query = "SELECT COUNTRY, NAME FROM HARBOUR ORDER BY COUNTRY";

            return null;
        }

        public void InsertHarbor( Harbor harbor )
        {
            string query = "INSERT INTO HARBOR VALUES(" +
                $"{harbor.Id}, " +
                $"{harbor.Name}, " +
                $"{harbor.LocalName}, " +
                $"{harbor.Country}, " +
                $"{harbor.Latitude}, " +
                $"{harbor.Longitude}, " +
                ")";
        }
    }
}
