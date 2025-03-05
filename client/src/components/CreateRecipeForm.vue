<script setup>
import { recipeService } from '@/services/RecipeService';
import Pop from '@/utils/Pop';
import { Modal } from 'bootstrap';
import { ref } from 'vue';

const categories = ['breakfast', 'lunch', 'dinner', 'snack', 'dessert']

const editableRecipeData = ref({
    title: '',
    category: '',
    instructions: '',
    img: ''
})




async function createRecipe() {
    try {
        await recipeService.createRecipe(editableRecipeData.value)
        editableRecipeData.value = {
            title: '',
            category: '',
            instructions: '',
            img: ''
        }

        Modal.getInstance('#createRecipeModal').hide()
        Modal.getInstance('#recipeModal').show()
    }
    catch (error) {
        Pop.error(error);
    }
}
</script>


<template>
    <form @submit.prevent="createRecipe()" class="sahitya-regular">
        <div>
            <select v-model="editableRecipeData.category" class="form-select text-capitalize mb-3"
                aria-label="Recipe Category" required>
                <option selected value="" disabled>Choose a category</option>
                <option v-for="category in categories" :key="'option' + category" :value="category"
                    class="text-capitalize">{{ category }}</option>
            </select>
        </div>
        <div class="form-floating mb-3">
            <input v-model="editableRecipeData.title" type="text" class="form-control" id="title" placeholder="Title..."
                required minlength="3" maxlength="30">
            <label for="title">Title</label>
        </div>
        <div class="form-floating mb-3">
            <textarea v-model="editableRecipeData.instructions" type="textarea" class="form-control" id="instructions" placeholder="Instructions..."
                required rows="5" minlength="5" maxlength="1000"></textarea>
            <label for="instructions">Instructions</label>
        </div>
        <div class="form-floating mb-3">
            <input v-model="editableRecipeData.img" type="url" class="form-control" id="img" placeholder="Image Url..."
                required maxlength="1000">
            <label for="img">Image Url</label>
        </div>
        <div class="text-end">
            <button role="button" type="submit" class="form-submit sahitya-bold p-1">Submit</button>
        </div>
    </form>
</template>


<style lang="scss" scoped>


.form-submit{
    cursor: pointer;
    background-color: #527360;
    border-radius: 0.5rem;
    color: white;
}

</style>
