<template>
  <div class="col-3">
    <div class="vaultCard" @click="getVaultKeeps(vault.id)">
      <h3>{{ vault.name }}</h3>
    </div>
  </div>
</template>

<script>
import { reactive } from '@vue/reactivity'
import { AppState } from '../AppState'
import { vaultsService } from '../services/VaultsService'
export default {
  props: {
    vault: { type: Object, required: true }
  },
  setup() {
    const state = reactive({
      vault: AppState.profileVaults
    })
    return {
      state,
      getVaultKeeps(id) {
        vaultsService.getVaultKeeps(id)
        console.log(id, 'get this shit')
      }

    }
  }

}
</script>

<style scoped>
.vaultCard {
  height: 8rem;
  border: 2px solid black;
}

</style>
