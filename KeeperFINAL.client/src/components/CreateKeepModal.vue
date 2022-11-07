<template>
  <div class="modal fade" data-backdrop="static" id="createKeepModal" tabindex="-1"
    aria-labelledby="createKeepModalLabel" aria-hidden="true">
    <div class="modal-dialog">
      <div class="modal-content">
        <div class="modal-header border border-0">
          <div class="modal-title fs " id="createKeepModalLabel">Create Keep</div>

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
            <div class="d-flex justify-content-between">
              <!-- <div class="mb-3 form-check">
              <input type="checkbox" class="form-check-input" id="exampleCheck1">
              <label class="form-check-label" for="exampleCheck1">Check me out</label>
            </div> -->
              <button type="submit" class="btn btn-primary" data-bs-dismiss="modal">Submit</button>
              <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
            </div>
          </form>
        </div>
      </div>
    </div>
  </div>
</template>


<script>
import { ref } from "vue"
import { keepsService } from "../services/KeepsService.js";
import Pop from "../utils/Pop.js";

export default {
  setup() {
    const editable = ref({});
    return {
      editable,
      async handleSubmit() {
        try {
          await keepsService.createKeep(editable.value)
          editable.value = {}
        } catch (error) {
          console.error(error);
          Pop.error(error, "[handleSubmit]");
        }
      },
    }
  }
}
</script>


<style lang="scss" scoped>
.fs {
  font-size: 40px;
  font-weight: 700;
}
</style>