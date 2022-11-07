<template>
  <div class="component">
    <form @submit.prevent="handleSubmit()" class="d-flex justify-content-center align-items-center">
      <div class="dropDown" height="30" width="30">
        <select v-model="editable" class="form-select" aria-label="Default select example">


          <option v-for="v in vaults" :value="v"><a class="dropdown-item" >{{ v.name }}</a></option>

        </select>
      </div>

      <div class="ms-2">
        <button class="btn p-0 px-1 bg-c6 mt-1 add" type="submit">
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

export default {

  setup() {
    const editable = ref({});
    return {
      editable,
      async handleSubmit() {
// console.log(AppState.accountVaults);
      

        
        let formData = {
          keepId: AppState.activeKeep.id,
          vaultId: editable.value.id
        }
        try {
          console.log(formData);
          await vaultKeepsService.createVaultKeep(formData)
        } catch (error) {
        }
      },
      vaults: computed(() => AppState.accountVaults),
    }
  }
}
</script>


<style lang="scss" scoped>

</style>