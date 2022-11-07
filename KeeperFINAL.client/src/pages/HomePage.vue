<template>
  <section class="container">
    <div class="pt-5">
      <div class="masonrycol">
        <div class="" v-for="k in keeps">
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
import { keepsService } from "../services/KeepsService.js";
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
    });
    return {
      keeps: computed(() => AppState.keeps),
    };
  },
  components: { KeepCard, }
}
</script>

<style scoped lang="scss">
.masonrycol {
  columns: 4;
}
</style>
