import { reactive } from 'vue'

// NOTE AppState is a reactive object to contain app level data
export const AppState = reactive({
  user: {},
  account: {},
  keeps: [],
  activeKeep: null,
  activeVault: null,
  profile: null,
  profileKeeps: [],
  profileVaults: [],
  vaultKeeps: []
})
