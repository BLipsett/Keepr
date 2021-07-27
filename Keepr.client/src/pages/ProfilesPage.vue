<template>
  <h1>Profile page</h1>
  <div class="container-fluid">
    <div class="row">
      <div class="card-columns">
        <Vault v-for="v in state.vaults" :key="v.id" :keep="v" />
      </div>
    </div>
  </div>

  <div class="row">
    <div class="card-columns">
      <Keep v-for="k in state.keeps" :key="k.id" :keep="k" />
    </div>
  </div>
</template>

<script>
import { computed, reactive, watchEffect } from '@vue/runtime-core'
import { profilesService } from '../services/ProfilesService'
import { logger } from '../utils/Logger'
import { useRoute } from 'vue-router'
import { AppState } from '../AppState'
export default {
  setup() {
    const state = reactive({
      keeps: computed(() => AppState.profileKeeps),
      vaults: computed(() => AppState.profileVaults)

    })
    const route = useRoute()
    watchEffect(
      async() => {
        try {
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

<style>

</style>
