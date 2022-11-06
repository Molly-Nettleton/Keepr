<template>
  <nav class="navbar navbar-expand-lg navbar-white bg-white px-3 d-flex justify-content-between elevation-3">
    <div class="d-flex">
      <router-link class="" :to="{ name: 'Home' }">
        <button class="button btn border border-3 me-3 hover">Home</button>
      </router-link>

      <div class="dropdown">
        <button class="button btn border border-3 dropdown-toggle" type="button" id="dropdownMenuButton1"
          data-bs-toggle="dropdown" aria-expanded="false">
          Create
        </button>

        <ul class="dropdown-menu" aria-labelledby="dropdownMenuButton1">
          
          <li class="dropdown-item border-bottom" data-bs-target="#createKeepModal" data-bs-toggle="modal">New Keep</li>

          <li><a class="dropdown-item" data-bs-target="#createVaultModal" data-bs-toggle="modal" href="#">New Vault</a>
          </li>
        </ul>
      </div>
    </div>

    <router-link class="navbar-brand d-flex pe-5 me-5" :to="{ name: 'Home' }">
      <div class="d-flex flex-column align-items-center">
        <img alt="logo" src="src\assets\img\Keepr logo.png" class="bg-white" height="45" />
      </div>
    </router-link>
    <!-- LOGIN COMPONENT HERE -->
    <Login class="hover" />

  </nav>

</template>

<script>
import Login from './Login.vue'
export default {
  setup() {
    return {}
  },
  components: { Login }
}
</script>

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

@media screen and (min-width: 768px) {
  nav {
    height: 64px;
  }
}
</style>
