<template>
  <div class="card bg-dark text-white border border-0 elevation-3 hoverable m-3 hover vaultcard"  :style="{ backgroundImage: `url(${vault?.img})` }">
    <!-- <img :src="vault.img" class="card-img img-fluid" alt="..."> -->
    <div class="card-img-overlay d-flex align-items-end justify-content-between">
      <router-link :to="{ name: 'Vault', params: { id: vault.id } }" class="text-white">
        <h5>
          {{ vault.name }}</h5>
      </router-link>
    </div>
  </div>
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
.vaultcard{
  height: 170px;
  background-position: center;
  background-size: cover;
}
</style>