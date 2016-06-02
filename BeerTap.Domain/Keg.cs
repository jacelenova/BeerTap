using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeerTap.Domain
{
    public class Keg
    {
        public Keg() : this("")
        {
            
        }

        public Keg(string name)
        {
            this.Content = MaxContent;
            this.Name = name;
        }

        [Key]
        public int Id { get; set; }
        
        public string Name { get; set; }

        public int Content { get; set; }

        public int OfficeId { get; set; }

        [ForeignKey("OfficeId")]
        public Office Office { get; set; }

        [NotMapped]
        public const int MaxContent = 10000;

    }
}
