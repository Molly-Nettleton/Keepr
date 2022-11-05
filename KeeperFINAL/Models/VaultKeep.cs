namespace KeeperFINAL.Models;
using KeeperFINAL.Interfaces;
using System;

public class VaultKeep : IDbItem<int>
{
  public int Id { get; set; }
  public int VaultId { get; set; }
  public int KeepId { get; set; }
  public string CreatorId { get; set; }
  public DateTime CreatedAt { get; set; }
  public DateTime UpdatedAt { get; set; }
}

// NOTE vaultkeep/keptkeep = fave/faverecipe
public class KeptKeep : Keep
{
  public int KeepId { get; set; }
  public int VaultId { get; set; }
  public int VaultKeepId { get; set; }
  public string VaultKeepCreatorId { get; set; }
}