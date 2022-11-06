import { Account } from "./Account.js"
export class VaultKeep {
  constructor(data) {
    this.id = data.id
    this.creatorId = data.creatorId
    this.keepId = data.keepId
    this.vaultId = data.vaultId
    this.creator = new Account(data.creator)
    this.createdAt = data.createdAt
    this.updatedAt = data.updatedAt
  }
}