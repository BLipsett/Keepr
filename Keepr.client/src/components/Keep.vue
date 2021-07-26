<template>
  <div class="card keepCard" @click="popmodal" data-toggle="modal" data-target="#exampleModal">
    <p>{{ keep.name }}</p>
    <p>{{ keep.shares }}</p>
    <img class="keepImg" :src="keep.img" />
    <button type="button" class="btn btn-primary" data-toggle="modal" data-target="#exampleModal">
      Launch demo modal
    </button>
  </div>
  <KeepModal />
</template>

<script>
import $ from 'jquery'
import { keepsService } from '../services/KeepsService'
export default {
  props: {
    keep: { type: Object, required: true }
  },
  setup() {
    return {
      popmodal() {
        console.log('pop modal')
        keepsService.setActiveKeep(){
          
        }
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
