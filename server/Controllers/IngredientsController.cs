namespace allspice.Controllers;


[ApiController]
[Route("api/ingredients")]

public class IngredientsController : ControllerBase{

    public IngredientsController(IngredientsService ingredientsService, Auth0Provider auth0Provider)
    {
        _ingredientsService = ingredientsService;
        _auth0Provider = auth0Provider;
    }

    private readonly IngredientsService _ingredientsService;
    private readonly Auth0Provider _auth0Provider;




    [Authorize]
    [HttpPost]
    public async Task<ActionResult<Ingredient>> CreateIngredient([FromBody] Ingredient ingredientData)
    {
        try
        {
            Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
            
            Ingredient ingredient = _ingredientsService.CreateIngredient(ingredientData, userInfo.Id);
            return Ok(ingredient);
        }
        catch (Exception error)
        {

            return BadRequest(error.Message);
        }
    }

    [Authorize]
    [HttpDelete("{ingredientId}")]
    public async Task<ActionResult<Ingredient>> DeleteIngredient(int ingredientId){
        try
        {
            Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
            string ingredient = _ingredientsService.DeleteIngredient(ingredientId, userInfo.Id);
            return Ok(ingredient);
            
        }
        catch (Exception error)
        {

            return BadRequest(error.Message);
        }
    }
}