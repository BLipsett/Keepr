import { AppState } from '../AppState'
import { logger } from '../utils/Logger'
import { api } from './AxiosService'

class VaultsService {
  async getVaultKeeps(id) {
    const res = await api.get('api/vaults/' + id + '/keeps')
    AppState.vaultKeeps = res.data
    logger.log(res.data, ' heres the keeps i hope')
  }

  async createVaultKeep(vid, kid) {
    const keepData = {}
    keepData.vaultId = vid
    keepData.keepId = kid
    const res = await api.post('api/vaultKeeps', keepData)
    AppState.vaultKeeps = res.data
    logger.log(res)
  }

  async createVault(vaultData) {
    try {
      const res = await api.post('api/vaults', vaultData)
      AppState.keeps = res.data
    } catch (error) {
      logger.error('unable to create')
    }
  }

  // async getVaults() {
  //   const res = await api.get('api/vaults')
  // }
}

export const vaultsService = new VaultsService()
