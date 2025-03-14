import { logger } from "@/utils/Logger.js"
import { api } from "./AxiosService.js"
import { Recipe } from "@/models/Recipe.js"
import { AppState } from "@/AppState.js"



class RecipeService {

    getMyRecipes(accountId) {
        const recipes = AppState.recipe.filter(recipe => recipe.creatorId == accountId)
        logger.log('getting my recipes')
        AppState.recipe = recipes
    }

    getMyFavoriteRecipes(favoriteRecipes) {
        const recipes = AppState.recipe.filter(r => r.id == favoriteRecipes.recipeId)
        logger.log('getting favorite recipes')
        AppState.recipe = recipes
    }


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
        AppState.recipe = []
        const response = await api.get('api/recipes')
        logger.log('got all recipes', response.data)
        const recipes = response.data.map(recipePOJO => new Recipe(recipePOJO))
        AppState.recipe = recipes
    }

    async updateInstructions(recipeData, recipeId) {
        const response = await api.put(`api/recipes/${recipeId}`, recipeData)
        logger.log(`updating instructions`, response.data)
        const recipe = new Recipe(response.data)
        AppState.activeRecipe = recipe
        AppState.recipe.splice(AppState.recipe.findIndex(r => r.id == recipeId), 1, recipe)
    }

}

export const recipeService = new RecipeService()