using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FortCode.Model
{
    public class Fort
    {
        // external Id, easier to reference: 1,2,3 or A, B, C etc.
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }       
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

    }
      public class UserLocation
    {
        public string Id { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Email { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

    }


}
