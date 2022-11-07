<template>
  <div class="modal fade" data-backdrop="static" id="createVaultModal" tabindex="-1"
    aria-labelledby="createVaultModalLabel" aria-hidden="true">
    <div class="modal-dialog">
      <div class="modal-content">
        <div class="modal-header border border-0">
          <div class="modal-title fs " id="createVaultModalLabel">Create Vault</div>

        </div>
        <div class="modal-body">
          <form @submit.prevent="handleSubmit()">
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
import { vaultsService } from "../services/VaultsService.js";
import { ref } from "vue"

export default {
  setup() {
    const editable = ref({});
    return {
      editable,
      async handleSubmit() {
        try {
          await vaultsService.createVault(editable.value)
          editable.value = {}
        } catch (error) {
          console.error(error);
        }
      }
    }
  }
}
</script>


<style lang="scss" scoped>
.title {
  font-size: 30px;
  font-weight: 700;
}

.fs {
  font-size: 40px;
  font-weight: 700;
}
</style>