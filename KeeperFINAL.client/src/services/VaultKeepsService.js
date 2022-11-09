import { AppState } from "../AppState.js";
import { VaultKeep } from "../models/VaultKeep.js";
import { api } from "./AxiosService.js";

class VaultKeepsService {

  async createVaultKeep(data) {
    const res = await api.post("api/vaultKeeps/", data)
    console.log(res.data);
    AppState.activeKeep.keptCount++
    let newVaultKeep = new VaultKeep(res.data)
    AppState.vaultKeeps = [...AppState.vaultKeeps, newVaultKeep]
    console.log("hello??")

    console.log(activeKeep)
  }

  async removeFromVault(id) {
    const res = await api.delete(`api/vaultkeeps/${id}`)
    AppState.vaultKeeps = AppState.keeps.filter(k => k.id !== id)
    AppState.vaultKeeps = AppState.vaultKeeps.filter(k => k.id !== id)
    console.log(res.data)
  }
}
export const vaultKeepsService = new VaultKeepsService();