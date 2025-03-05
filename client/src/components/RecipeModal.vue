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
                            <i class="mdi mdi-close-thick fs-5 pe-1"></i>
                        </div>
                        <div class="row justify-content-between w-100">
                            <div class="recipe-title col-8 text-success sahitya-regular pe-0">
                                <h3 class="m-0">{{ recipe.title }}</h3>
                            </div>
                            <div v-if="recipe.creatorId == account?.id"
                                class="col-1 align-self-center px-0 text-center sahitya-regular">
                                <div type="button" data-bs-toggle="dropdown" aria-expanded="false">
                                    <i class="mdi mdi-dots-horizontal fs-4"></i>
                                </div>
                                <ul class="dropdown-menu text-center">
                                    <li>
                                        <div @click="editMode = true" :hidden="editMode == true"
                                            class="ms-2 text-success">Edit</div>
                                    </li>
                                    <li>
                                        <div @click="editMode = false" :hidden="editMode == false"
                                            class="ms-2 text-success">Cancel</div>
                                    </li>
                                    <li>
                                        <div @click="deleteRecipe()" class="ms-2 text-danger">Delete</div>
                                    </li>
                                </ul>
                            </div>
                        </div>
                        <div class="mb-2">
                            <span class="sahitya-bold">
                                {{ recipe.category }} by:
                            </span>

                            <span class="sahitya-italic">
                                {{ recipe.creator.name }}
                            </span>
                        </div>
                        <div class="mb-1">
                            <h5 class="ingredient-title sahitya-bold">Ingredients</h5>
                        </div>
                        <div class="ingredient-list">
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
                        </div>
                        <form class="row mt-2" v-if="editMode == true" @submit.prevent="createIngredient()">
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
.recipe-title {
    overflow: hidden;
    text-overflow: ellipsis;
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

.dropdown-menu div {
    cursor: pointer;
    transition: 0.3s ease-in-out;
}

.dropdown-menu div:hover {
    transform: scale(1.05);
}

.ingredient-list {
    height: 10rem;
    overflow-y: scroll;
}

.ingredient-list::-webkit-scrollbar {
    width: 8px;
}


.ingredient-list::-webkit-scrollbar-track {
    background: #f1f1f1;
    border-radius: 4px;
}


.ingredient-list::-webkit-scrollbar-thumb {
    background: #219653;
    border-radius: 4px;
}


.ingredient-list::-webkit-scrollbar-thumb:hover {
    background: #176d73;
}
</style>