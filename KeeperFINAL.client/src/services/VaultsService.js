import { AppState } from "../AppState.js";
import { Keep } from "../models/Keep.js";
import { Vault } from "../models/Vault.js";
import { api } from "./AxiosService.js";


class VaultsService {

  async getVaultById(id) {
    const res = await api.get(`api/vaults/${id}`)
    AppState.activeVault = new Vault(res.data)
  }

  async getKeepsByVaultId(id) {
    const res = await api.get(`api/vaults/${id}/keeps`)
    console.log(res.data);
    AppState.profilesKeeps = res.data.map(k => new Keep(k))
    console.log(AppState.profilesKeeps);
  }

  async createVault(vaultData) {
    const res = await api.post("api/vaults", vaultData);
    console.log("[createVault]", res.data);
    const newVault = new Vault(res.data);
    AppState.vaults = [newVault, ...AppState.vaults]
    AppState.accountVaults = [newVault, ...AppState.accountVaults]
    console.log(AppState.accountVaults)
  }
}
export const vaultsService = new VaultsService();