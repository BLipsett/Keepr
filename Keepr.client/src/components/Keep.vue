<template>
  <div class="card keepCard" @click="setActiveKeep(keep)" data-toggle="modal" data-target="#exampleModal">
    <img class="keepImg" :src="keep.img">
    <div class="card-img-overlay">
      <p>{{ keep.name }}</p>
      <img class="profPic" :src="keep.creator.picture" @click="getProfile(keep.creatorId)" />
    </div>
  </div>
  <div v-if="state.activeKeep">
    <KeepModal :keep="state.activeKeep" />
  </div>
</template>

<script>
import { reactive } from '@vue/reactivity'
import { profilesService } from '../services/ProfilesService'
import { AppState } from '../AppState'
import { router } from '../router'
import $ from 'jquery'

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
        $('#exampleModal').modal('toggle')
        console.log(AppState.activeKeep)
      },
      async getProfile(id) {
        await profilesService.getProfile(id)
        $('#exampleModal').modal('toggle')
        router.push({ name: 'Profiles', params: { id } })

        console.log('get profile', id)
      }

    }
  }

}
</script>

<style scoped>
.keepCard {
max-width: 100%;
}

.keepImg {
 max-width: 100%;
 object-fit: cover;
}

.profPic {
  height: 3rem;
  width: 3rem;
  border-radius: 50%;
}
</style>
