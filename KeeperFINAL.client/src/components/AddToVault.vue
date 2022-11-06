<template>
    <div class="component">
      <form class="d-flex justify-content-center align-items-center">
        <div class="dropDown" height="30" width="30">
          <select v-model="editable" class="form-select" aria-label="Default select example">
            <option selected value="favorite">Favorite</option>
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
export default {
  setup(){
    return {}
  }
}
</script>


<style lang="scss" scoped>

</style>