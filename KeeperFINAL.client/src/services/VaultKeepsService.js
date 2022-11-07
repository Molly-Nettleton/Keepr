import { AppState } from "../AppState.js";
import { VaultKeep } from "../models/VaultKeep.js";
import { api } from "./AxiosService.js";

class VaultKeepsService {

  async createVaultKeep(keepId) {
    const res = await api.post("api/vaultKeeps", keepId)
    console.log(res.data);
    let newVaultKeep = new VaultKeep(res.data)

    AppState.vaultKeeps = [...AppState.vaultKeeps, newVaultKeep]

  }

}
export const vaultKeepsService = new VaultKeepsService();