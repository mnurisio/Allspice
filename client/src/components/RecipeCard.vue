<script setup>
import { Recipe } from '@/models/Recipe';
import { ingredientService } from '@/services/IngredientsService';
import { recipeService } from '@/services/RecipeService';



const props = defineProps({
    recipe: {type: Recipe, required: true}
}) 


async function setActiveRecipe(){
    recipeService.setActiveRecipe(props.recipe)
    await ingredientService.getIngredientsByRecipeId(props.recipe.id)
}



</script>


<template>
    <div @click="setActiveRecipe()" role="button" data-bs-toggle="modal" data-bs-target="#recipeModal">
        <div class="recipeCard card my-md-4" :style="{backgroundImage: `url(${recipe.img})`}">
            <div class="card-body text-light align-items-between">
                <div class=" row justify-content-between">
                    <div class="col-md-4">
                        <span class="bg-secondary py-md-1 px-md-2 rounded rounded-pill">{{ recipe.category }}</span>
                    </div>
                    <div class="col-md-2">
                        <span class="bg-secondary py-md-1 px-md-2 rounded rounded-pill">❣️</span>
                    </div>
                </div>
                <div class="row px-md-2 pt-md-2">
                    <div class="bg-secondary">
                        {{ recipe.title }}
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>


<style lang="scss" scoped>
.recipeCard{
    background-size: cover;
    background-position: center;
    min-height: 20dvh;
    box-shadow: 4px 8px 19px -3px rgba(0, 0, 0, 0.27);
}
</style>