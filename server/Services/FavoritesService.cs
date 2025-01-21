


namespace allspice.Services;

public class FavoritesService{


    public FavoritesService(FavoritesRepository repository)
    {
        _repository = repository;
    }
    private readonly FavoritesRepository _repository;

    internal FavoriteRecipe CreateFavorite(Favorite favoriteData)
    {
        FavoriteRecipe FavoriteRecipe = _repository.CreateFavorite(favoriteData);
        return FavoriteRecipe;
    }

    internal List<FavoriteRecipe> GetAccountFavoriteRecipes(string userId)
    {
        List<FavoriteRecipe> favoriteRecipes = _repository.GetAccountFavoriteRecipes(userId);
        return favoriteRecipes;
    }

    private Favorite GetFavoriteById(int favoriteId){
        Favorite favorite = _repository.GetFavoriteById(favoriteId);

        if(favorite == null) throw new Exception($"Invalid favorite ID of {favoriteId}");

        return favorite;
    }

    internal string DeleteFavorite(int favoriteId, string userId)
    {
        Favorite favorite = GetFavoriteById(favoriteId);

        if(favorite.AccountId != userId) throw new Exception("NOT YOUR FAVORITE TO DELETE, PAL");

        _repository.DeleteFavorite(favoriteId);

        return "Favorite has been deleted";
    }
}