<script setup>
import { AppState } from '@/AppState';
import { Recipe } from '@/models/Recipe';
import { favoritesService } from '@/services/FavoritesService';
import { ingredientService } from '@/services/IngredientsService';
import { recipeService } from '@/services/RecipeService';
import Pop from '@/utils/Pop';
import { computed, ref } from 'vue';



const account = computed(() => AppState.account)
const favoritedData = computed(() => AppState.favorites.find(favorite => favorite.id == props.recipe.id))

const editableFavoriteData = ref({
    recipeId: '',
    accountId: ''
})

const props = defineProps({
    recipe: { type: Recipe, required: true }
})



async function setActiveRecipe() {
    recipeService.setActiveRecipe(props.recipe)
    await ingredientService.getIngredientsByRecipeId(props.recipe.id)
}

async function deleteFavorite(favoriteId) {
    try {
        await favoritesService.deleteFavorite(favoriteId)
    }
    catch (error) {
        Pop.error(error);
    }
}

async function createFavorite() {
    try {
        
        editableFavoriteData.value.recipeId = props.recipe.id
        editableFavoriteData.value.accountId = account.value.id
        await favoritesService.createFavorite(editableFavoriteData.value)
    }
    catch (error) {
        Pop.error(error);
    }
}

function handleFavoriteClick(event) {
    event.stopPropagation(); // Force stop event bubbling
    event.preventDefault(); // Prevent any unintended default behavior
}

</script>


<template>

    <div class="main-card">
        <div class="recipeCard card my-md-4 my-2 sahitya-regular fs-4" :style="{ backgroundImage: `url(${recipe.img})` }">
            <div @click="handleFavoriteClick" class="favorite-button text-light">
                <span v-if="favoritedData" @click="deleteFavorite(favoritedData.favoriteId)" role="button" class="text-danger">
                    <i class="mdi mdi-heart"></i>
                </span>
                <span v-else @click="createFavorite()" role="button">
                    <i class="mdi mdi-heart-outline"></i>
                </span>
            </div>
            <div @click="setActiveRecipe()" role="button" data-bs-toggle="modal" data-bs-target="#recipeModal" class="card-body text-light container">
                <div class="row justify-content-between">
                    <div class="col-md-7 recipe-title">
                        {{ recipe.title }}
                    </div>
                    <div class="col-md-3 recipe-title text-md-center">
                        {{ recipe.category }}
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>


<style lang="scss" scoped>
.favorite-button {
    position: absolute;
    right: 5px;
    text-shadow: 1px 1px 1px black;
}

.recipeCard {
    background-size: cover;
    background-position: center;
    min-height: 20dvh;
    box-shadow: 4px 8px 19px -3px rgba(0, 0, 0, 0.27);
}

.card-body {
    padding-left: 12px;
    padding-right: 12px;
    padding-bottom: 0;
    display: flex;
    flex-direction: column;
    justify-content: end;
    background: linear-gradient(to bottom, rgb(0, 0, 0, 0.1), rgb(0, 0, 0, 0.7));
    border-bottom-right-radius: 0.25rem;
    border-bottom-left-radius: 0.25rem;
    overflow: hidden;
}

.recipe-title {
    overflow: hidden;
    text-overflow: ellipsis;
    white-space: nowrap;
    text-shadow: 1px 1px 1px black;
}
</style>