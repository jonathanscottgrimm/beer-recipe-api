using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BeerApiBackend.Models;
using BeerApiBackend.Dtos;

namespace BeerApiBackend.Controllers
{
   
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class BeerController : Controller
    {
        private readonly Beer_contextDAL _beerData;

        public BeerController(Beer_contextDAL beerData) => _beerData = beerData;

        /// <summary>
        /// Gets all beer recipes.
        /// </summary>
        /// <returns>A list of BeerRecipe</returns>
        /// <response code="200">Returns the list of beer recipes</response>
        /// <response code="404">If there are no beer recipes</response>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<BeerRecipe>))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetRecipes()
        {
            var recipes = await _beerData.GetBeerRecipes();

            if (recipes == null || !recipes.Any())
                return NotFound();

            return Ok(recipes);
        }

        /// <summary>
        /// Gets a beer recipe by id.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>BeerRecipe</returns>
        /// <response code="200">Returns a beer recipe</response>
        /// <response code="404">If no beer recipe with that id is found</response>
        [HttpGet("{Id}", Name = "GetRecipe")]
        [ProducesResponseType(200, Type = typeof(BeerRecipe))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetRecipe([FromRoute] int Id)
        {
            var recipe = await _beerData.GetBeerRecipe(Id);

            if (recipe == null)
                return NotFound();

            return Ok(recipe);
        }



        /// <summary>
        /// Searches ingredients by all or part of a recipe name.
        /// </summary>
        /// <param name="query">the name of the recipe</param>
        /// <returns>List of BeerRecipe</returns>
        /// <response code="200">A list of beer recipes</response>
        /// <response code="404">No beer recipes found for the query</response>
        [HttpGet("RecipeSearch")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<BeerRecipe>))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> RecipeSearch([FromQuery] string query)
        {
            var recipes = await _beerData.GetBeerRecipeSearch(query);

            if (recipes == null || !recipes.Any())
                return NotFound();

            return Ok(recipes);
        }

        /// <summary>
        /// Searches ingredients by all or part of an ingredient name.
        /// </summary>
        /// <param name="query">the name of the ingredient</param>
        /// <returns>List of Ingredients</returns>
        /// <response code="200">A list of Ingredients</response>
        /// <response code="404">No ingredients found for the query</response>
        [HttpGet("IngredientSearch")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Ingredients>))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> IngredientSearch([FromQuery] string query)
        {
            var ingredients = await _beerData.GetIngredientsSearch(query);
            if (ingredients == null || !ingredients.Any())
                return NotFound();

            return Ok(ingredients);
        }

        /// <summary>
        /// Adds a beer recipe to the Recipes table and the corresponding ingredients to the 
        /// IngredientsInBeers table.
        /// </summary>
        /// <param name="beerRecipe">The BeerRecipe object.</param>
        /// <returns>The newly created BeerRecipe</returns>
        /// <response code="200">The new beer recipe</response>
        /// <response code="400">Invalid BeerRecipe object</response>
        [HttpPost]
        [Route("/AddRecipe")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> AddBeerRecipe([FromBody]BeerRecipe beerRecipe)
        {
            if (!ModelState.IsValid)
                return BadRequest();

         var newId =   await _beerData.AddBeerRecipe(beerRecipe);
      
            return CreatedAtAction("/GetRecipe", new { id = newId });
        }
    }
}