using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeerApiBackend.Dtos;
using Microsoft.EntityFrameworkCore;

namespace BeerApiBackend.Models
{
    public class Beer_contextDAL
    {
        private readonly BeerAppDbContext _context;

        public Beer_contextDAL(BeerAppDbContext context)
        {
            _context = context;
        }

        public async Task<List<BeerRecipe>> GetBeerRecipes()
        {
            try
            {
                var recipes = _context.Recipes.Select(br => new BeerRecipe
                {
                    Name = br.Name,
                    RecipeId = br.Id,
                    IngredientsList = br.IngredientsInBeers.Select(i => new Ingredients
                    {
                        Id = i.Ingredients.Id,
                        Name = i.Ingredients.Name
                    }).ToList()
                });


                return await recipes.ToListAsync();
            }

            catch
            {
                throw;
            }

        }


        public async Task<BeerRecipe> GetBeerRecipe(int recipeId)
        {
            try
            {
                var recipe = _context.Recipes
                       .Where(r => r.Id == recipeId).Select(br => new BeerRecipe
                       {
                           Name = br.Name,
                           RecipeId = br.Id,
                           IngredientsList = br.IngredientsInBeers.Select(i => new Ingredients
                           {
                               Id = i.Ingredients.Id,
                               Name = i.Ingredients.Name
                           }).ToList()
                       });

                return await recipe.FirstOrDefaultAsync();
            }

            catch
            {
                throw;
            }
        }

        public async Task<IEnumerable<BeerRecipe>> GetBeerRecipeSearch(string recipeQuery)
        {
            try
            {
                var searchResults = _context.Recipes.Where(r => r.Name.StartsWith(recipeQuery))
                       .Select(recipes => new BeerRecipe
                       {
                           Name = recipes.Name,
                           RecipeId = recipes.Id,
                           IngredientsList = recipes.IngredientsInBeers.Select(i => new Ingredients
                           {
                               Name = i.Ingredients.Name,
                               Id = i.Ingredients.Id
                           }).ToList()
                       });

                return await searchResults.ToListAsync();
            }
            catch
            {
                throw;
            }
        }


        public async Task<IEnumerable<Ingredients>> GetIngredientsSearch(string ingredientQuery)
        {
            try
            {
                var searchResult = _context.Ingredients.Where(i => i.Name.StartsWith(ingredientQuery));

                return await searchResult.ToListAsync();
            }

            catch
            {
                throw;
            }
        }


        public async Task<int> AddBeerRecipe(BeerRecipe beerRecipe)
        {

            var newRecipe = new Recipes
            {
                Name = beerRecipe.Name
            };

            var newRecipeIngredientsList = beerRecipe.IngredientsList.Select(x => new Ingredients
            {
                Name = x.Name,
                Id = x.Id
            });

            try
            {
                var recipe = await _context.Set<Recipes>().AddAsync(newRecipe);

                foreach (var ingredient in beerRecipe.IngredientsList)
                {
                    IngredientsInBeers ingredientInRecipe = new IngredientsInBeers
                    {
                        RecipeId = recipe.Entity.Id,
                        IngredientId = ingredient.Id
                    };

                    await _context.Set<IngredientsInBeers>().AddAsync(ingredientInRecipe);
                }

                await _context.SaveChangesAsync();

                return newRecipe.Id;
            }

            catch
            {
                throw;
            }

        }
    }
}
