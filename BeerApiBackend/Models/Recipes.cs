using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BeerApiBackend.Models
{
    public partial class Recipes
    {
        public int Id { get; set; }
       [Required]
        public string Name { get; set; }
        public  ICollection<IngredientsInBeers> IngredientsInBeers { get; set; }
    }
}
