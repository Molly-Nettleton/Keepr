<template>
  <!-- Modal -->
  <div class="modal fade pic" id="exampleModal" aria-labelledby="exampleModalLabel" aria-hidden="true">
    <div class="modal-dialog modal-dialog-centered modal-xl ">
      <div class="modal-content shadow all container-fluid p-0 pe-2 modalness">
        <div class="d-flex justify-content-between cols" v-if="keep">

          <div class="col-6 col1" title="Keep Image" aria-label="Keep Image">
            <img :src="keep?.img" alt="" class="imgcol rounded-start" @error="badImg" />
          </div>

          <div class="col-md-6 col2 d-flex justify-content-between py-4 px-5 flex-column">
            <div class="counts">
              <div class="d-flex justify-content-center">
                <div class="" title="View Count" aria-label="Keep View Count">
                  <img src="src\assets\img\Vector (3).png" height="12" width="18" alt="">
                  <span class="ms-1">{{ keep?.viewCount }}</span>
                </div>
                <div class="ms-3" title="Vault Count" aria-label="Keep Vault Count">
                  <img src="src\assets\img\Logo (1).png" height="18" width="18" alt="">
                  <span class="ms-1 ">{{ keep?.keptCount }}</span>
                </div>
              </div>


            </div>
            <div class=" justify-content-center">
              <div class="title">
                <h4 class="d-flex justify-content-center fw-bold" title="Keep Name" aria-label="Keep Name">{{ keep?.name
                }}
                </h4>
              </div>
              <div class="d-flex justify-content-center desc" title="Description" aria-label="Keep description"> {{
                  keep?.description
              }}</div>
            </div>
            <div class=" other" title="Add to a vault" aria-label="Add to a vault.">
              <div class="d-flex justify-content-between">
                <AddToVault />

                <!-- <span v-if="account.id == activeVault.creatorId"> -->
                <span v-if="activeVault && user.isAuthenticated">
                  <button class="btn border border-3 p-2 mt-4 hover"
                    @click="removeFromKeep(keep?.vaultKeepId)">Remove</button>
                </span>
                <!-- </span> -->

                <span v-if="account.id == keep.creatorId">
                  <div class="text-white mt-3" data-bs-dismiss="modal" title="Creator name" aria-label="Creator Name">
                    <router-link :to="{ name: 'Account', params: { id: keep?.creator.id } }">
                      <img :src="keep?.creator.picture" height="50" width="50"
                        class="ms-2 rounded-circle elevation-2 hover creator" alt=""> <span class="text-dark name">{{
                            keep?.creator.name
                        }}</span>
                    </router-link>
                  </div>
                </span>

                <span v-else>
                  <div class="text-white userstuff mt-3" data-bs-dismiss="modal" title="Creator name"
                    aria-label="Creator Name">
                    <router-link :to="{ name: 'Profile', params: { id: keep?.creator.id } }">
                      <img :src="keep?.creator.picture" height="50" width="50"
                        class="ms-2 rounded-circle elevation-2 hover " alt="">
                      <span class="text-dark name ms-2">{{ keep?.creator.name }}</span>
                    </router-link>

                  </div>
                </span>
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
import { AppState } from "../AppState.js";
import { Keep } from "../models/Keep.js";
import Pop from "../utils/Pop.js";
import { vaultKeepsService } from "../services/VaultKeepsService.js";
import { Modal } from "bootstrap";

export default {
  props: {
    keep: {
      type: Keep,
      required: true,
    }
  },
  setup() {
    return {
      account: computed(() => AppState.account),
      activeVault: computed(() => AppState.activeVault),
      user: computed(() => AppState.user),

      async removeFromKeep(id) {
        try {
          const yes = await Pop.confirm();
          if (!yes) {
            return;
          }
          await vaultKeepsService.removeFromVault(id)
          Modal.getOrCreateInstance('#exampleModal').hide()
          Pop.success("Removed from vault.")
        } catch (error) {
          Pop.error('[]', error)
        }
      }
    }
  }
}
</script>


<style lang="scss" scoped>
.imgcol {
  height: 82vh;
  width: 564px;
  object-fit: cover;
}

.pic {
  background-color: #d2cedce8;
}

@media screen AND (max-width: 768px) {
  .cols {
    flex-direction: column;
  }

  .col2 {
    padding: 0% !important;
  }

  .imgcol {
    border-radius: 0% !important;
    height: 82vh;
    width: 478.5px;
    object-fit: fit;
  }

  .desc {
    text-align: center;
    padding: 1rem
  }

  .title {
    margin-top: 1rem;
  }

  .counts {
    margin-top: 1rem;
  }

  .other {
    padding-top: 1rem;
    padding-bottom: 1rem;
    padding-left: 1rem;
    padding-right: 1rem;
  }

  .name {
    margin-left: 1rem
  }
}
</style>