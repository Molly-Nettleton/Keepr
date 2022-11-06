<template>
  <header>
    <Navbar />
  </header>
  <main>

    <router-view />

  </main>

  <!-- <footer class="bg-dark text-light">
    Made with 💖 by CodeWorks
  </footer> -->

  <CreateVaultModal />
  <CreateKeepModal />
  <ModalComponent>
  </ModalComponent>
  <ActiveKeepModal :keep="activeKeep" />

</template>

<script>
import { computed } from 'vue'
import { AppState } from './AppState'
import CreateKeepModal from "./components/CreateKeepModal.vue"
import CreateVaultModal from "./components/CreateVaultModal.vue"
import Navbar from './components/Navbar.vue'
import ModalComponent from './components/ModalComponent.vue'
import ActiveKeepModal from "./components/ActiveKeepModal.vue"

export default {
  setup() {
    return {
      appState: computed(() => AppState),
      keep: computed(() => AppState.activeKeep),
      activeKeep: computed(() => AppState.activeKeep)
    }
  },
  components: { Navbar, CreateKeepModal, CreateVaultModal, ModalComponent, ActiveKeepModal }
}
</script>
<style lang="scss">
@import "./assets/scss/main.scss";

:root {
  --main-height: calc(100vh - 32px - 64px);
}


footer {
  display: grid;
  place-content: center;
  height: 32px;
}
</style>
