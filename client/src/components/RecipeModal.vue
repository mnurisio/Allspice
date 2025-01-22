<script setup>
import { AppState } from '@/AppState';
import { recipeService } from '@/services/RecipeService';
import Pop from '@/utils/Pop';
import { Modal } from 'bootstrap';
import { computed, ref } from 'vue';
import { ingredientService } from '@/services/IngredientsService';
import { favoritesService } from '@/services/FavoritesService';




const recipe = computed(() => AppState.activeRecipe)
const ingredients = computed(() => AppState.ingredients)
const account = computed(() => AppState.account)

const editMode = ref(false)


const editableIngredientData = ref({
    quantity: '',
    name: '',
    recipeId: '',
})

const editableFavoriteData = ref({
    recipeId: '',
    accountId: ''
})



async function createFavorite() {
    try {
        editableFavoriteData.value.recipeId = recipe.value.id
        editableFavoriteData.value.accountId = account.value.id
        await favoritesService.createFavorite(editableFavoriteData.value)
    }
    catch (error) {
        Pop.error(error);
    }
}


async function createIngredient() {
    try {
        editableIngredientData.value.recipeId = recipe.value.id
        await ingredientService.createIngredient(editableIngredientData.value)
        editableIngredientData.value = {
            quantity: '',
            name: '',
            recipeId: '',
        }
    }
    catch (error) {
        Pop.error(error);
    }
}

async function deleteRecipe() {
    try {
        const confirm = await Pop.confirm("are you sure you want to delete this recipe?")
        if (!confirm) return
        await recipeService.deleteRecipe()

        Modal.getInstance('#recipeModal').hide()
    }
    catch (error) {
        Pop.error(error);
    }
}

async function deleteIngredient(ingredientId){
    try {
        const confirm = await Pop.confirm("are you sure you want to delete this ingredient?")
        if (!confirm) return
        await ingredientService.deleteIngredient(ingredientId)
    }
    catch (error){
    Pop.error(error);
    }
}


</script>


<template>
    <div class="container-fluid modal fade " id="recipeModal" tabindex="-1" aria-labelledby="recipeModalLabel">
        <div class="modal-dialog modal-lg">
            <div v-if="recipe" class="modal-content">
                <div class="row">
                    <div class="col-md-6">
                        <img class="img-fluid w-100 h-100" :src="recipe.img" :alt="recipe.title">
                    </div>
                    <div class="col-6">
                        <div class="row">
                            <span @click="editMode = false" role="button" data-bs-dismiss="modal" class="text-end">
                                <i class="mdi mdi-close-thick p-2"></i>
                            </span>
                        </div>
                        <div class="fs-3 text-success">
                                <p class="m-0">{{ recipe.title }}</p>
                                <div v-if="recipe.creatorId == account?.id" class="dropdown">
                                    <button class="btn btn-secondary dropdown-toggle" type="button" data-bs-toggle="dropdown" aria-expanded="false">
                                    Options
                                    </button>
                                    <ul class="dropdown-menu">
                                        <li><button @click="editMode = true" :hidden="editMode == true" class="btn btn-primary rounded rounded-pill ms-2">Edit</button></li>
                                        <li><button @click="editMode = false" :hidden="editMode == false" class="btn btn-primary rounded rounded-pill ms-2">Cancel</button></li>
                                        <li><button @click="deleteRecipe()" class="btn btn-danger ms-2 rounded rounded-pill">Delete</button></li>
                                    </ul>
                                </div>      
                        </div>
                        <div>
                            by: {{ recipe.creator.name }}
                        </div>
                            <div class="row my-2">
                                <div class="col-md-2 text-center">
                                    <span @click="createFavorite()" role="button" class=" px-2 py-1 bg-secondary rounded rounded-pill">❣️</span>
                                </div>
                                <div class="col-md-2">
                                    <span class="px-2 py-1 rounded rounded-pill bg-secondary text-light">
                                        {{ recipe.category }}
                                    </span>
                                </div>
                            </div>
                        <div class="mb-4">
                            <h5>Ingredients</h5>
                        </div>
                        <div v-for="ingredient in ingredients" :key="ingredient.id">
                            <div class="mb-2">
                                <span v-if="editMode == true" role="button" @click="deleteIngredient(ingredient.id)"><i class="mdi mdi-delete-outline"></i> {{ ingredient.quantity }} {{ ingredient.name }}</span>
                            </div>
                        </div>
                        <form class="me-3 mt-4 row" v-if="editMode == true" @submit.prevent="createIngredient()">
                            <div class="form-floating mb-3 col-6 px-1">
                                <input v-model="editableIngredientData.quantity" type="text" class="form-control"
                                    id="quantity" placeholder="Quantity" required>
                                <label for="title">Quantity</label>
                            </div>
                            <div class="form-floating mb-3 col-6 px-1">
                                <input v-model="editableIngredientData.name" type="text" class="form-control" id="name"
                                    placeholder="Name..." required>
                                <label for="name">Name</label>
                            </div>
                            <div class="text-start mb-2">
                                <button type="submit" class="btn btn-primary">Submit</button>
                            </div>
                        </form>
                        <div class="mb-4">
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