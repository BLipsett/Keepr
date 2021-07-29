import { AppState } from '../AppState'
import { logger } from '../utils/Logger'
import { api } from './AxiosService'

class KeepsService {
  async getKeepById(id) {
    const res = await api.get('api/keeps/' + id)
    AppState.activeKeep = res.data
  }

  async getKeeps() {
    const res = await api.get('api/keeps')
    AppState.keeps = res.data
    logger.log(res.data)
  }

  async createKeep(keepData) {
    try {
      const res = await api.post('api/keeps', keepData)
      AppState.keeps = res.data
    } catch (error) {
      logger.error('unable to create')
    }
  }

  async deleteKeep(id) {
    try {
      const res = await api.delete('api/keeps/' + id)
      logger.log(res.data, ' deleted keep')
    } catch (error) {
      logger.error('unable to delete')
    }
  }
}

export const keepsService = new KeepsService()
