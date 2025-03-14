<script setup>
import { AppState } from '@/AppState';
import CreateRecipeForm from '@/components/CreateRecipeForm.vue';
import ModalWrapper from '@/components/ModalWrapper.vue';
import RecipeCard from '@/components/RecipeCard.vue';
import { Recipe } from '@/models/Recipe';
import { favoritesService } from '@/services/FavoritesService';
import { recipeService } from '@/services/RecipeService';
import { logger } from '@/utils/Logger';
import Pop from '@/utils/Pop';
import { computed, onMounted, ref, watch } from 'vue';


// const recipes = computed(() => {
//   if(currentDisplay.value == "All"){
//     return AppState.recipe
//   }
//   if(currentDisplay.value == "favorites"){
//     return AppState.favoriteRecipes
//   }
// })

const recipes = computed(() => AppState.recipe)
const favoritedData = computed(() => AppState.favorites.filter(favorite => favorite.id == props.recipe.id))
const account = computed(() => AppState.account)

const props = defineProps({
  recipe: { type: Recipe, required: true }
})

const currentDisplay = ref("All")

onMounted(() => {
  getAllRecipes()
})

watch(account, getAccountFavorites)

async function getAllRecipes() {
  try {
    await recipeService.getAllRecipes()
  }
  catch (error) {
    logger.error(error);
  }
}

async function getAccountFavorites() {
  try {
    await favoritesService.getAccountFavorites()
  }
  catch (error) {
    Pop.error(error);
  }
}

function getMyRecipes(){
  try {
    const accountId = account.value.id
    recipeService.getMyRecipes(accountId)
  }
  catch (error){
    Pop.error(error);
  }
}

// async function getMyFavoriteRecipes(){
//   try {
//     const favoriteRecipes = favoritedData
//     await favoritesService.getMyFavoriteRecipes(favoriteRecipes)
    
//   }
//   catch (error){
//     Pop.error(error);
//   }
// }

</script>

<template>
  <div class="container">
    <div class="row bg-hero justify-content-center p-md-5">
      <div class="col-3 text-light text-center m-md-5 sahitya-bold titleSlogan">
        <p class="fs-1 m-2 titleSlogan">All-Spice</p>
        <p class="fs-4 m-1 titleSlogan">Cherish Your Family</p>
        <p class="fs-4 m-1 titleSlogan">And Their Cooking</p>
      </div>
      <div v-if="account" class="row justify-content-center mainMenu">
        <div class="col-md-5 card mb-5">
          <div class="row text-center text-success p-3 sahitya-regular fs-4 Shadow">
            <div @click="getAllRecipes()" class="col-md-4 align-self-center"><span class="homeLinks" role="button">All
                Recipes</span></div>
            <div @click="getMyRecipes()" class="col-md-4 align-self-center"><span class="homeLinks" role="button">My
                Recipes</span></div>
            <!-- <div @click="getMyFavoriteRecipes()" class="col-md-3 align-self-center"><span class="homeLinks"
                role="button">Favorites</span></div> -->
            <div class="col-md-4 align-self-center">
              <span class="homeLinks" role="button" data-bs-toggle="modal" data-bs-target="#createRecipeModal">Create
                Recipe</span>
            </div>
          </div>
        </div>
      </div>
      <div v-else class="row justify-content-center mainMenu">
        <div class="col-md-5 card mb-5">
          <div class="row text-center text-success p-3 sahitya-regular fs-4 Shadow">
            <div class="col-12">
              Please login to create recipes
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
  <div class="container-fluid mt-md-5 ">
    <div class="row">
      <div v-for="recipe in recipes" :key="recipe.id" class="col-md-4">
        <RecipeCard :recipe="recipe" />
      </div>
    </div>
  </div>
  <ModalWrapper modalId="createRecipeModal" modalTitle="Create Recipe">
    <CreateRecipeForm />
  </ModalWrapper>
</template>

<style scoped lang="scss">
@media(min-width: 768px) {
  .bg-hero {
    background-image: url('https://images.unsplash.com/photo-1604740795024-c06eeca4bf89?w=900&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MzN8fHJ1c3RpYyUyMGNvb2tpbmd8ZW58MHx8MHx8fDI%3D');
    background-size: cover;
    background-position: center;
    margin: 1% 0.5%;
    box-shadow: 4px 8px 19px -3px rgba(0, 0, 0, 0.27);
    border-radius: 1rem;
  }

  .mainMenu {
    position: relative;
    top: 125px
  }

  .titleSlogan {
    text-shadow: 1px 1px black;
    backdrop-filter: blur(5px);
    user-select: none;
  }
}

@media(max-width: 768px) {
  .titleSlogan {
    display: none;
  }
}


.homeLinks:hover {
  text-decoration: underline;
}

.Shadow {
  box-shadow: 4px 8px 19px -3px rgba(0, 0, 0, 0.27);
}
</style>
