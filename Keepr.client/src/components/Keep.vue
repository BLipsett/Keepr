<template>
  <div class="card keepCard" @click="setActiveKeep(keep)" data-toggle="modal" data-target="#exampleModal">
    <p>{{ keep.name }}</p>
    <p>{{ keep.shares }}</p>
    <img class="keepImg" :src="keep.img" />
    <button type="button" class="btn btn-primary" data-toggle="modal" data-target="#exampleModal">
      Launch demo modal
    </button>
  </div>
  <div v-if="state.activeKeep">
    <KeepModal :keep="state.activeKeep" />
  </div>
</template>

<script>
import { reactive } from '@vue/reactivity'

import { AppState } from '../AppState'
export default {
  props: {
    keep: { type: Object, required: true }
  },
  setup() {
    const state = reactive({
      activeKeep: AppState.activeKeep
    })
    return {
      state,
      async setActiveKeep(keep) {
        AppState.activeKeep = keep
        console.log(AppState.activeKeep)
      }

    }
  }

}
</script>

<style>
.keepCard {
max-width: 100%;
width: 12vw;
}

.keepImg {
max-width: 100%;
  object-fit: cover;
}
</style>
