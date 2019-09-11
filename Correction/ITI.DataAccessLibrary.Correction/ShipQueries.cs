using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITI.DataAccessLibrary.Correction.Model;

namespace ITI.DataAccessLibrary.Correction
{
    public class ShipQueries
    {
        public List<ContainerShip> GetAllShip()
        {
            string query = "SELECT * " +
                "FROM SHIP";

            return null;
        }

        public List<ContainerShip> GetShipByCrew()
        {
            string query = "SELECT NAME, CREW " +
                "FROM SHIP " +
                "ORDER BY CREW";

            return null;
        }

        public List<ContainerShip> GetShipByDepartureTime()
        {
            string query = "SELECT NAME, DEPARTURETIME " +
                "FROM SHIP " +
                "ORDER BY DEPARTURETIME";

            return null;
        }

        public List<ContainerShip> GetShipWIthHarborInCountry()
        {
            string query = "SELECT SH.NAME AS SHIPNAME, H.NAME AS HARBORNAME " +
                "FROM SHIP" +
                "INNER JOIN HARBOR ON SHIP.ATIS = HARBOR.CURRENTSHIP" +
                "WHERE HARBOR.COUNTRY = 'FR'";
            return null;
        }

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

        public List<ContainerShip> GetShipByVolume()
        {
            string query = " SELECT NAME, ( MAXWIDTH * MAXLENGTH * MAXHEIGHT ) AS VOLUME " +
                "FROM SHIP";

            return null;
        }



        public void InsertShip( ContainerShip ship )
        {
            string query = $"INSERT INTO SHIP values(" +
                $"{ship.ATISCode}, " +
                $"{ship.Name}, " +
                $"{ship.Destination}, " +
                $"{ship.Origin}, " +
                $"{ship.Cargo}, " +
                $"{ship.DepartureTime}, " +
                $"{ship.ArrivalTime}, " +
                $"{ship.Crew}, " +
                $"{ship.MaxWeight}, "+
                $"{ship.MaxWidth}, " +
                $"{ship.MaxLength}, " +
                $"{ship.MaxHeight}, " +
                $"{ship.MaxSpeed}, " +
                $")";
        }

        public void DeleteShip( ContainerShip ship )
        {
            string query = $"DELETE FROM SHIP " +
                $"WHERE {ship.ATISCode}";
        }

        public void UpdateShip( ContainerShip ship )
        {

        }


    }
}
