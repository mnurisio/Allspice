<script setup>
import { AppState } from '@/AppState';
import { Recipe } from '@/models/Recipe';
import { computed } from 'vue';
import RecipeDetails from './RecipeDetails.vue';



defineProps({
    recipe: { type: Recipe, required: true }
}) 
</script>


<template>
    <div class="modal fade" id="recipeModal" tabindex="-1" aria-labelledby="recipeModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <RecipeDetails />
            </div>
        </div>
    </div>
</template>


<style lang="scss" scoped></style>