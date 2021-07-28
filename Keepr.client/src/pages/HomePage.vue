<template>
  <div class="container-fluid">
    <div class="card-columns">
      <Keep v-for="k in state.keeps" :key="k.id" :keep="k" />
    </div>
  </div>
</template>
<script>
import { reactive } from '@vue/reactivity'
import { computed, watchEffect } from '@vue/runtime-core'
import { AppState } from '../AppState'
import { keepsService } from '../services/KeepsService'
// import { vaultsService } from '../services/VaultsService'
import { profilesService } from '../services/ProfilesService'

export default {
  setup() {
    const state = reactive({
      keeps: computed(() => AppState.keeps),
      account: computed(() => AppState.account)
    })
    watchEffect(async() => keepsService.getKeeps())
    watchEffect(async() => profilesService.getVaultsByProfile(state.account.id))

    return {
      state
    }
  }
}
</script>

  <style lang="scss">
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
