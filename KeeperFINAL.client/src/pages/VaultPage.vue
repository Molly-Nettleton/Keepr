<template>
  <div class="container">
    <div class="row  justify-content-center">
      <div class="imgrow rounded-2 mt-5 text-white d-flex align-items-center justify-content-end flex-column"
        :style="{ backgroundImage: `url(${vault?.img})` }">
        <h1>{{ vault?.name }}</h1>
        <p>by {{ vault?.creator.name }}</p>
      </div>

    </div>

    <div>
      <h1>Keeps</h1>
      <div class="">
        <div class="masonrycol" v-for="k in keep">
          <KeepCard :keep="k" :key="k.id" />
        </div>
      </div>
    </div>
  </div>
</template>


<script>
import { computed } from "@vue/reactivity";
import { onMounted } from "vue";
import { useRoute } from "vue-router";
import { AppState } from "../AppState.js";
import KeepCard from "../components/KeepCard.vue";
import { vaultsService } from "../services/VaultsService.js";
import Pop from "../utils/Pop.js";

export default {
  setup() {
    const route = useRoute();
    async function getVaultById() {
      try {
        await vaultsService.getVaultById(route.params.id);
      }
      catch (error) {
        Pop.error(error, "[getVault]");
      }
    }
    async function getKeepsByVaultId() {
      try {
        
        await vaultsService.getKeepsByVaultId(route.params.id);
      }
      catch (error) {
        Pop.error(error);
      }
    }
    onMounted(() => {
      getVaultById();
      getKeepsByVaultId();
    });
    return {
      vault: computed(() => AppState.activeVault),
      keep: computed(() => AppState.profilesKeeps)
      // img: computed(() => `url(${AppState.activeVault?.img})`)
    };
  },
  components: { KeepCard }
}
</script>


<style lang="scss" scoped>
.imgrow {
  background-size: cover;
  background-position: center;
  height: 250px;
  width: 650px;
}

.masonrycol {
  columns: 4;
}
</style>