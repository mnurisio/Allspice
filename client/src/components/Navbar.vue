<script setup>
import { onMounted, ref } from 'vue';
import { loadState, saveState } from '../utils/Store.js';
import Login from './Login.vue';

const theme = ref(loadState('theme') || 'light')

onMounted(() => {
  document.documentElement.setAttribute('data-bs-theme', theme.value)
})

function toggleTheme() {
  theme.value = theme.value == 'light' ? 'dark' : 'light'
  document.documentElement.setAttribute('data-bs-theme', theme.value)
  saveState('theme', theme.value)
}

</script>

<template>
  <nav class="navbar navbar-expand-sm d-flex justify-content-between px-3">
    <router-link class="navbar-brand" :to="{ name: 'Home' }">
      <div class="d-flex flex-column align-items-center sahitya-regular text-success fs-3 ms-1">
        All-Spice
      </div>
    </router-link>
    <div id="navbarText">
      <ul class="navbar-nav me-auto">
        <li>
          <Login />
        </li>
      </ul>
      <!-- LOGIN COMPONENT HERE -->
      
    </div>
  </nav>
</template>

<style scoped>
a:hover {
  text-decoration: none;
}

.nav-link {
  text-transform: uppercase;
}

.navbar-nav .router-link-exact-active {
  border-bottom: 2px solid var(--bs-success);
  border-bottom-left-radius: 0;
  border-bottom-right-radius: 0;
}

@media screen and (min-width: 576px) {
  nav {
    height: 64px;
  }
}
</style>
