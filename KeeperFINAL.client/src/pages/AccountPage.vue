<template>
  <UserPage />
</template>

<script>
import { computed } from 'vue'
import { AppState } from '../AppState'
import UserPage from "../components/UserPage.vue";
export default {
  setup() {
    return {
      account: computed(() => AppState.account)
    };
  },
  components: {}
}
</script>

<style scoped>
img {
  max-width: 100px;
}
</style>
