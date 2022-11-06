<template>

  <div class="card bg-dark text-white border border-0 elevation-3 hoverable m-3 hover">
    <img :src="keep.img" class="card-img" alt="...">
    <div class="card-img-overlay d-flex align-items-end justify-content-between">
      <h5 @click="getKeepDetails()" class="card-title" data-bs-toggle="modal" data-bs-target="#exampleModal">
        {{ keep.name }}</h5>
      <img :src="keep.creator.picture" height="40" width="40" class="rounded-circle elevation-2" alt="">
    </div>
  </div>
</template>


<script>
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
    return {

      async getKeepDetails() {
        try {
          await keepsService.getKeepDetails(props.keep)
        } catch (error) {
          console.error('[]', error)
          Pop.error(error)
        }
      }
    }
  }
}
</script>


<style lang="scss" scoped>

</style>