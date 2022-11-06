import { reactive } from 'vue'

export const AppState = reactive({
  user: {},
  /** @type {import('./models/Account.js').Account} */
  account: {},

  /** @type {import('./models/Keep.js').Keep.js} */
  keeps: [],

  /** @type {import('./models/Vault.js').Vault.js} */
  vault: {},

  /** @type {import('./models/Keep.js').Keep.js | null} */
  activeKeep: null,
})
