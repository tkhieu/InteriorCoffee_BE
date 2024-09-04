using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InteriorCoffee.Domain.Models.Documents
{
    public class ProductImages
    {
        public string Thumbnail { get; set; }
        public List<string> NormalImages { get; set; }
    }
}
