<script setup>
import { AppState } from '@/AppState';
import RecipeCard from '@/components/RecipeCard.vue';
import { recipeService } from '@/services/RecipeService';
import { logger } from '@/utils/Logger';
import Pop from '@/utils/Pop';
import { computed, onMounted, watch } from 'vue';
import { useRoute } from 'vue-router';


const recipes = computed(() => AppState.recipe)

const route = useRoute()

onMounted(() => {
  getAllRecipes()
})



async function getAllRecipes(){
  try {
    await recipeService.getAllRecipes()
  }
  catch (error){
    logger.error(error);
  }

}
</script>

<template>
  <!-- //NOTE "Top of page" -->
  <div class="container-fluid ">
    <div class="row bg-hero justify-content-center">
      <div class="col-3 text-light text-center">
        <h1>All-Spice</h1>
        <h6>Cherish Your Family</h6>
        <h6>And Their Cooking</h6>
      </div>
    </div>
  </div>
  <div class="container-fluid">
    <div class="row">
      <div v-for="recipe in recipes" :key="recipe.id" class="col-4">
        <RecipeCard :recipe="recipe" />
      </div>
    </div>
  </div>
</template>

<style scoped lang="scss">


.bg-hero{
  background-image: url('https://images.unsplash.com/photo-1497534547324-0ebb3f052e88?w=800&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8Nnx8cnVzdGljJTIwZm9vZHxlbnwwfHwwfHx8Mg%3D%3D');
  background-size: cover;
  background-position: center;

}
</style>
