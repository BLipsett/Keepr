<template>
  <div class="modal fade" id="exampleModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
    <div class="modal-dialog">
      <div class="modal-content">
        <div class="modal-header">
          <h5 class="modal-title" id="exampleModalLabel">
            Modal title
          </h5>
          <button type="button" class="btn-close btn-block" data-dismiss="modal" aria-label="Close" @click="unsetActive()">
            close
          </button>
        </div>
        <div class="modal-body">
          <img class="keepImg" :src="state.activeKeep.img" />
          <p>{{ state.activeKeep.name }}</p>
          ..
        </div>
        <button class="btn-primary" @click="read">
          keep button
        </button>
      </div>
    </div>
  </div>
</template>

<script>
import { reactive } from '@vue/reactivity'
import { AppState } from '../AppState'
import { computed } from '@vue/runtime-core'

export default {
  props: {
    keep: { type: Object, required: true }
  },
  setup(props) {
    const state = reactive({
      activeKeep: computed(() => AppState.activeKeep)
    })
    return {
      state,
      read() {
        console.log(state.activeKeep)
      },
      unsetActive() {
        AppState.activeKeep = {}
      }

    }
  }
}
</script>

<style>

.keepImg {
  width: 20vw;
}

</style>
