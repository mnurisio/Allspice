namespace allspice.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{
  private readonly AccountService _accountService;
  private readonly Auth0Provider _auth0Provider;
  private readonly FavoritesService _favoriteService;

    public AccountController(AccountService accountService, Auth0Provider auth0Provider, FavoritesService favoriteService)
    {
        _accountService = accountService;
        _auth0Provider = auth0Provider;
        _favoriteService = favoriteService;
    }

    [HttpGet]
  public async Task<ActionResult<Account>> Get()
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      return Ok(_accountService.GetOrCreateAccount(userInfo));
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }


  [HttpGet("favorites")]
  public async Task<ActionResult<List<FavoriteRecipe>>> GetAccountFavoriteRecipes(){
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      List<FavoriteRecipe> favoriteRecipes = _favoriteService.GetAccountFavoriteRecipes(userInfo.Id);
      return Ok(favoriteRecipes);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }
}
