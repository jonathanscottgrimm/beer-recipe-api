using System.Collections.Generic;

namespace BeerApiBackend.Models
{
    public partial class IngredientsInBeers
    {
        public int RecipeId { get; set; }
        public int IngredientId { get; set; }
        public Recipes Recipes { get; set; }
         public Ingredients Ingredients { get; set; }
      
    }
}
