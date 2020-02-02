using System;
using System.Collections.Generic;

namespace ITI.DataAccessLibrary.Correction.Model
{
    public class ContainerShipRedux
    {
        public int Id { get; set; }
        public string ATISCode { get; set; }
        public string Name { get; set; }
        public int Crew { get; set; }
        public int ContainerCount { get; set; }
        public int TotalWeight { get; set; }
    }
}
