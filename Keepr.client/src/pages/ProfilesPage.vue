<template>
  <div class="container-fluid">
    <div class="row mb-4">
      <div class="col-md-3">
        <img v-if="state.profile.picture == state.profile.picture" class="profCreator" :src="state.profile.picture" />
      </div>
      <div class="col-md-3 d-flex flex-row">
        <div>
          <h1>
            {{ state.profile.name }}
          </h1>
        </div>
        <h2>
          Vaults: {{ state.vaults.length }}
        </h2>
        <h2>
          Keeps: {{ state.keeps.length }}
        </h2>
      </div>
    </div>
    <div class="row">
      <div class="col-12">
        <button type="button" class="btn btn-primary" data-toggle="modal" data-target="#vaultModal">
          create vault
        </button>
      </div>
    </div>
    <div class="row">
      <Vault v-for="v in state.vaults" :key="v.id" :vault="v" />
    </div>
    <div class="row">
      <div class="col-12">
        <button type="button" class="btn btn-primary" data-toggle="modal" data-target="#keepModal">
          create keep
        </button>
      </div>
      <div class="row">
        <div class="card-columns">
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
      state
    }
  }
}
</script>

<style scoped>
.profCreator {
  height: 12rem;
  width: 12rem;
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
