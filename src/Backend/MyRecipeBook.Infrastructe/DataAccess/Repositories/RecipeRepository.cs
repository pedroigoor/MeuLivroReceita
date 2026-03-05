using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using MyRecipeBook.Domain.Dtos;
using MyRecipeBook.Domain.Entities;
using MyRecipeBook.Domain.Repositories.Recipe;

namespace MyRecipeBook.Infrastructe.DataAccess.Repositories
{
    internal class RecipeRepository : IRecipeWriteOnlyRepository, IRecipeReadOnlyRepository, IRecipeUpdateOnlyRepository
    {
    private readonly MyRecipeBookDbContext _dbContext;

        public RecipeRepository(MyRecipeBookDbContext dbContext) => _dbContext = dbContext;

        public async Task Add(Recipe recipe) => await _dbContext.Recipes.AddAsync(recipe);

        public async Task Delete(long recipeId)
        {
            var recipe = await _dbContext.Recipes.FindAsync(recipeId);

            _dbContext.Recipes.Remove(recipe!);
        }

        public async Task<IList<Recipe>> Filter(User user, FilterRecipesDto filters)
        {
           var query = _dbContext.Recipes
                .AsNoTracking()
                .Include(r => r.Ingredients)
                .Where(r => r.Active && r.UserId == user.Id);

            if (!string.IsNullOrEmpty(filters.RecipeTitle_Ingredient))
            {
                query = query.Where(r => r.Title.Contains(filters.RecipeTitle_Ingredient) ||
                                         r.Ingredients.Any(i => i.Item.Contains(filters.RecipeTitle_Ingredient)));
            }
            if (filters.CookingTimes.Any())
            { 
                query = query.Where(r => r.CookingTime.HasValue && filters.CookingTimes.Contains(r.CookingTime.Value));
            }
            if (filters.Difficulties.Any())
            {
                query = query.Where(r => r.Difficulty.HasValue && filters.Difficulties.Contains(r.Difficulty.Value));
            }
            if (filters.DishTypes.Any())
            {
                query = query.Where(r => r.DishTypes.Any(dishType => filters.DishTypes.Contains(dishType.Type)));
            }
            return await query.ToListAsync();
        }

        async Task<Recipe?> IRecipeReadOnlyRepository.GetById(User user, long recipeId)
        {
            return await GetFullRecipe()
                .AsNoTracking()
                .FirstOrDefaultAsync(recipe => recipe.Active && recipe.Id == recipeId && recipe.UserId == user.Id);
        }

        async Task<Recipe?> IRecipeUpdateOnlyRepository.GetById(User user, long recipeId)
        {
            return await GetFullRecipe()
                .FirstOrDefaultAsync(recipe => recipe.Active && recipe.Id == recipeId && recipe.UserId == user.Id);
        }

        public void Update(Recipe recipe) => _dbContext.Recipes.Update(recipe);

        public async Task<IList<Recipe>> GetForDashboard(User user)
        {
            return await _dbContext
                .Recipes
                .AsNoTracking()
                .Include(recipe => recipe.Ingredients)
                .Where(recipe => recipe.Active && recipe.UserId == user.Id)
                .OrderByDescending(r => r.CreatedOn)
                .Take(5)
                .ToListAsync();
        }

        private IIncludableQueryable<Recipe, IList<DishType>> GetFullRecipe()
        {
            return _dbContext
                .Recipes
                .Include(recipe => recipe.Ingredients)
                .Include(recipe => recipe.Instructions)
                .Include(recipe => recipe.DishTypes);
        }
    }

}
