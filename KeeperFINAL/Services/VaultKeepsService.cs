namespace KeeperFINAL.Services;

public class VaultKeepsService
{
  private readonly VaultsRepository _vr;
  private readonly KeepsRepository _kr;
  private readonly VaultKeepsRepository _vkr;

  private readonly VaultsService _vs;

  public VaultKeepsService(VaultsRepository vr, KeepsRepository kr, VaultKeepsRepository vkr, VaultsService vs)
  {
    _vr = vr;
    _kr = kr;
    _vkr = vkr;
    _vs = vs;
  }

  public VaultKeep CreateVaultKeep(VaultKeep vaultKeepData, string userId)
  {
    Vault vault = _vs.GetVaultById(vaultKeepData.VaultId, vaultKeepData.CreatorId);
    if (vault.CreatorId != userId)
    {
      throw new Exception("Bad ID.");
    }
    return _vkr.CreateVaultKeep(vaultKeepData);
  }

  public VaultKeep GetVaultKeepById(int vaultKeepId)
  {
    VaultKeep vaultKeep = _vkr.GetVaultKeepById(vaultKeepId);
    if (vaultKeep == null)
    {
      throw new Exception("Bad ID.");
    }
    return vaultKeep;
  }

  internal List<KeptKeep> GetKeepsByVaultId(int vaultId)
  {
    return _vkr.GetKeepsByVaultId(vaultId);
  }

  internal void DeleteVaultKeep(int vaultKeepId, string accountId)
  {
    var vk = GetVaultKeepById(vaultKeepId);
    if (vk.CreatorId != accountId)
    {
      throw new Exception("Unauthorized.");
    }
    _vkr.DeleteVaultKeep(vaultKeepId);
  }
}