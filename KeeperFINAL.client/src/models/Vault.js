import { Account } from "./Account.js"
export class Vault {
  constructor(data) {
    this.id = data.id
    this.creatorId = data.creatorId
    this.creator = new Account(data.creator)
    // this.profile = data.profile
    this.createdAt = data.createdAt
    this.updatedAt = data.updatedAt
    this.name = data.name
    this.description = data.description
    this.img = data.img
    this.keepCount = data.keepCount
    this.viewCount = data.views
    this.isPrivate = data.isPrivate
  }
}