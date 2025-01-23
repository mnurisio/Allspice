<script setup>
import { AppState } from '@/AppState';
import CreateRecipeForm from '@/components/CreateRecipeForm.vue';
import ModalWrapper from '@/components/ModalWrapper.vue';
import RecipeCard from '@/components/RecipeCard.vue';
import { favoritesService } from '@/services/FavoritesService';
import { recipeService } from '@/services/RecipeService';
import { logger } from '@/utils/Logger';
import Pop from '@/utils/Pop';
import { computed, onMounted, watch } from 'vue';


const recipes = computed(() => AppState.recipe)

const account = computed(() => AppState.account)


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

async function getAccountFavorites(){
  try {
    await favoritesService.getAccountFavorites()
  }
  catch (error){
    Pop.error(error);
  }
}

</script>

<template>
  <!-- //NOTE "Top of page" -->
  <div class="container-fluid">
    <div class="row bg-hero justify-content-center p-md-5">
      <div class="col-12 text-light text-center p-md-5 sahitya-regular titleSlogan">
        <p class="fs-1 m-2 titleSlogan">All-Spice</p>
        <p class="fs-4 m-1 titleSlogan">Cherish Your Family</p>
        <p class="fs-4 m-1 titleSlogan">And Their Cooking</p>
      </div>
      <div class="row justify-content-center mainMenu">
        <div class="col-md-5 card">
          <div v-if="account" class="row text-center text-success p-3 sahitya-regular fs-3 Shadow">
            <div class="col-md-3"><span class="homeLinks" role="button">Home</span></div>
            <div class="col-md-3"><span class="homeLinks" role="button">My Recipes</span></div>
            <div class="col-md-3"><span class="homeLinks" role="button">Favorites</span></div>
            <div class="col-md-3">
              <span class="homeLinks" role="button" data-bs-toggle="modal" data-bs-target="#createRecipeModal">Create Recipe</span>
            </div>
          </div>
          <div v-else class="row text-center text-success p-3 sahitya-regular fs-3 Shadow">
            <div class="col-md-4"><span class="homeLinks" role="button">Home</span></div>
            <div class="col-md-4"><span class="homeLinks" role="button">My Recipes</span></div>
            <div class="col-md-4"><span class="homeLinks" role="button">Favorites</span></div>
          </div>
        </div>
      </div>
    </div>
  </div>
  <div class="container-fluid mt-md-5">
    <div class="row">
      <div v-for="recipe in recipes" :key="recipe.id" class="col-md-4">
        <RecipeCard :recipe="recipe" />
      </div>
    </div>
  </div>
  <ModalWrapper modalId="createRecipeModal" modalTitle="Create Fire Recipe">
    <CreateRecipeForm />
  </ModalWrapper>
</template>

<style scoped lang="scss">
.bg-hero {
  background-image: url('https://images.unsplash.com/photo-1515823808611-65fd8e56c71a?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8NDR8fHJ1c3RpYyUyMGZvb2R8ZW58MHx8MHx8fDI%3D');
  background-size: cover;
  background-position: center;
  margin: 1% 0.5%;
  box-shadow: 4px 8px 19px -3px rgba(0, 0, 0, 0.27);
}

.mainMenu {
  position: relative;
  top: 80px;
}

.titleSlogan {
  text-shadow: 1px 1px black;
  backdrop-filter: grayscale(80%);
  user-select: none;
}

.homeLinks:hover {
  text-decoration: underline;
}

.Shadow {
  box-shadow: 4px 8px 19px -3px rgba(0, 0, 0, 0.27);
}
</style>
