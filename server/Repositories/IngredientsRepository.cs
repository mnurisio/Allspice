



namespace allspice.Repositories;

public class IngredientsRepository
{

    public IngredientsRepository(IDbConnection db)
    {
        _db = db;
    }
    private readonly IDbConnection _db;

    internal Ingredient CreateIngredient(Ingredient ingredientData)
    {
        string sql = @"
        INSERT INTO
        ingredients(name, quantity, recipe_id)
        VALUES(@name, @quantity, @recipeId);

        SELECT ingredients.*, recipes.*
        FROM ingredients
        JOIN recipes ON ingredients.recipe_id = recipes.id
        WHERE
        ingredients.id = LAST_INSERT_ID();";

        Ingredient ingredient = _db.Query(sql, (Ingredient ingredient, Recipe recipe) =>
        {
            return ingredient;
        }, ingredientData).SingleOrDefault();
        return ingredient;
    }

    internal List<Ingredient> GetIngredientsByRecipeId(int recipeID)
    {
        string sql = @"
        SELECT
        ingredients.*,
        recipes.*
        FROM ingredients
        JOIN recipes ON recipes.id = ingredients.recipe_id
        WHERE recipe_id = @recipeID;";

        List<Ingredient> ingredients = _db.Query(sql, (Ingredient ingredient, Recipe recipe) => 
        {
            return ingredient;
        }, new {recipeID}).ToList();

        return ingredients;
    }

    internal Ingredient GetIngredientById(int ingredientId)
    {
        string sql = @"
        SELECT * FROM ingredients WHERE id = @ingredientId;";

        Ingredient ingredient = _db.Query<Ingredient>(sql, new {ingredientId}).SingleOrDefault();
        return ingredient;
    }

    internal void DeleteIngredient(int ingredientId)
    {
        string sql = @"DELETE FROM ingredients WHERE id = @ingredientId LIMIT 1;";

        int rowsAffected = _db.Execute(sql, new {ingredientId});

        if(rowsAffected == 0) throw new Exception("DELETE DID NOT WORK");
        if(rowsAffected > 1) throw new Exception("DELETE WORKED TOO WELL");
    }
}
