using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.DataAccessLibrary.Model
{
    public class Container
    {
        public int Id { get; set; }
        public string Reference { get; set; }
        public string Content { get; set; }
        public ContainerShip CurrentShip { get; set; }
        public Harbor Origin { get; set; }
        public Harbor Destination { get; set; }
        public bool IsOpenTop { get; set; }
        public int EmptyMass { get; set; }
        public int MaxMass { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }
    }
}
