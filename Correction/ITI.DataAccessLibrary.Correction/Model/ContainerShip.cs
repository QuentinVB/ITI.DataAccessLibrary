using System;
using System.Collections.Generic;

namespace ITI.DataAccessLibrary.Model
{
    public class ContainerShip
    {
        public int Id { get; set; }
        public string ATISCode { get; set; }
        public string Name { get; set; }
        public Harbor Destination { get; set; }
        public Harbor Origin { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public List<Container> Cargo { get; set; }
        public int Crew { get; set; }
        public int MaxWeight { get; set; }
        public double MaxSpeed { get; set; }
        public int MaxWidth { get; set; }
        public int MaxLength { get; set; }
        public int MaxHeight { get; set; }

       
    }
}
