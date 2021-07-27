<template>
  <div class="container-fluid">
    <div class="row">
      <div class="card-columns">
        <Keep v-for="k in state.keeps" :key="k.id" :keep="k" />
      </div>
    </div>
  </div>
</template>
<script>
import { reactive } from '@vue/reactivity'
import { computed, watchEffect } from '@vue/runtime-core'
import { AppState } from '../AppState'
import { keepsService } from '../services/KeepsService'

export default {
  setup() {
    const state = reactive({
      keeps: computed(() => AppState.keeps)
    })
    watchEffect(async() => keepsService.getKeeps())
    return {
      state
    }
  }
}
</script>

  <style scoped lang="scss">
    @media (min-width: 576px){
    .card-columns {
    column-count: 4;
    column-gap: 1.25rem;
    orphans: 1;
    widows: 1;
    }
    }

    @media (max-width: 576px)
    {
    .card-columns {
    column-count: 3;
    column-gap: 1.25rem;
    orphans: 1;
    widows: 1;
    }
    }
  </style>
