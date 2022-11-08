<template>
  <router-link :to="{ name: 'Vault', params: { id: vault.id } }" class="text-white">
    <div class="card bg-dark text-white border border-0 elevation-3 hoverable m-3 hover vaultcard"
      :style="{ backgroundImage: `url(${vault?.img})` }" :title="vault.name" :aria-label="vault.name">
      <!-- <img :src="vault.img" class="card-img img-fluid" alt="..."> -->
      <div class="card-img-overlay d-flex align-items-end justify-content-between">
        <div class="fs-3 fontbg text-uppercase">
          {{ vault.name }}</div>
        <div v-if="vault.isPrivate"><i class="mdi mdi-lock fs-2" title="Private Vault"></i></div>
      </div>
    </div>
  </router-link>
</template>


<script>
import { Vault } from "../models/Vault.js";
import { vaultsService } from "../services/VaultsService.js"

export default {
  props: {
    vault: {
      type: Vault,
      required: true,
    }
  },
  setup() {
    return {
    }
  }
}
</script>


<style lang="scss" scoped>
.vaultcard {
  height: 170px;
  background-position: center;
  background-size: cover;
}

@media screen AND (max-width: 768px) {

}
</style>