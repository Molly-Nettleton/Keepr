import { reactive } from 'vue'

export const AppState = reactive({
  user: {},
  /** @type {import('./models/Account.js').Account} */
  account: {},

  /** @type {import('./models/Account.js').Account} */
  profiles: [],

  /** @type {import('./models/Account.js').Account | null} */
  activeProfile: null,

  /** @type {import('./models/Keep.js').Keep.js[]} */
  keeps: [],

  /** @type {import('./models/Vault.js').Vault.js[]} */
  vaults: [],

  /** @type {import('./models/Keep.js').Keep.js | null} */
  activeKeep: null,

  /** @type {import('./models/Vault.js').Vault.js | null} */
  activeVault: null,

  /** @type {import('./models/Vault.js').Vault.js[]} */
  accountVaults: [],
  /** @type {import('./models/Vault.js').Vault.js[]} */
  profilesVaults: [],
  /** @type {import('./models/Keep.js').Keep.js[]} */
  accountKeeps: [],
  /** @type {import('./models/Keep.js').Keep.js[]} */
  profilesKeeps: [],

  /** @type {import('./models/VaultKeep.js').VaultKeep.js[]} */
  vaultKeeps: [],
  /** @type {import('./models/VaultKeep.js').VaultKeep.js[]} */
  activeVaultKeep: [],
})
