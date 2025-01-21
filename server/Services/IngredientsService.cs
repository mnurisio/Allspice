

namespace allspice.Services;

public class IngredientsService{

    public IngredientsService(IngredientsRepository repository, RecipeService recipeService)
    {
        _repository = repository;
        _recipeService = recipeService;
    }
    private readonly IngredientsRepository _repository;
    private readonly RecipeService _recipeService;

    internal Ingredient CreateIngredient(Ingredient ingredientData, string Userid)
    {
        Recipe recipe = _recipeService.GetRecipeById(ingredientData.RecipeId);

        if(recipe == null) throw new Exception("THIS RECIPE DOES NOT EXIST");

        if(recipe.CreatorId != Userid) throw new Exception("CANT ADD INGREDIENTS TO A RECIPE THAT IS NOT YOURS FAM");

        Ingredient ingredient = _repository.CreateIngredient(ingredientData);

        return ingredient;

    }

    internal List<Ingredient> GetIngredientsByRecipeId(int recipeID)
    {
        List<Ingredient> ingredients = _repository.GetIngredientsByRecipeId(recipeID);
        return ingredients;
    }

    internal Ingredient GetIngredientById(int ingredientId){

        Ingredient ingredient = _repository.GetIngredientById(ingredientId);

        if(ingredient == null) throw new Exception($"Invalid Ingredient ID of {ingredientId}");

        return ingredient;
    }

    internal string DeleteIngredient(int ingredientId, string userId)
    {
        Ingredient ingredient = GetIngredientById(ingredientId);
        Recipe recipe = _recipeService.GetRecipeById(ingredient.RecipeId);


        if(recipe.CreatorId != userId) throw new Exception("THIS AIN'T YOUR INGREDIENT, PAL");

        _repository.DeleteIngredient(ingredientId);

        return "Fire ingredient has been removed";


    }
}