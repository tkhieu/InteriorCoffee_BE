using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InteriorCoffee.Domain.Models
{
    public class Voucher
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id {  get; set; } = null!;
        public string Code {  get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int DiscountPercentage { get; set; }
        public string Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int MaxUse {  get; set; }
        public double MinOrderValue { get; set; }
        public List<string> UsedAccountIds { get; set; }

        public string TypeId { get; set; }
    }
}
