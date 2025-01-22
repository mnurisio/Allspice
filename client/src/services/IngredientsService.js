import { AppState } from "@/AppState.js"
import { recipeService } from "./RecipeService.js"
import { api } from "./AxiosService.js"
import { logger } from "@/utils/Logger.js"
import { Ingredient } from "@/models/Ingredient.js"


class IngredientService {

    async deleteIngredient(ingredientId) {
        const response = await api.delete(`api/ingredients/${ingredientId}`)
        logger.log('deleting ingredient', response.data)
        const ingredientToDelete = AppState.ingredients.findIndex(ingredient => ingredient.id == ingredientId)
        AppState.ingredients.splice(ingredientToDelete, 1)

    }

    async createIngredient(ingredientData) {
        const response = await api.post('api/ingredients', ingredientData)
        logger.log('creating ingredient', response.data)
        const ingredient = new Ingredient(response.data)
        AppState.ingredients.push(ingredient)
    }
    async getIngredientsByRecipeId(recipeId) {
        const response = await api.get(`api/recipes/${recipeId}/ingredients`)
        logger.log("got ingredients", response.data)
        const ingredients = response.data.map(pojo => new Ingredient(pojo))
        AppState.ingredients = ingredients

    }

}

export const ingredientService = new IngredientService()