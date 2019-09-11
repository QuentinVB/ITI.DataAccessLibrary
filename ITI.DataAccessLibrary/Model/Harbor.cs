using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.DataAccessLibrary.Model
{
    public class Harbor
    {
        public int Id { get; set; }
        public string Name;
        public string Country { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }

    }
}
