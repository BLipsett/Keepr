<template>
  <div
    class="
       modal
       fade"
    id="exampleModal"
    tabindex="-1"
    aria-labelledby="exampleModalLabel"
    aria-hidden="true"
  >
    <div v-if="state.activeKeep" class="modal-dialog">
      <div class="modal-content d-flex flex-column p-2">
        <div class="row">
          <div class="col-6">
            <img class="modalKeepImg" :src="state.activeKeep.img" />
          </div>
          <div class="col-6">
            <i class="fas fa-times ml-auto m-2" data-dismiss="modal" aria-label="Close" @click="unsetActive()"></i>
            <div class="icons col-12 d-flex justify-content-center p-2">
              <i class="fas fa-eye p-2">{{ state.activeKeep.views }} </i>
              <i class="fab fa-korvue p-2">{{ state.activeKeep.keeps }} </i>
              <i class="fas fa-share-alt p-2">{{ state.activeKeep.shares }} </i>
            </div>
            <div class="d-flex justify-content-center">
              <h2>
                {{ state.activeKeep.name }}
              </h2>
            </div>
            <div class="d-flex justify-content-center">
              <p>{{ state.activeKeep.description }}</p>
            </div>
            <br />
            <div v-if="state.activeKeep.creator.picture">
              <button class="btn-success">
                ADD TO VAULT
              </button>
              <i class="far fa-trash-alt"></i>
              <img v-if="state.activeKeep.creator.picture" class="profPic" :src="state.activeKeep.creator.picture" />
              <p>{{ state.activeKeep.creator.name }}</p>
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

<style scoped>

.icons i {
  font-size: 1.5rem;
}

.modalKeepImg {
  width: 30vw;
}

.modal-dialog {
  max-width: 60vw;
  width: 100%;
}

.profPic {
  height: 2rem;
  width: 2rem;
  border-radius: 25%;
}

</style>
