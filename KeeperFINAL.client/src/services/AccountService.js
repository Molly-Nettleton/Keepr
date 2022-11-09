import { AppState } from '../AppState'
import { Account } from "../models/Account.js"
import { Keep } from "../models/Keep.js"
import { Vault } from "../models/Vault.js"
import { logger } from '../utils/Logger'
import { api } from './AxiosService'

class AccountService {
  async getAccount() {
    try {
      const res = await api.get('/account')
      AppState.account = res.data
    } catch (err) {
      logger.error('HAVE YOU STARTED YOUR SERVER YET???', err)
    }
  }

  async getMyKeeps() {
    const res = await api.get(`/account/keeps`)
    AppState.accountKeeps = res.data.map((k) => new Keep(k))
    console.log(AppState.accountKeeps)
  }

  async getMyVaults() {
    const res = await api.get(`/account/vaults`)
    AppState.accountVaults = res.data.map((v) => new Vault(v))
    console.log(AppState.accountVaults)
  }

  async editAccountInfo(accountData,) {
    const res = await api.put(`/account`, accountData)
    AppState.account = new Account(res.data)
  }
}

export const accountService = new AccountService()
