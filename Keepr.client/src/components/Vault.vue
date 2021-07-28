<template>
  <div class="col-3">
    <div class="vaultCard">
      <h3>{{ vault.name }}</h3>
    </div>
  </div>
</template>

<script>
import { reactive } from '@vue/reactivity'
import { AppState } from '../AppState'
export default {
  props: {
    vault: { type: Object, required: true }
  },
  setup() {
    const state = reactive({
      vault: AppState.profileVaults
    })
    return {
      state

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
