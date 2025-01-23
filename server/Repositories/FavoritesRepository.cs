


namespace allspice.Repositories;

public class FavoritesRepository
{


    public FavoritesRepository(IDbConnection db)
    {
        _db = db;
    }

    private readonly IDbConnection _db;

    internal FavoriteRecipe CreateFavorite(Favorite favoriteData)
    {
        string sql = @"
        INSERT INTO
        favorites(recipe_id, account_id)
        VALUES(@recipeId, @accountId);

        SELECT
        favorites.*,
        accounts.*,
        recipes.*
        FROM favorites
        JOIN accounts ON favorites.account_id = accounts.id
        JOIN recipes ON favorites.recipe_id = recipes.id
        WHERE favorites.id = LAST_INSERT_ID();";

        FavoriteRecipe FavoriteRecipe = _db.Query(sql, (Favorite favorite, FavoriteProfile account, FavoriteRecipe recipe) =>
        {
            recipe.AccountId = account.Id;
            recipe.Creator = account;
            recipe.FavoriteId = favorite.Id;
            return recipe;
        }, favoriteData).SingleOrDefault();

        return FavoriteRecipe;
    }

    internal List<FavoriteRecipe> GetAccountFavoriteRecipes(string userId)
    {
        string sql = @"
        SELECT
        favorites.*,
        recipes.*,
        accounts.*
        FROM favorites
        JOIN recipes ON favorites.recipe_id = recipes.id
        JOIN accounts ON accounts.id = recipes.creator_id
        WHERE favorites.account_id = @userId
        ;";

        List<FavoriteRecipe> favoriteRecipes = _db.Query(sql, (Favorite favorite, FavoriteRecipe recipe, Profile account) =>
        {
            recipe.AccountId = account.Id;
            recipe.FavoriteId = favorite.Id;
            recipe.Creator = account;
            return recipe;
        }, new { userId }).ToList();

        return favoriteRecipes;
    }

    internal Favorite GetFavoriteById(int favoriteId)
    {
        string sql = @"
        SELECT *
        FROM favorites where id = @favoriteId
        ;";

        Favorite favorite = _db.Query<Favorite>(sql, new {favoriteId}).SingleOrDefault();
        return favorite;
    }

    internal void DeleteFavorite(int favoriteId)
    {
        string sql = @"DELETE FROM favorites WHERE id = @favoriteId;";

        int rowsAffected = _db.Execute(sql, new {favoriteId});

        if(rowsAffected == 0) throw new Exception("DELETE DID NOT WORK");
        if(rowsAffected > 1) throw new Exception("DELETE WORKED TOO WELL");
    }
}