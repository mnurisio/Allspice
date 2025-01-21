namespace allspice.Controllers;


[ApiController]
[Route("api/recipes")]
public class RecipeController : ControllerBase
{
    public RecipeController(RecipeService recipeService, Auth0Provider auth0Provider, IngredientsService ingredientsService)
    {
        _recipeService = recipeService;
        _auth0Provider = auth0Provider;
        _ingredientsService = ingredientsService;
    }
    private readonly RecipeService _recipeService;
    private readonly Auth0Provider _auth0Provider;
    private readonly IngredientsService _ingredientsService;


    [Authorize]
    [HttpPost]

    public async Task<ActionResult<Recipe>> CreateRecipe([FromBody] Recipe recipeData)
    {
        try
        {
            Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
            recipeData.CreatorId = userInfo.Id;
            Recipe recipe = _recipeService.CreateRecipe(recipeData);
            return Ok(recipe);
        }
        catch (Exception error)
        {

            return BadRequest(error.Message);
        }

    }


    [HttpGet]
    public ActionResult<List<Recipe>> GetAllRecipes()
    {
        try
        {
            List<Recipe> recipe = _recipeService.GetAllRecipes();
            return Ok(recipe);
        }
        catch (Exception error)
        {

            return BadRequest(error.Message);
        }
    }


    [HttpGet("{recipeID}")]
    public ActionResult<Recipe> GetRecipeById(int recipeId)
    {
        try
        {
            Recipe recipe = _recipeService.GetRecipeById(recipeId);
            return Ok(recipe);
        }
        catch (Exception error)
        {

            return BadRequest(error.Message);
        }
    }


    [Authorize]
    [HttpPut("{recipeId}")]
    public async Task<ActionResult<Recipe>> UpdateRecipe(int recipeId, [FromBody] Recipe recipeUpdateData)
    {
        try
        {
            Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
            recipeUpdateData.CreatorId = userInfo.Id;
            Recipe recipe = _recipeService.UpdateRecipe(recipeId, userInfo.Id, recipeUpdateData);
            return Ok(recipe);
        }
        catch (Exception error)
        {

            return BadRequest(error.Message);
        }
    }

    [Authorize]
    [HttpDelete("{recipeId}")]
    public async Task<ActionResult<Recipe>> DeleteRecipe(int recipeId){
        try
        {
            Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
            string message = _recipeService.DeleteRecipe(recipeId, userInfo.Id);
            return Ok(message);
        }
        catch (Exception error)
        {

            return BadRequest(error.Message);
        }
    }


    [HttpGet("{recipeID}/ingredients")]
    public ActionResult<List<Ingredient>> GetIngredientsByRecipeId(int recipeID){
        try
        {
            List<Ingredient> ingredients = _ingredientsService.GetIngredientsByRecipeId(recipeID);
            return Ok(ingredients);
        }
        catch (Exception error)
        {

            return BadRequest(error.Message);
        }
    }

}