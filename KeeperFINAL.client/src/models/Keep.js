import { Account } from "./Account.js"
export class Keep {
  constructor(data) {
    this.id = data.id
    this.creatorId = data.creatorId
    this.creator = data.creator
    // this.profile = data.profile
    this.createdAt = data.createdAt
    this.updatedAt = data.updatedAt
    this.name = data.name
    this.description = data.description
    this.img = data.img
    this.keptCount = data.kept
    this.viewCount = data.views
    this.vaultId = data.vaultId
    this.vaultKeepId = data.vaultKeepId
  }
}