import { logger } from "@/utils/Logger.js"
import { api } from "./AxiosService.js"
import { Recipe } from "@/models/Recipe.js"
import { AppState } from "@/AppState.js"


class RecipeService {
    async deleteRecipe() {
        const recipe = AppState.activeRecipe
        const response = await api.delete(`api/recipes/${recipe.id}`)
        logger.log('deleting recipe', response.data)
        const recipeToDelete = AppState.recipe.indexOf(recipe)
        AppState.recipe.splice(recipeToDelete, 1)
        AppState.activeRecipe = null
    }
    async createRecipe(recipeData) {
        const response = await api.post('api/recipes', recipeData)
        logger.log('Created Recipe', response.data)
        const recipe = new Recipe(response.data)
        AppState.recipe.push(recipe)
        AppState.activeRecipe = recipe
    }
    setActiveRecipe(recipe) {
        AppState.activeRecipe = recipe
    }

    async getRecipeById(recipeId) {
        const response = await api.get(`api/recipes/${recipeId}`)
        logger.log("getting recipe by ID", response.data)
    }

    async getAllRecipes() {
        const response = await api.get('api/recipes')
        logger.log('got all recipes', response.data)
        const recipes = response.data.map(recipePOJO => new Recipe(recipePOJO))
        AppState.recipe = recipes
    }

}

export const recipeService = new RecipeService()