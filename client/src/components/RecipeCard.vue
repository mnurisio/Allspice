<script setup>
import { AppState } from '@/AppState';
import { Recipe } from '@/models/Recipe';
import { ingredientService } from '@/services/IngredientsService';
import { recipeService } from '@/services/RecipeService';
import Pop from '@/utils/Pop';
import { computed, ref } from 'vue';




const props = defineProps({
    recipe: { type: Recipe, required: true }
})


async function setActiveRecipe() {
    recipeService.setActiveRecipe(props.recipe)
    await ingredientService.getIngredientsByRecipeId(props.recipe.id)
}



</script>


<template>
    <div @click="setActiveRecipe()" role="button" data-bs-toggle="modal" data-bs-target="#recipeModal">
        <div class="recipeCard card my-md-4 my-2 sahitya-regular fs-4" :style="{ backgroundImage: `url(${recipe.img})` }">
            <div class="card-body text-light container">
                <div class="row justify-content-between">
                    <div class="col-md-7 recipe-title">
                            {{ recipe.title }}
                    </div>
                    <div class="col-md-3">
                            {{ recipe.category }}
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>


<style lang="scss" scoped>
.recipeCard {
    background-size: cover;
    background-position: center;
    min-height: 20dvh;
    box-shadow: 4px 8px 19px -3px rgba(0, 0, 0, 0.27);
}

.card-body{
    padding-left: 12px;
    padding-right: 12px;
    padding-bottom: 0;
    display: flex;
    flex-direction: column;
    justify-content: end;
    background: linear-gradient(to bottom, rgb(0, 0, 0, 0.1), rgb(0, 0, 0, 0.7));
    overflow: hidden;
}

.recipe-title{
    overflow: hidden;
    text-overflow: ellipsis;
    white-space: nowrap;
}
</style>