<template>
  <div class="container-fluid">
    <div class="row">
      <div class="card-columns">
        <Keep v-for="k in state.keeps" :key="k.id" :keep="k" />
      </div>
    </div>
  </div>
  <!-- Button trigger modal -->
  <button type="button" class="btn btn-primary" data-toggle="modal" data-target="#exampleModal">
    Launch demo modal
  </button>

  <!-- Modal -->
  <div class="modal fade" id="exampleModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
    <div class="modal-dialog">
      <div class="modal-content">
        <div class="modal-header">
          <h5 class="modal-title" id="exampleModalLabel">
            Modal title
          </h5>
          <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
        </div>
        <div class="modal-body">
          ...
        </div>
        <div class="modal-footer">
          <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">
            Close
          </button>
          <button type="button" class="btn btn-primary">
            Save changes
          </button>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { reactive } from '@vue/reactivity'
import { computed, watchEffect } from '@vue/runtime-core'
import { AppState } from '../AppState'
import { keepsService } from '../services/KeepsService'

export default {
  setup() {
    const state = reactive({
      keeps: computed(() => AppState.keeps)
    })
    watchEffect(async() => keepsService.getKeeps())
    return {
      state
    }
  }
}

</script>

<style scoped lang="scss">
@media (min-width: 576px){
  .card-columns {
    column-count: 4;
    column-gap: 1.25rem;
    orphans: 1;
    widows: 1;
  }
}

@media (max-width: 576px)
{
.card-columns {
    column-count: 3;
    column-gap: 1.25rem;
    orphans: 1;
    widows: 1;
}
}
</style>
