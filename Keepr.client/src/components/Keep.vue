<template>
  <div>
    <div class="card keepCard" @click="setActiveKeep(keep.id)" data-toggle="modal" data-target="#exampleModal">
      <img class="keepBg"
           alt="keep image"
           :src="keep.img"
      >
      <div class="card-img-overlay d-flex">
        <div class="align-items-center">
          <p class="cardTitle">
            {{ keep.name }}
          </p>
        </div>
      </div>
      <img @click.stop
           class="profPic"
           title="profile image"
           alt="profile image"
           :src="keep.creator.picture"
           @click="getProfile(keep.creatorId)"
      />
    </div>
  </div>
</template>

<script>
import { reactive } from '@vue/reactivity'
import { profilesService } from '../services/ProfilesService'
import { AppState } from '../AppState'
import { router } from '../router'
import $ from 'jquery'
import { vaultsService } from '../services/VaultsService'
import { keepsService } from '../services/KeepsService'

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
      async setActiveKeep(id) {
        await keepsService.getKeepById(id)
        // AppState.activeKeep = keep
        $('#exampleModal').modal('toggle')
        console.log(AppState.activeKeep, 'active keeep')
      },
      async getProfile(id) {
        $('#exampleModal').modal('toggle')
        // $('#exampleModal').modal('toggle')
        // $('#exampleModal').modal('hide')
        // $('body').removeClass('modal-open')
        // $('.modal-backdrop').remove()
        await profilesService.getProfile(id)
        router.push({ name: 'Profiles', params: { id } })
        console.log('get profile', id)
      },
      async addToVault(kid) {
        const vid = 312
        vaultsService.createVaultKeep(vid, kid)
        console.log(vid, kid)
      }

    }
  }

}
</script>

<style scoped>
.keepCard {
max-width: 100%;
border-radius: 1.25rem;
cursor: pointer;
}

.keepBg {
 max-width: 100%;
 object-fit: cover;
border-radius: 1.25rem;

}

.profPic {
  height: 3rem;
  width: 3rem;
  border-radius: 50%;
  position: absolute;
  bottom: 20px;
  right: 20px;
}

.cardTitle {
  font-size: 1.75rem;
  color: whitesmoke;
}
</style>
