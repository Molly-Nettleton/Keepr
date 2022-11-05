namespace KeeperFINAL.Services;

public class VaultsService
{
  private readonly VaultsRepository _vr;
  private readonly KeepsRepository _kr;

  public VaultsService(VaultsRepository vr, KeepsRepository kr)
  {
    _vr = vr;
    _kr = kr;
  }

  internal Vault CreateVault(Vault vaultData)
  {
    return _vr.CreateVault(vaultData);
  }

  internal Vault GetVaultById(int vaultId)
  {
    var foundVault = _vr.GetVaultById(vaultId);
    if (foundVault == null)
    {
      throw new Exception("Vault not found");
    }
    if (foundVault.IsPrivate == true)
    {
      throw new Exception("This vault is private.");
    }
    return foundVault;
  }

  internal Vault EditVault(Vault vaultData, string accountId)
  {
    if (vaultData.CreatorId != accountId)
    {
      throw new Exception("Unauthorized.");
    }
    Vault original = GetVaultById(vaultData.Id);
    original.Name = vaultData.Name ?? original.Name;
    original.Description = vaultData.Description ?? original.Description;
    original.Img = vaultData.Img ?? original.Img;

    Vault vault = _vr.EditVault(original);
    return vault;
  }

  internal void DeleteVault(int vaultId, string userId)
  {
    Vault vault = GetVaultById(vaultId);
    if (vault.CreatorId != userId)
    {
      throw new Exception("Bad ID.");
    }
    _vr.DeleteVault(vaultId);
  }
}