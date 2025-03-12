



namespace allspice.Repositories;

public class RecipeRepository
{
    public RecipeRepository(IDbConnection db)
    {
        _db = db;
    }
    private readonly IDbConnection _db;

    internal Recipe CreateRecipe(Recipe recipeData)
    {
        string sql = @"
        INSERT INTO 
        recipes(title, instructions, img, category, creator_id)
        VALUES(@title, @instructions, @img, @category, @creatorId);

        SELECT
        recipes.*,
        profile_view.*
        FROM recipes
        JOIN profile_view ON recipes.creator_id = profile_view.id
        WHERE recipes.id = LAST_INSERT_ID();
        ";

        Recipe recipe = _db.Query(sql, (Recipe recipe, Profile account) =>
        {
            recipe.Creator = account;
            return recipe;
        }, recipeData).SingleOrDefault();

        return recipe;
    }

    internal List<Recipe> GetAllRecipes()
    {
        string sql = @"
        SELECT 
        recipes.*,
        profile_view.*
        FROM recipes
        JOIN profile_view ON recipes.creator_id = profile_view.id
        ;";

        List<Recipe> recipe = _db.Query(sql, (Recipe recipe, Profile account) =>
        {
            recipe.Creator = account;
            return recipe;
        }).ToList();
        return recipe;
    }

    internal Recipe GetRecipeById(int recipeId)
    {
        string sql = @"
        SELECT
        recipes.*,
        profile_view.*
        FROM recipes
        JOIN profile_view ON recipes.creator_id = profile_view.id
        WHERE recipes.id = @recipeId
        ;";

        Recipe recipe = _db.Query(sql, (Recipe recipe, Profile account) => 
        {
            recipe.Creator = account;
            return recipe;
        }, new {recipeId}).SingleOrDefault();

        return recipe;
    }

    internal void UpdateRecipe(Recipe recipeData)
    {
        string sql = @"
        UPDATE recipes
        SET
        title = @title,
        instructions = @instructions
        WHERE id = @Id LIMIT 1
        ;";

        int rowsAffected = _db.Execute(sql, recipeData);

        if(rowsAffected == 0) throw new Exception("UPDATE DID NOT WORK");
        if(rowsAffected > 1) throw new Exception("UPDATE WORKED TOO WELL");

    }

    internal void DeleteRecipe(int recipeId)
    {
        string sql = @"
        DELETE FROM recipes WHERE id = @recipeId LIMIT 1;
        ";

        int rowsAffected = _db.Execute(sql, new {recipeId});

        if(rowsAffected == 0) throw new Exception("DELETE DID NOT WORK");
        if(rowsAffected > 1) throw new Exception("DELETE WORKED TOO WELL");
    }
}