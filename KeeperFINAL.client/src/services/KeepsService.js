import { AppState } from "../AppState.js";
import { Keep } from "../models/Keep.js";
import { api } from "./AxiosService.js";


class KeepsService {
  async getKeepDetails(keep) {

    await api.get(`api/keeps/${keep.id}`)
    // AppState.activeKeep = new Keep(res.data)
    AppState.activeKeep = keep
    if (AppState.activeKeep.creatorId != AppState.account.id) {
      AppState.activeKeep.viewCount++
    }
    console.log(AppState.activeKeep)
  }

  async getAllKeeps() {
    const res = await api.get('api/keeps')
    console.log(res.data);
    AppState.keeps = res.data.map(k => new Keep(k))
    console.log(AppState.keeps);
  }

  async createKeep(keepData) {
    const res = await api.post("api/keeps", keepData);
    console.log("[createKeep]", res.data);
    const newKeep = new Keep(res.data);
    AppState.keeps = [newKeep, ...AppState.keeps];
    AppState.accountKeeps = [newKeep, ...AppState.accountKeeps]
  }

  async removeKeep(id) {
    await api.delete(`api/keeps/${id}`);
    AppState.keeps = AppState.keeps.filter(k => k.id != id)
    AppState.accountKeeps = AppState.accountKeeps.filter(k => k.id != id)
  }
}
export const keepsService = new KeepsService();