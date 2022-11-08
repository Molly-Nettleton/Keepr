<template>
  <div class="account Page container" v-if="profile">

    <div class="container main">
      <div class="row bg-c5 eum-ipsum rounded-1 ">
        <div class="row">
          <img :src="profile.coverImg" alt="" class="banner img-fluid">
        </div>
        <div class="col-md-12 d-flex justify-content-center">
          <img :src="profile?.picture" alt="" class="eum rounded-circle mt-2 icon forcedImg" />
        </div>
      </div>
    </div>
    <div class="row justify-content-center align-items-center g-2 mt-3 mb-2">
      <div class="col-md text-center">
        <h1 class="username mt-5">{{ profile?.name }}</h1>
        <div class="fw-bold"> {{ keeps.length }} Keeps ║ ║ {{ vaults.length }} Vaults</div>
      </div>
    </div>
    <div>
      <h1>Vaults</h1>
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

  <div v-else class="container-fluid">
    <div class="row p-5">
      <div class="col-12 text-center p-5">
        <i class="mdi mdi-loading mdi-spin fs-1"></i>
        <i class="mdi mdi-loading mdi-spin mx-4 fs-1"></i>
        <i class="mdi mdi-loading mdi-spin fs-1"></i>
      </div>
    </div>
  </div>
</template>


<script>
import { computed } from "@vue/reactivity";
import { onMounted } from "vue";
import { useRoute } from "vue-router";
import { AppState } from "../AppState.js";
import KeepCard from "../components/KeepCard.vue";
import VaultCard from "../components/VaultCard.vue";
import { Account } from "../models/Account.js";
import { profilesService } from "../services/ProfilesService.js";
import Pop from "../utils/Pop.js";


export default {
  // props: {
  //   profile: {
  //     type: Account,
  //     required: true,
  //   }
  // },
  setup() {
    const route = useRoute();
    async function getProfileById() {
      try {
        await profilesService.getProfileById(route.params.id);
      } catch (error) {
        Pop.error(error, "[getProfile]");
      }
    }
    async function getProfilesKeeps() {
      try {
        await profilesService.getProfilesKeeps(route.params.id);
      }
      catch (error) {
        Pop.error(error);
      }
    }
    async function getProfilesVaults() {
      try {
        await profilesService.getProfilesVaults(route.params.id)
      } catch (error) {
        Pop.error(error);
      }
    }

    onMounted(() => {
      getProfileById();
      getProfilesKeeps();
      getProfilesVaults();
    });
    return {
      account: computed(() => AppState.account),
      profile: computed(() => AppState.activeProfile),
      keeps: computed(() => AppState.profilesKeeps),
      vaults: computed(() => AppState.profilesVaults),

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
    width: 300px;
    margin: 0%;
    padding: 0%;
  }

  .main {
    margin-left: 3.5rem;
    margin-top: 1rem;
  }
}
</style>