using System.ComponentModel.DataAnnotations;

namespace PoeAgriEnergyConnect.Models
{
    public class Product
    {
      
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string Category { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime ProductionDate { get; set; }



        public string FarmerId { get; set; }
        //public Farmer Farmer { get; set; }
    }
}
