<template>
  <div v-if="state.activeKeep != null"
       class="
       modal
       fade"
       id="vaultKeepModal"
       tabindex="-1"
       aria-labelledby="vaultKeepModalModalLabel"
       aria-hidden="true"
  >
    <div class="modal-dialog">
      <div class="modal-content d-flex flex-column p-2">
        <div class="row">
          <div class="col-6">
            <img class="modalKeepImg" :src="state.activeKeep.img" />
          </div>
          <div class="col-6">
            <i class="fas fa-times ml-auto m-2" data-dismiss="modal" aria-label="Close"></i>
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
              <div class="dropdown">
                <button class="btn btn-secondary dropdown-toggle"
                        type="button"
                        id="dropdownMenuButton"
                        data-toggle="dropdown"
                        aria-haspopup="true"
                        aria-expanded="false"
                >
                  ADD TO VAULT
                </button>
                <div v-if="state.vaults != null" class="dropdown-menu" aria-labelledby="dropdownMenuButton">
                  <li v-for="v in state.vaults" :key="v.id" @click="addToVault(v.id)">
                    {{ v.name }}
                  </li>
                </div>
              </div>
              <button v-if="state.account.id == state.activeKeep.creatorId" class="remove-button" @click="deleteFromVaultKeep(state.activeKeep.vaultKeepId)">
                remove from vault
              </button>
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
import $ from 'jquery'
import { vaultsService } from '../services/VaultsService'

export default {

  setup() {
    const state = reactive({
      activeKeep: computed(() => AppState.activeKeep),
      vaults: computed(() => AppState.profileVaults),
      // vaultKeep: computed(() => AppState.vaultKeeps),
      account: computed(() => AppState.account)
    })
    return {
      state,
      async addToVault(vid) {
        const kid = state.activeKeep.id
        $('#exampleModal').modal('toggle')
        await vaultsService.createVaultKeep(vid, kid)
        console.log(vid, kid)
      },
      async deleteFromVaultKeep(id) {
        await vaultsService.deleteFromVaultKeep(id)
        console.log(id)
      }
      // unsetActive() {
      //   AppState.activeKeep = {}
      // }

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
