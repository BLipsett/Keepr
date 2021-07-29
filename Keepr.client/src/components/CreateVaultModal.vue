<template>
  <div class="modal fade" id="vaultModal" tabindex="-1" aria-labelledby="vaultModalLabel" aria-hidden="true">
    <div class="modal-dialog">
      <div class="modal-content">
        <div class="modal-header">
          <h5 class="modal-title" id="vaultModalLabel">
            Create Vault
          </h5>
          <button type="button" class="close" data-dismiss="modal" aria-label="Close">
            <span aria-hidden="true">&times;</span>
          </button>
        </div>
        <div class="modal-body">
          <form @submit.prevent="createVault">
            <div class="form-group">
              <label for="title">Title</label>
              <input v-model="state.vaultInfo.name" type="text" class="form-control" id="title" aria-describedby="title">
            </div>
            <div class="form-group">
              <label for="imageURL">ImageUrl</label>
              <input v-model="state.vaultInfo.img" type="text" class="form-control" id="imageURL" aria-describedby="imageURL">
            </div>
            <div class="form-group form-check">
              <input type="checkbox" class="form-check-input" id="private" v-model="state.vaultInfo.isPrivate">
              <label class="form-check-label" for="private">Make Vault Private</label>
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
import { vaultsService } from '../services/VaultsService'
export default {
  setup() {
    const state = reactive({
      vaultInfo: {}
    })
    return {
      state,
      async createVault() {
        try {
          await vaultsService.createVault(state.vaultInfo)
          $('#vaultModal').modal('toggle')
          state.vaultInfo = {}
          console.log(state.vaultInfo)
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
