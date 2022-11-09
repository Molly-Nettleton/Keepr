<template>
  <section class="container">
    <div class="pt-md-5 pt-3 pb-4 ">
      <div class="masonrycol">
        <div class="d-flex align-items-center" v-for="k in keeps">
          <KeepCard :keep="k" :key="k.id" />
        </div>
      </div>
    </div>
  </section>
</template>

<script>
import { computed } from "@vue/reactivity";
import { onMounted } from "vue";
import { AppState } from "../AppState.js";
import KeepCard from "../components/KeepCard.vue";
import { accountService } from "../services/AccountService.js";
import { keepsService } from "../services/KeepsService.js";
import { vaultsService } from "../services/VaultsService.js";
import Pop from "../utils/Pop.js";


export default {
  setup() {
    async function getAllKeeps() {
      try {
        await keepsService.getAllKeeps();
      }
      catch (error) {
        console.error("[getting all keeps]", error);
        Pop.error(error);
      }

    }
    onMounted(() => {
      getAllKeeps();
      AppState.activeVault = null;
    });
    return {
      keeps: computed(() => AppState.keeps),
      vaults: computed(() => AppState.accountVaults),
      account: computed(() => AppState.account),
      activeVault: computed(() => AppState.activeVault),
    };
  },
  components: { KeepCard, }
}
</script>

<style scoped lang="scss">
.masonrycol {
  columns: 4;
}

@media screen AND (max-width: 768px) {
  .masonrycol {
    columns: 2;
  }
}
</style>
