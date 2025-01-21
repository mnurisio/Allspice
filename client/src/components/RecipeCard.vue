<script setup>
import { Recipe } from '@/models/Recipe';

defineProps({
    recipe: {type: Recipe, required: true}
}) 
</script>


<template>
    <div role="button" data-bs-toggle="modal" data-bs-target="#recipeModal">
        <div class="recipeCard card my-4" :style="{backgroundImage: `url(${recipe.img})`}">
            <div class="card-body text-light align-items-between">
                <div class=" row justify-content-between">
                    <div class="col-4">
                        <span class="bg-secondary py-1 px-2 rounded rounded-pill">{{ recipe.category }}</span>
                    </div>
                    <div class="col-2">
                        <span class="bg-secondary py-1 px-2 rounded rounded-pill">❣️</span>
                    </div>
                </div>
                <div class="row px-2 pt-2">
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
}
</style>