<template>
  <div class="modal fade" id="keepModal" tabindex="-1" aria-labelledby="keepModalLabel" aria-hidden="true">
    <div class="modal-dialog">
      <div class="modal-content">
        <div class="modal-header">
          <h5 class="modal-title" id="keepModalLabel">
            Modal title
          </h5>
          <button type="button" class="close" data-dismiss="modal" aria-label="Close">
            <span aria-hidden="true">&times;</span>
          </button>
        </div>
        <div class="modal-body">
          <form @submit.prevent="createKeep">
            <div class="form-group">
              <label for="title">Title</label>
              <input v-model="state.keepInfo.name" type="text" class="form-control" id="title" aria-describedby="title">
            </div>
            <div class="form-group">
              <label for="imageURL">ImageUrl</label>
              <input v-model="state.keepInfo.img" type="text" class="form-control" id="imageURL" aria-describedby="imageURL">
            </div>
            <div class="form-group">
              <label for="description">Description</label>
              <textarea v-model="state.keepInfo.description" class="form-control" id="description" rows="5"></textarea>
            </div>
            <button type="submit" class="btn btn-primary">
              Create
            </button>
          </form>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import $ from 'jquery'
import { reactive } from '@vue/reactivity'
import { keepsService } from '../services/KeepsService'
export default {
  setup() {
    const state = reactive({
      keepInfo: {}
    })
    return {
      state,
      async createKeep() {
        try {
          await keepsService.createKeep(state.keepInfo)
          $('#keepModal').modal('toggle')
          state.keepInfo = {}
          console.log(state.keepInfo)
        } catch (error) {
          Notification.toast(error)
        }
      }
    }
  }
}
</script>

<style>

</style>
