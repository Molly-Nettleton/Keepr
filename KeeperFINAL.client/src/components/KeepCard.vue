<template>
  <span v-if="routeAccount">
    <div class="card bg-dark text-white border border-0 elevation-4 m-3 hover">
      <img :src="keep.img" class="card-img" alt="...">
      <div class="card-img-overlay d-flex align-items-end justify-content-between">

        <div @click="getKeepDetails()" class="card-title fontbg fs-3 fw-bold text-lowercase hoverable"
          data-bs-toggle="modal" data-bs-target="#exampleModal">
          <span :title="keep.name" :aria-label="keep.name">{{ keep.name }}</span>
        </div>

        <div v-if="account.id == keep.creatorId" class=" text-end">
          <i class="mdi mdi-delete fs-5 remove rounded-5 px-1 hoverable" @click="removeKeep()"></i>
        </div>
      </div>
    </div>

  </span>


  <span v-else-if="routeHome">
    <div @click="getKeepDetails()" data-bs-toggle="modal" data-bs-target="#exampleModal"
      class="card bg-dark text-white border border-0 elevation-5 hoverable my-2 hover">
      <img :src="keep.img" class="card-img" alt="..." :title="keep.name" :aria-label="keep.name">
      <div class="card-img-overlay d-flex align-items-end justify-content-between">
        <div class="card-title fontbg fs-3 fw-bold text-lowercase">
          <span :title="keep.name" :aria-label="keep.name" class="title">{{ keep.name }}</span>
        </div>
        <img :src="keep.creator.picture" height="40" width="40"
          class="rounded-circle elevation-2 userpic border border-2 border-solid border-white" alt=""
          :title="keep.creator.name" :aria-label="keep.creator.name">
      </div>
    </div>
  </span>

  <span v-else>
    <div @click="getKeepDetails()" data-bs-toggle="modal" data-bs-target="#exampleModal"
      class="card bg-dark text-white border border-0 elevation-3 hoverable m-3 hover">
      <img :src="keep.img" class="card-img" alt="..." :title="keep.name" :aria-label="keep.name">
      <div class="card-img-overlay d-flex align-items-end justify-content-between">
        <div class="card-title fontbg fs-3 fw-bold text-lowercase">
          <span :title="keep.name" :aria-label="keep.name" class="title">{{ keep.name }}</span>
        </div>
      </div>
    </div>
  </span>


</template>


<script>
import { computed } from "@vue/reactivity"
import { useRoute } from "vue-router"
import { AppState } from "../AppState.js"
import { Keep } from "../models/Keep.js"
import { keepsService } from "../services/KeepsService.js"
import Pop from "../utils/Pop.js"

export default {
  props: {
    keep: {
      type: Keep,
      required: true,
    }
  },
  setup(props) {
    const route = useRoute()
    return {
      routeAccount: computed(() => route.name == "Account"),
      routeHome: computed(() => route.name == "Home"),
      routeVault: computed(() => route.name == "Vault"),
      account: computed(() => AppState.account),

      async getKeepDetails() {
        try {
          await keepsService.getKeepDetails(props.keep)
        } catch (error) {
          console.error('[]', error)
          Pop.error(error)
        }
      },

      async removeKeep() {
        try {
          const yes = await Pop.confirm();
          if (!yes) {
            return;
          }
          await keepsService.removeKeep(props.keep.id);
          Pop.success(`Keep deleted.`)
        } catch (error) {
          Pop.error(error);
        }
      }
    }
  }
}
</script>


<style lang="scss" scoped>
.remove {
  color: rgba(0, 0, 0, 0.279);
  background-color: rgba(255, 255, 255, 0.284);
}

.remove:hover {
  color: rgb(255, 0, 0);
  background-color: white;
  transition: all 0.7s ease;
}

@media screen AND (max-width: 768px) {
  .title {
    font-size: small;
  }

  .userpic {
    display: none;
  }
}
</style>