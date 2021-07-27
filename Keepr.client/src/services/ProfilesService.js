import { AppState } from '../AppState'
import { logger } from '../utils/Logger'
import { api } from './AxiosService'

class ProfilesService {
  async getProfile(id) {
    const res = await api.get('api/profiles/' + id)
    AppState.profile = res.data
    logger.log(res.data)
  }

  async getKeepsByProfile(id) {
    const res = await api.get('api/profiles/' + id + '/keeps')
    AppState.profileKeeps = res.data
    logger.log(res.data)
  }

  async getVaultsByProfile(id) {
    const res = await api.get('api/profiles/' + id + '/vaults')
    AppState.profileVaults = res.data
    logger.log(res.data)
  }
}
export const profilesService = new ProfilesService()
