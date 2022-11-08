<template>
  <div class="container" v-if="vault">
    <div class="row  justify-content-center">
      <div class="imgrow rounded-2 mt-5 text-white d-flex align-items-center justify-content-end flex-column "
        :style="{ backgroundImage: `url(${vault?.img})` }">
        <router-link :to="{ name: 'Profile', params: { id: vault?.creator.id } }" class="text-white text-center">
          <h1>{{ vault?.name }}</h1>
          <p>by {{ vault?.creator.name }}</p>
        </router-link>

        <div class="dropdown position-absolute drpdwn">
          <button class="button btn border border-3 dropdown-toggle hover rounded-pill" type="button"
            id="dropdownmenuvault" data-bs-toggle="dropdown" aria-expanded="false">
          </button>
          <ul class="dropdown-menu" aria-labelledby="dropdownmenuvault">
            <li class="dropdown-item border-bottom" data-bs-target="#editVaultModal" data-bs-toggle="modal">Edit Vault
            </li>
            <li @click="removeVault()"><a class="dropdown-item">Delete</a>
            </li>
          </ul>
        </div>

      </div>
      <div class="d-flex justify-content-center pt-3">
        <div class="bg-light py-2 px-3 rounded-3">{{ vault?.description }}</div>
      </div>
      <div class="d-flex justify-content-center pt-3">
        <div class="bg-grey py-2 px-3 rounded-pill">{{ vault?.keepCount }} keeps</div>
      </div>
    </div>
    <div class="pb-4 pt-4">
      <div class="masonrycol">
        <div class="pt-" v-for="k in keep">
          <KeepCard :keep="k" :key="k.id" />
        </div>
      </div>
    </div>
  </div>


  <div v-else class="container-fluid">
    <div class="row">
      <div class="col-12 text-center">
        <i class="mdi mdi-loading mdi-spin fs-1"></i>
        <i class="mdi mdi-loading mdi-spin mx-4 fs-1"></i>
        <i class="mdi mdi-loading mdi-spin fs-1"></i>
      </div>
    </div>
  </div>




  <div class="modal fade" data-backdrop="static" id="editVaultModal" tabindex="-1" aria-labelledby="editVaultModalLabel"
    aria-hidden="true">
    <div class="modal-dialog">
      <div class="modal-content">
        <div class="modal-header border border-0">
          <div class="modal-title fs " id="editVaultModalLabel">Edit Vault</div>

        </div>
        <div class="modal-body">
          <form @submit.prevent="editVault()">
            <div class="mb-3 ">
              <input type="text" class="form-control border border-0" required name="title" id="title"
                aria-describedby="Vault title" placeholder="Title..." v-model="editable.name">
            </div>
            <div class="mb-3">

              <input type="url" required name="imgurl" class="form-control border border-0" id="imgurl"
                placeholder="Img URL..." v-model="editable.img">
            </div>
            <div class="mb-3">

              <textarea type="text" class="form-control border border-0" id="description" placeholder="Description"
                rows="6" cols="50" v-model="editable.description"></textarea>
            </div>
            <div class="mb-3 form-check">
              <input type="checkbox" class="form-check-input" id="exampleCheck1" v-model="editable.isPrivate">
              <label class="form-check-label" for="exampleCheck1">Private vault?</label>
            </div>
            <button type="submit" class="btn btn-primary" data-bs-dismiss="modal">Submit</button>
            <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
          </form>
          <div class="d-flex justify-content-between">


          </div>
        </div>
      </div>
    </div>
  </div>
</template>


<script>
import { computed, ref } from "@vue/reactivity";
import { onMounted, watchEffect } from "vue";
import { useRoute, useRouter } from "vue-router";
import { AppState } from "../AppState.js";
import KeepCard from "../components/KeepCard.vue";
import { Vault } from "../models/Vault.js";
import { vaultsService } from "../services/VaultsService.js";
import Pop from "../utils/Pop.js";

export default {
  setup(props) {
    const editable = ref({});
    const route = useRoute();
    const router = useRouter();
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
    watchEffect(() => {
      if (AppState.activeVault?.isPrivate) {
        router.push({ name: 'Home' })
        Pop.toast('Private vault.')
        AppState.activeVault = null
      };
      editable.value = { ...AppState.activeVault }
    })
    onMounted(() => {
      getVaultById();
      getKeepsByVaultId();
    });
    return {
      editable,
      vault: computed(() => AppState.activeVault),
      keep: computed(() => AppState.profilesKeeps),
      // img: computed(() => `url(${AppState.activeVault?.img})`)
      async removeVault() {
        try {
          const yes = await Pop.confirm();
          if (!yes) {
            return;
          }
          await vaultsService.removeVault(route.params.id)
        } catch (error) {
          Pop.error(error);
        }
      },
      async editVault() {
        try {
          let vaultId = AppState.activeVault.id
          await vaultsService.editVault(editable.value, vaultId)
        } catch (error) {
          console.error('[]', error)
        }
      }
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

.drpdwn {
  transform: translateY(3rem) translateX(19em);
}

.masonrycol {
  columns: 4;
}

@media screen AND (max-width: 768px) {
  .masonrycol {
    columns: 2;
  }

  .drpdwn {
    transform: translateY(3rem) translateX(10em);
  }
}
</style>