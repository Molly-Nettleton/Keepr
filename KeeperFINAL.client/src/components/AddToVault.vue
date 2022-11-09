<template>
  <div class="component" v-if="user.isAuthenticated">
    <div>
      <div class="text-grey">Add to vault..</div>
    </div>
    <form @submit.prevent="handleSubmit()" class="d-flex justify-content-center align-items-center border-0">
      <div class="dropDown" height="30" width="30">
        <select v-model="editable" class="form-select hoverable border border-grey border-3" required
          title="Add to vault" aria-label="Select vault..">
          <option v-for="v in vaults" :value="v" required selected><a class="dropdown-item">{{ v.name }}</a></option>
        </select>
      </div>
      <div class="ms-2">
        <button class="btn p-0 px-1 bg-c6 mt-1 add hoverable" type="submit">
          <i class="fs-5 add mdi mdi-plus text-dark"></i>
        </button>
      </div>
    </form>
  </div>
</template>


<script>
import { computed } from "@vue/reactivity";
import { AppState } from "../AppState.js";
import { ref } from "vue"
import { vaultKeepsService } from "../services/VaultKeepsService.js";
import Pop from "../utils/Pop.js";

export default {

  setup() {
    const editable = ref({});
    return {
      editable,
      async handleSubmit() {
        let formData = {
          keepId: AppState.activeKeep.id,
          vaultId: editable.value.id

        }
        console.log(formData);

        try {
          Pop.success(
            `Added to list.`
          );
          console.log(formData);
          await vaultKeepsService.createVaultKeep(formData)

        } catch (error) {
          console.error(error)
        }
      },
      vaults: computed(() => AppState.accountVaults),
      user: computed(() => AppState.user),
      activeKeep: computed(() => AppState.activeKeep)
    }
  }
}
</script>


<style lang="scss" scoped>

</style>