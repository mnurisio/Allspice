


namespace allspice.Services;

public class RecipeService
{

    public RecipeService(RecipeRepository repository)
    {
        _repository = repository;
    }
    private readonly RecipeRepository _repository;

    internal Recipe CreateRecipe(Recipe recipeData)
    {
        Recipe recipe = _repository.CreateRecipe(recipeData);
        return  recipe;
    }

    internal List<Recipe> GetAllRecipes()
    {
        List<Recipe> recipe = _repository.GetAllRecipes();
        return recipe;
    }

    internal Recipe GetRecipeById(int recipeId)
    {
        Recipe recipe = _repository.GetRecipeById(recipeId);

        if(recipe == null) throw new Exception($"invalid recipe ID of {recipeId}");

        return recipe;
    }

    internal Recipe UpdateRecipe(int recipeId, string userId, Recipe recipeUpdateData)
    {
        Recipe recipe = GetRecipeById(recipeId);

        if(recipe.CreatorId != userId) throw new Exception("THIS IS NOT YOUR RECIPE, PAL");

        recipe.Title = recipeUpdateData.Title ?? recipe.Title;
        recipe.Instructions = recipeUpdateData.Instructions ?? recipe.Instructions;

        _repository.UpdateRecipe(recipe);
        return recipe;
    }

    internal string DeleteRecipe(int recipeId, string userId)
    {
        Recipe recipe = GetRecipeById(recipeId);
        if(recipe.CreatorId != userId) throw new Exception("THIS IS NOT YOUR RECIPE, PAL");

        _repository.DeleteRecipe(recipeId);

        return "Fire recipe has been deleted forever";

    }
}