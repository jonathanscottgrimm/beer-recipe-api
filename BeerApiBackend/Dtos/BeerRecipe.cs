using BeerApiBackend.Models;
using System.Collections.Generic;

namespace BeerApiBackend.Dtos
{
    public class BeerRecipe
    {
        public int RecipeId { get; set; }
        public string Name { get; set; }
        public IEnumerable<Ingredients> IngredientsList { get; set; }
    }
}