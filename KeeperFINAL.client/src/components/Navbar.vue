<template>
  <nav class="navbar navbar-expand-lg navbar-white bg-white px-3 d-flex justify-content-between elevation-3 pt-0">
    <div class="d-flex ">

      <router-link class="home" :to="{ name: 'Home' }">
        <button class="button btn border border-3 me-3 hover text-dark order-md-0 home p-3" title="Home"
          aria-label="Home Button">Home</button>
      </router-link>

      <span v-if="user.isAuthenticated">
        <div class="dropdown order-md-2 create ">
          <button class="button btn border border-3 dropdown-toggle hover p-3" type="button" id="dropdownMenuButton1"
            data-bs-toggle="dropdown" aria-expanded="false" title="Create" aria-label="Create Vault or Keep">
            Create
          </button>
          <ul class="dropdown-menu" aria-labelledby="Create Vault Or Keep Menu">
            <li class="dropdown-item border-bottom" data-bs-target="#createKeepModal" data-bs-toggle="modal"
              title="Create Keep" aria-label="Create Keep">New Keep</li>
            <li><a class="dropdown-item" data-bs-target="#createVaultModal" data-bs-toggle="modal" href="#"
                title="Create Vault" aria-label="Create Vault">New Vault</a>
            </li>
          </ul>
        </div>
      </span>
    </div>


    <div class="d-flex flex-column align-items-center me-5">
      <img alt="logo" src="src\assets\img\Keepr logo.png" class="bg-white hover" height="65" />
    </div>


    <Login class="hover order-md-1 order-3 login" />
  </nav>

</template>

<script>
import Login from './Login.vue'
import { computed } from 'vue'
import { AppState } from "../AppState.js"

export default {
  setup() {
    return {
      user: computed(() => AppState.user)
    }
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


.navbar {
  height: 90px;
}

@media screen AND (max-width: 768px) {


  .home {
    display: none;
  }

  .icon {
    margin-left: 6.5rem;
  }

  .navbar {
    height: 90px;
    position: bottom
  }
}
</style>
