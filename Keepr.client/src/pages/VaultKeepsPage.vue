<template>
  <h2>
    vault keeps
  </h2>
  <div class="container-fluid">
    <div class="card-columns">
      <Keep v-for="v in state.vKeeps" :key="v.id" :keep="v" />
    </div>
  </div>
</template>

<script>
import { reactive } from '@vue/reactivity'
import { AppState } from '../AppState'
import { computed, watchEffect } from '@vue/runtime-core'
import Keep from '../components/Keep.vue'
import { vaultsService } from '../services/VaultsService'
import { useRoute } from 'vue-router'
export default {
  // components: { Keep },
  setup() {
    const route = useRoute()
    const state = reactive({
      vKeeps: computed(() => AppState.vaultKeeps)
    })
    watchEffect(async() => vaultsService.getVaultKeeps(route.params.id))
    return {
      state

    }
  }

}
</script>

<style>

</style>
