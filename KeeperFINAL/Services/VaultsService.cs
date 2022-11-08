namespace KeeperFINAL.Services;

public class VaultsService
{
  private readonly VaultsRepository _vr;
  private readonly KeepsRepository _kr;

  private readonly VaultKeepsRepository _vkr;

  public VaultsService(VaultsRepository vr, KeepsRepository kr, VaultKeepsRepository vkr)
  {
    _vr = vr;
    _kr = kr;
    _vkr = vkr;
  }

  internal Vault CreateVault(Vault vaultData)
  {
    return _vr.CreateVault(vaultData);
  }

  internal Vault GetVaultById(int vaultId, string userId)
  {
    Vault foundVault = _vr.GetVaultById(vaultId);
    if (foundVault == null)
    {
      throw new Exception("Vault not found");
    }
    if (foundVault.IsPrivate == true)
    {
      if (foundVault.CreatorId != userId)
      {
        throw new Exception("Bad ID.");
      }
    }
    return foundVault;
  }

  internal Vault EditVault(Vault vaultData, string accountId)
  {
    Vault original = GetVaultById(vaultData.Id, accountId);
    if (original.CreatorId != accountId)
    {
      throw new Exception("Unauthorized.");
    }
    original.Name = vaultData.Name ?? original.Name;
    original.Description = vaultData.Description ?? original.Description;
    original.Img = vaultData.Img ?? original.Img;
    original.IsPrivate = vaultData.IsPrivate ?? original.IsPrivate;

    Vault vault = _vr.EditVault(original);
    return vault;
  }

  internal void DeleteVault(int vaultId, string userId)
  {
    Vault vault = GetVaultById(vaultId, userId);
    if (vault.CreatorId != userId)
    {
      throw new Exception("Bad ID.");
    }
    _vr.DeleteVault(vaultId);
  }

  internal List<KeptKeep> GetKeepsByVaultId(int vaultId, string accountId)
  {
    Vault vault = GetVaultById(vaultId, accountId);
    List<KeptKeep> keeps = _vkr.GetKeepsByVaultId(vaultId);
    return _vkr.GetKeepsByVaultId(vaultId);
  }

}