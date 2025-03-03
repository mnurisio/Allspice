<script setup>
import { AppState } from '@/AppState';
import { recipeService } from '@/services/RecipeService';
import Pop from '@/utils/Pop';
import { Modal } from 'bootstrap';
import { computed, ref } from 'vue';
import { ingredientService } from '@/services/IngredientsService';




const recipe = computed(() => AppState.activeRecipe)
const ingredients = computed(() => AppState.ingredients)
const account = computed(() => AppState.account)

const editMode = ref(false)



const editableIngredientData = ref({
    quantity: '',
    name: '',
    recipeId: '',
})

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

async function deleteIngredient(ingredientId) {
    try {
        const confirm = await Pop.confirm("are you sure you want to delete this ingredient?")
        if (!confirm) return
        await ingredientService.deleteIngredient(ingredientId)
    }
    catch (error) {
        Pop.error(error);
    }
}


</script>


<template>
    <div class="container-fluid modal fade " id="recipeModal" tabindex="-1" aria-labelledby="recipeModalLabel">
        <div class="modal-dialog modal-lg">
            <div v-if="recipe" class="modal-content container">
                <div class="row">
                    <div class="col-md-6 px-0">
                        <img class="img-fluid w-100 h-100" :src="recipe.img" :alt="recipe.title">
                    </div>
                    <div class="col-md-6 pe-md-0">
                        <div class="text-end" @click="editMode = false" role="button" data-bs-dismiss="modal">
                            <i class="mdi mdi-close-thick fs-5"></i>
                        </div>
                        <div class="row justify-content-between w-100">
                            <div class="recipe-title col-md-8 text-success sahitya-regular pe-0">
                                <h3 class="m-0">{{ recipe.title }}</h3>
                            </div>
                            <div v-if="recipe.creatorId == account?.id" class="col-2 align-self-center px-0 text-center">
                                <div type="button" data-bs-toggle="dropdown" aria-expanded="false">
                                    <i class="mdi mdi-dots-horizontal fs-4"></i>
                                </div>
                                <ul class="dropdown-menu">
                                    <li><button @click="editMode = true" :hidden="editMode == true"
                                            class="btn btn-primary rounded rounded-pill ms-2">Edit</button></li>
                                    <li><button @click="editMode = false" :hidden="editMode == false"
                                            class="btn btn-primary rounded rounded-pill ms-2">Cancel</button></li>
                                    <li><button @click="deleteRecipe()"
                                            class="btn btn-danger ms-2 rounded rounded-pill">Delete</button></li>
                                </ul>
                            </div>
                        </div>
                        <div>
                            by: {{ recipe.creator.name }}
                        </div>
                        <div class="row my-2">
                            <div class="col-md-2">
                                <span class="px-2 py-1 rounded rounded-pill bg-secondary text-light">
                                    {{ recipe.category }}
                                </span>
                            </div>
                        </div>
                        <div class="mb-1">
                            <h5>Ingredients</h5>
                        </div>
                        <div v-for="ingredient in ingredients" :key="ingredient.id">
                            <div class="mb-1">
                                <span v-if="editMode == true">
                                    <i class="mdi mdi-delete-outline text-danger" role="button"
                                        @click="deleteIngredient(ingredient.id)"></i> {{ ingredient.quantity }} {{
                                            ingredient.name }}
                                </span>
                                <span v-else>
                                    {{ ingredient.quantity }} {{ ingredient.name }}
                                </span>
                            </div>
                        </div>
                        <form class="row" v-if="editMode == true" @submit.prevent="createIngredient()">
                            <div class="form-floating mb-3 col-4 px-1">
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

.recipe-title{
    overflow: hidden;
    text-overflow: ellipsis;
    white-space: nowrap;
}

.img {
    object-fit: cover;
    object-position: center;
}

.categoryName {
    background-color: #ffffffa4;
}

.modal-content {
    overflow: hidden;
}
</style>