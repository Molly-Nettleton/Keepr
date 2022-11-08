<template>
  <div class="account container">
    <div class="container main">
      <div class="bg-c5 eum-ipsum rounded-5 " :style="{ backgroundImage: `url(${account?.coverImg})` }">
        <div class="col-md-12 d-flex justify-content-center">
          <img :src="account?.picture" alt="" class="eum rounded-circle mt-2 icon forcedImg" />
        </div>
      </div>
    </div>

    <div class="row justify-content-center align-items-center g-2 mt-3 mb-2">
      <div class="col-md text-center">

        <div class="dropdown position-absolute drpdwn">
          <button class="button btn border border-3 dropdown-toggle hover rounded-pill" type="button"
            id="dropdownmenuvault" data-bs-toggle="dropdown" aria-expanded="false">
          </button>
          <ul class="dropdown-menu" aria-labelledby="dropdownmenu">
            <li class="dropdown-item" data-bs-target="#editAccountModal" data-bs-toggle="modal">Edit Account Info
            </li>
          </ul>
        </div>


        <h1 class="username mt-5">{{ account?.name }}</h1>
        <div class="fw-bold">{{vaults.length}} Vaults ║║  {{keeps.length}} Keeps</div>
      </div>
    </div>
    <div>
      <h1 class="vaults">Vaults</h1>
      <div class="row">
        <div class="col-md-3" v-for="v in vaults">
          <VaultCard :vault="v" :key="v.id" />
        </div>
      </div>
    </div>
    <div>
      <h1>Keeps</h1>
      <div class="masonrycol mb-3">
        <div class="" v-for="k in keeps">
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
              <input type="text" class="form-control border border-0" required name="name" id="name"
                aria-describedby="Profile Name" placeholder="Name..." v-model="editable.name">
            </div>
            <div class="mb-3">

              <input type="url" required name="picture" class="form-control border border-0" id="picture"
                placeholder="Profile Picture" v-model="editable.picture">
            </div>
            <div class="mb-3">

              <input type="url" class="form-control border border-0" id="coverImg" placeholder="coverImg" rows="6"
                cols="50" v-model="editable.coverImg">
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
import { onMounted } from "vue";
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
        getMyVaults()
    });
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
  height: 350px;
  margin-top: 50px;
  position: relative;
  margin-inline: 200px;
}

.banner {
  height: 43.5vh;
  object-fit: cover;
  border-radius: 55px;
}

.eum {
  position: absolute;
  bottom: -17%;
}

.forcedImg {
  height: 120px;
  width: 120px;
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
  transform: translateY(-1rem) translateX(64.5rem);
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

  .eum-ipsum {
    width: 230px;
    margin: 0%;
    padding: 0%;
  }

  .main {
    margin-left: 3rem;
    margin-top: 1rem;
  }

    .drpdwn {
      transform: translateY(-1rem) translateX(15.5rem);
    }

    .vaults{
      padding-left: 1rem;
    }

}
</style>