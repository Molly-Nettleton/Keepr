<template>
  <nav class="navbar navbar-expand-lg navbar-white bg-white px-3 d-flex justify-content-between elevation-3">
    <div class="d-flex">

      <router-link class="home" :to="{ name: 'Home' }">
        <button class="button btn border border-3 me-3 hover text-dark order-md-0 home" title="Home"
          aria-label="Home Button">Home</button>
      </router-link>

      <div class="dropdown order-md-2 create">
        <button class="button btn border border-3 dropdown-toggle hover" type="button" id="dropdownMenuButton1"
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
    </div>

    <router-link class="navbar-brand d-flex pe-5 me-5 order-md-1 icon" :to="{ name: 'Home' }">
      <div class="d-flex flex-column align-items-center">
        <img alt="logo" src="src\assets\img\Keepr logo.png" class="bg-white hover" height="45" />
      </div>
    </router-link>

    <Login class="hover order-md-1 order-3 login" />

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

@media screen AND (max-width: 768px) {


  .home {
    display: none;
  }

  .icon {
    margin-left: 1.5rem;
  }

  .navbar {
    position: bottom;
  }

}
</style>
