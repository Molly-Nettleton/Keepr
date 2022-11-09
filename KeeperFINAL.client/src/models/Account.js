export class Account {
  constructor(data) {
    this.id = data.id || data_id
    this.email = data.email
    this.name = data.name
    this.picture = data.picture
    this.coverImg = data.coverImg
  }
}
