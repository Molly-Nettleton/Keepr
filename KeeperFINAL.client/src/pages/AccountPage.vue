<template>
  <div class="account container px-5">


    <div class="col-12">
      <div class="bg-c5 eum-ipsum rounded-5 " :style="{ backgroundImage: `url(${account?.coverImg})` }">
        <div class="col-md-12 d-flex justify-content-center">
          <img :src="account?.picture" alt="" class="eum rounded-circle icon forcedImg" />
        </div>
      </div>
    </div>



    <div class="row justify-content-center align-items-center g-2 mt-5 mb-2">
      <div class="col-md text-center">

        <div title="Edit account info" class="dropdown position-absolute drpdwn">
          <button class="button btn border border-3 dropdown-toggle hover rounded-pill" type="button"
            id="dropdownmenuvault" data-bs-toggle="dropdown" aria-expanded="false">
          </button>
          <ul class="dropdown-menu" aria-labelledby="dropdownmenu">
            <li class="dropdown-item" data-bs-target="#editAccountModal" data-bs-toggle="modal">Edit Account Info
            </li>
          </ul>
        </div>


        <h1 class="username mt-md-5 mt-3 fw-bold">{{ account?.name }}</h1>
        <div class="fw-bold">{{ vaults.length }} Vaults ║ {{ keeps.length }} Keeps</div>
      </div>
    </div>
    <div>
      <h1 class="vaults fw-bold">Vaults</h1>
      <div class="row vaultr">
        <div class="col-md-3" v-for="v in vaults">
          <VaultCard :vault="v" :key="v.id" />
        </div>
      </div>
    </div>
    <div class="mt-4">
      <h1 class="fw-bold">Keeps</h1>
      <div class="masonrycol mb-3">
        <div class="d-flex align-items-center" v-for="k in keeps">
          <KeepCard :keep="k" :key="k.id" />
        </div>
      </div>
    </div>
  </div>


  <div class="modal fade" data-backdrop="static" id="editAccountModal" tabindex="-1"
    aria-labelledby="editAccountModalLabel" aria-hidden="true">
    <div class="modal-dialog">
      <div class="modal-content">
        <div class="modal-header border border-0">
          <div class="modal-title fs " id="editAccountModalLabel">Edit Account Info</div>

        </div>
        <div class="modal-body">
          <form @submit.prevent="editAccountInfo()">
            <div class="mb-3 ">
              <label for="name" class="form-label">Name</label>
              <input type="text" class="form-control border border-0" name="name" id="name"
                aria-describedby="Profile Name" placeholder="Name..." v-model="editable.name">

            </div>
            <div class="mb-3">
              <label for="picture" class="form-label">Picture</label>
              <input type="url" name="picture" class="form-control border border-0" id="picture"
                placeholder="Profile Picture" v-model="editable.picture">
            </div>
            <div class="mb-3">
              <label for="coverimg" class="form-label">Cover Image</label>
              <input type="url" class="form-control border border-0" name="coverImg" id="coverImg"
                placeholder="coverImg" rows="6" cols="50" v-model="editable.coverImg">
            </div>
            <div class="d-flex justify-content-between">
              <button type="submit" class="btn btn-primary" data-bs-dismiss="modal">Submit</button>
              <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
            </div>
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
import { AppState } from "../AppState.js";
import { Account } from "../models/Account.js";
import Pop from "../utils/Pop.js";
import KeepCard from "../components/KeepCard.vue";
import { accountService } from "../services/AccountService.js";
import VaultCard from "../components/VaultCard.vue";


export default {
  setup() {
    const editable = ref({});
    async function getMyKeeps() {
      try {
        await accountService.getMyKeeps();
      }
      catch (error) {
        Pop.error(error);
      }
    }

    async function getMyVaults() {
      try {
        await accountService.getMyVaults();
      } catch (error) {
        Pop.error(error)
      }
    }
    onMounted(() => {
      getMyKeeps(),
        getMyVaults(),
        AppState.activeVault = null;
    });
    watchEffect(() => {
      editable.value = { ...AppState.account }
    })
    return {
      editable,
      account: computed(() => AppState.account),
      keeps: computed(() => AppState.accountKeeps),
      vaults: computed(() => AppState.accountVaults),

      async editAccountInfo() {
        try {
          let accountId = AppState.account.id
          await accountService.editAccountInfo(editable.value, accountId)
        } catch (error) {
          console.error('[]', error)
        }
      }
    };

  },
  components: { KeepCard, VaultCard }
}
</script>


<style lang="scss" scoped>
.eum-ipsum {
  // background-image: URL("https://images.unsplash.com/photo-1553532070-9f677c9df3dc");
  background-size: cover;
  background-position: center;
  height: 500px;
  margin-top: 50px;
  position: relative;
}

.banner {
  height: 43.5vh;
  object-fit: cover;
  object-position: center;
  border-radius: 55px;
}

.eum {
  position: absolute;
  bottom: -17%;
}

.forcedImg {
  height: 160px;
  width: 160px;
}

.icon {
  border: 3px solid white;
}

.cardholder {
  max-height: 70vh;
  overflow-y: auto;
  overflow-x: hidden;
}

.masonrycol {
  columns: 4;
}

.drpdwn {
  transform: translateY(-3rem) translateX(72.5rem);
}

// .vaultr {
//   height: 450px;
//   overflow: auto;
// }

.fs {
  font-size: 40px;
  font-weight: 700;
}

@media screen AND (max-width: 768px) {
  .masonrycol {
    columns: 2;
  }

  .banner {
    height: 43.5vh;
    object-fit: cover;
    border-radius: 55px;
  }


  .main {
    margin-left: 3rem;
    margin-top: 1rem;
  }

  .drpdwn {
    transform: translateY(-3rem) translateX(22.5rem);
  }

  .vaults {
    padding-left: 1rem;
  }

  .eum-ipsum {
    // background-image: URL("https://images.unsplash.com/photo-1553532070-9f677c9df3dc");
    background-size: cover;
    background-position: center;
    height: 400px;
    margin-top: 50px;
    position: relative;
  }

}
</style>