import { AppState } from "../AppState.js"
import { Account } from "../models/Account.js"
import { Keep } from "../models/Keep.js"
import { Vault } from "../models/Vault.js"
import { api } from "./AxiosService.js"

class ProfilesService {
  async getProfileById(id) {
    const res = await api.get(`api/profiles/${id}`)
    AppState.activeProfile = new Account(res.data)
  }

  async getProfilesKeeps(id) {
    const res = await api.get(`api/profiles/${id}/keeps`);
    // console.log(res.data)
    AppState.profilesKeeps = res.data.map((k) => new Keep(k));
    // console.log(AppState.profilesKeeps)
  }

  async getProfilesVaults(id) {
    const res = await api.get(`api/profiles/${id}/vaults`)
    console.log(res.data)
    AppState.profilesVaults = res.data.map((v) => new Vault(v))
    console.log(AppState.profilesVaults)
  }
}
export const profilesService = new ProfilesService