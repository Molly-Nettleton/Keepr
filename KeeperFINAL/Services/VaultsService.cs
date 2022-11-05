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
    if(foundVault == null)
    {
      throw new Exception("Vault not found");
    }
    return foundVault;
  }
}