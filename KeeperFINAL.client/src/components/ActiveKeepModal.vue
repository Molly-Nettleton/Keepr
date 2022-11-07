<template>
  <!-- Modal -->
  <div class="modal fade pic" id="exampleModal" aria-labelledby="exampleModalLabel" aria-hidden="true">
    <div class="modal-dialog modal-dialog-centered modal-xl ">
      <div class="modal-content shadow all container-fluid p-0 pe-2 modalness">
        <div class="d-flex justify-content-between" v-if="keep">

          <div class="col-md-6">
            <img :src="keep?.img" alt="" class="imgcol rounded-start" @error="badImg" />
          </div>


          <div class="col-md-6 d-flex justify-content-between py-4 px-5 flex-column">

            <div class="">
              <div class="d-flex justify-content-center">

                <div class="">
                  <img src="src\assets\img\Vector (3).png" height="12" width="18" alt="">
                  <span class="ms-1">{{ keep?.viewCount }}</span>
                </div>

                <div class="ms-3"><img src="src\assets\img\Logo (1).png" height="18" width="18" alt="">
                  <span class="ms-1 ">{{ keep?.keptCount }}</span>
                </div>

              </div>
            </div>


            <div class=" justify-content-center">
              <h4 class="d-flex justify-content-center">{{ keep?.name }} </h4>
              <div class="d-flex justify-content-center">{{ keep?.description }}</div>
            </div>
            <div class=" ">
              <div class="d-flex justify-content-between">
                <AddToVault />
                <div class="text-white" data-bs-dismiss="modal">
                  <router-link :to="{ name: 'Profile', params: { id: keep?.creator.id } }">
                    {{ keep?.creator.name }}<img :src="keep?.creator.picture" height="50" width="50"
                      class="ms-2 rounded-circle elevation-2 hover" alt="">
                  </router-link>

                </div>
              </div>

            </div>
          </div>

        </div>
      </div>
    </div>
  </div>

</template>



<script>
import { computed, ref } from "@vue/reactivity";
import { watchEffect } from "vue";
import { AppState } from "../AppState.js";
import { Keep } from "../models/Keep.js";


export default {
  props: {
    keep: {
      type: Keep,
      required: true,
    }
  },
  setup() {

    watchEffect(() => {
      // AppState.activeKeep;
    })
    return {
      account: computed(() => AppState.account)
    }
  }
}
</script>


<style lang="scss" scoped>
.imgcol {
  height: 82vh;
  width: 550px;
  object-fit: cover;
}

.pic {
  background-color: #d2cedc;
}
</style>