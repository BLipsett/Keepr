<template>
  <div class="container-fluid" v-if="state.profile">
    <div class="row my-4">
      <div class="col-md-3">
        <img v-if="state.profile.picture == state.profile.picture" class="profCreator" :src="state.profile.picture" />
      </div>

      <div class="col-md-3 ">
        <h1>
          {{ state.profile.name }}
        </h1>

        <h2>
          Vaults: {{ state.vaults.length }}
        </h2>
        <h2>
          Keeps: {{ state.keeps.length }}
        </h2>
      </div>
    </div>

    <div class="row">
      <div class="col-12 d-flex flex-row">
        <h3>Create Vault<i class="fas fa-plus-circle pl-1" v-if="state.profile.id == account.id" data-toggle="modal" data-target="#vaultModal"></i></h3>
      </div>
    </div>
    <div class="row m-2">
      <Vault v-for="v in state.vaults" :key="v.id" :vault="v" />
    </div>
    <div class="row">
      <div class="col-12">
        <h3>Create Keep<i class="fas fa-plus-circle pl-1" v-if="state.profile.id == account.id" data-toggle="modal" data-target="#keepModal"></i></h3>

        <!-- <button v-if="state.profile.id == account.id" type="button" class="btn btn-primary" data-toggle="modal" data-target="#keepModal">
          create keep
        </button> -->
      </div>
      <div class="row m-2">
        <div class="card-columns m-2">
          <Keep v-for="k in state.keeps" :key="k.id" :keep="k" />
        </div>
      </div>
    </div>
  </div>
  <CreateKeepModal />
  <CreateVaultModal />
</template>

<script>
import { computed, reactive, watchEffect } from '@vue/runtime-core'
import { profilesService } from '../services/ProfilesService'
import { logger } from '../utils/Logger'
import { useRoute } from 'vue-router'
import { AppState } from '../AppState'
// import { createPopper } from 'popperjs/core'
export default {
  setup() {
    const state = reactive({
      keeps: computed(() => AppState.profileKeeps),
      vaults: computed(() => AppState.profileVaults),
      profile: computed(() => AppState.profile)

    })
    const route = useRoute()
    watchEffect(
      async() => {
        try {
          await profilesService.getProfile(route.params.id)
          await profilesService.getVaultsByProfile(route.params.id)
          await profilesService.getKeepsByProfile(route.params.id)
        } catch (error) {
          logger.log('couldnt find keeps')
        }
      }
    )
    return {
      state,
      account: computed(() => AppState.account)
    }
  }
}
</script>

<style scoped>
.profCreator {
  height: 11.5rem;
  width: 11.5rem;
  border-radius: 25%
}

@media (min-width: 576px){
    .card-columns {
    column-count: 5;
    column-gap: 1.25rem;
    orphans: 1;
    widows: 1;
    }
    }

    @media (max-width: 576px)
    {
    .card-columns {
    column-count: 4;
    column-gap: 1.25rem;
    orphans: 1;
    widows: 1;
    }
    }

</style>
