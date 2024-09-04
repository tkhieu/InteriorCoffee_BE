using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InteriorCoffee.Domain.Models.Documents
{
    public class DesignNonProducts
    {
        public string _id { get; set; }
        public string Type { get; set; }
        public Position Position { get; set; }
        public Scale Scale { get; set; }
        public Rotation Rotation { get; set; }
    }
}
