using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BeerTap.Domain
{
    public class Office
    {
        public Office() : this("")
        {
            
        }
        public Office(string name)
        {
            this.Name = name;
            this.Kegs = new List<Keg>();
        }

        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public IList<Keg> Kegs { get; set; }
    }
}
