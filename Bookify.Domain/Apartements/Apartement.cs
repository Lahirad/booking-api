using Bookify.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookify.Domain.Apartements
{
   public sealed class Apartement : Entity
    {
        public Apartement (Guid id
                          ,Name name
                          ,Description description
                          ,Address address
                          ,Money price
                          ,Money cleaningFee
                          , IReadOnlyList<Amenity> amenities):base(id)
        {
            Name = name;
            Description = description;
            Address = address;
            Price = price;
            CleaningFee = cleaningFee;
            Amenities = amenities;
        }

        private Apartement()
        {
            
        }
        public Name Name { get; private set; }
        public Description Description { get; private set; }
        public Address Address  { get; private set; }
        public Money Price { get; private set; }
        public Money CleaningFee { get; private set; }
        public DateTime? LastBookedOnUtc { get; internal set; }
        public IReadOnlyList<Amenity> Amenities { get; private set; }
    }
}
