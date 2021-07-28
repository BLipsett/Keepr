<template>
  <div class="modal fade" id="exampleModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
    <div class="modal-dialog">
      <div class="modal-content d-flex">
        <button type="button" class="btn-close btn-block" data-dismiss="modal" aria-label="Close" @click="unsetActive()">
          close
        </button>
        <div class="row">
          <div class="col-6">
            <img class="modalKeepImg" :src="state.activeKeep.img" />
          </div>
          <div class="justify-content-center">
            <div class="col-6 d-flex ">
              <div class="r">
                <i class="fas fa-eye">{{ state.activeKeep.views }} </i>
                <i class="fab fa-korvue">{{ state.activeKeep.keeps }} </i>
                <i class="fas fa-share-alt">{{ state.activeKeep.shares }} </i>
              </div>
              <h2>
                {{ state.activeKeep.name }}
              </h2>
              <p>{{ state.activeKeep.description }}</p>
            </div>
          </div>
        </div>
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

.modalKeepImg {
  width: 30vw;
}

.modal-dialog {
  max-width: 60vw;
  width: 100%;
}

</style>
