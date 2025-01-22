<script setup>
import { Recipe } from '@/models/Recipe';
import { ingredientService } from '@/services/IngredientsService';
import { recipeService } from '@/services/RecipeService';
import Pop from '@/utils/Pop';
import { Modal } from 'bootstrap';
import { ref } from 'vue';

const categories = ['breakfast', 'lunch', 'dinner', 'snack', 'dessert']

const editableRecipeData = ref({
    title: '',
    category: '',
    img: ''
})




async function createRecipe() {
    try {
        await recipeService.createRecipe(editableRecipeData.value)
        editableRecipeData.value = {
            title: '',
            category: '',
            img: ''
        }
        Modal.getInstance('#recipeModal').show()
    }
    catch (error) {
        Pop.error(error);
    }
}
</script>


<template>
    <form @submit.prevent="createRecipe()">
        <div class="form-floating mb-3">
            <input v-model="editableRecipeData.title" type="text" class="form-control" id="title" placeholder="Title..."
                required>
            <label for="title">Title</label>
        </div>
        <div>
            <select v-model="editableRecipeData.category" class="form-select text-capitalize"
                aria-label="Recipe Category" required>
                <option selected value="" disabled>Choose a category</option>
                <option v-for="category in categories" :key="'option' + category" :value="category"
                    class="text-capitalize">{{ category }}</option>
            </select>
        </div>
        <div class="form-floating">
            <input v-model="editableRecipeData.img" type="url" class="form-control" id="img" placeholder="Image Url..."
                required>
            <label for="img">Image Url</label>
        </div>
        <div class="text-end">
            <button type="submit" class="btn btn-primary">Submit</button>
        </div>
    </form>
</template>


<style lang="scss" scoped>
.modal-header {
    background-color: #527360;
}
</style>
