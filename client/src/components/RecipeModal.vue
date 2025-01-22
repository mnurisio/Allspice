<script setup>
import { AppState } from '@/AppState';
import { recipeService } from '@/services/RecipeService';
import Pop from '@/utils/Pop';
import { Modal } from 'bootstrap';
import { computed } from 'vue';
import { useRoute } from 'vue-router';




const recipe = computed(() => AppState.activeRecipe)
const ingredients = computed(() => AppState.ingredients)
const account = computed(()=> AppState.account)


async function deleteRecipe() {
    try {
        const confirm = await Pop.confirm("are you sure you want to delete this recipe?")
        if(!confirm) return
        await recipeService.deleteRecipe()

        Modal.getInstance('#recipeModal').hide()
    }
    catch (error) {
        Pop.error(error);
    }
}


</script>


<template>
    <div class="container-fluid modal fade " id="recipeModal" tabindex="-1" aria-labelledby="recipeModalLabel"
        aria-hidden="true">
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
                        <div>
                            <span class="fs-3 text-success">
                                {{ recipe.title }}
                            </span>
                            <span v-if="recipe.creatorId == account.id"><button @click="deleteRecipe()" class="btn btn-danger ms-2 rounded rounded-pill">Delete</button></span>
                        </div>
                        <div>
                            by: {{ recipe.creator.name }}
                            <div class="my-md-2">
                                <span class="rounded rounded-pill bg-secondary text-light py-md-1 px-md-2">{{
                                    recipe.category }}</span>
                            </div>
                        </div>
                        <div>
                            <h5>Ingredients</h5>
                        </div>
                        <div v-for="ingredient in ingredients" :key="ingredient.id">
                            <p>{{ ingredient.quantity }} {{ ingredient.name }}</p>
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

.categoryName {
    background-color: #ffffffa4;
}
</style>