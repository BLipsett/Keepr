<template>
  <h2>
    vault keeps
  </h2>
  <i v-if="state.account.id == state.activeVault.creatorId" class="trash far fa-trash-alt" @click="deleteVault(state.activeVault.id)"></i>
  <div class="container-fluid">
    <div class="card-columns">
      <VaultKeep v-for="v in state.vKeeps" :key="v.id" :keep="v" />
    </div>
  </div>
  <VaultKeepModal />
</template>

<script>
import { reactive } from '@vue/reactivity'
import { AppState } from '../AppState'
import { computed, watchEffect } from '@vue/runtime-core'
import { vaultsService } from '../services/VaultsService'
import { useRoute } from 'vue-router'
export default {
  setup() {
    const route = useRoute()
    const state = reactive({
      vKeeps: computed(() => AppState.vaultKeeps),
      account: computed(() => AppState.account),
      activeVault: computed(() => AppState.activeVault)
    })
    watchEffect(async() => vaultsService.getVaultKeeps(route.params.id))
    watchEffect(async() => vaultsService.getVault(route.params.id))
    return {
      state,
      async deleteVault(id) {
        await vaultsService.deleteVault(id)
      }

    }
  }

}
</script>

<style scoped>
.trash {
  font-size: 3rem
}

</style>
