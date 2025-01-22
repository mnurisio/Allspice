<script setup>
import { AppState } from '@/AppState';
import { Recipe } from '@/models/Recipe';
import { computed } from 'vue';



const recipe = computed(() => AppState.activeRecipe)
const ingredients = computed(()=> AppState.ingredients)




</script>


<template>
    <div class="container-fluid modal fade " id="recipeModal" tabindex="-1" aria-labelledby="recipeModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-lg">
            <div v-if="recipe" class="modal-content">
                    <div class="row">
                        <div class="col-md-6">
                            <img class="img-fluid w-100 h-100" :src="recipe.img" :alt="recipe.title">
                        </div>
                        <div class="col-6">
                            <div class="row">
                                <span role="button" data-bs-dismiss="modal" class="text-end">
                                    <i class="mdi mdi-close-thick p-2"></i>
                                </span>
                            </div>
                            <div class="fs-3 text-success">
                                {{ recipe.title }}
                            </div>
                            <div>
                                by: {{ recipe.creator.name }}
                                <div class="my-md-2">
                                    <span class="rounded rounded-pill bg-secondary text-light py-md-1 px-md-2">{{ recipe.category }}</span>
                                </div>
                            </div>
                            <div>
                                <h5>Ingredients</h5>
                            </div>
                            <div v-for="ingredient in ingredients" :key="ingredient.id">
                                <p>{{ ingredient.quantity }}  {{ ingredient.name }}</p>
                            </div>
                            <div>
                                <h5>Instructions</h5>
                            </div>
                            <div>
                                <p>{{ recipe.instructions }}</p>
                            </div>
                        </div>
                    </div>
            </div>
        </div>
    </div>
</template>


<style lang="scss" scoped>
.img {
    object-fit: cover;
    object-position: center;
}

.categoryName{
    background-color: #ffffffa4;
}
</style>