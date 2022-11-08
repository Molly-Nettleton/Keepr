namespace KeeperFINAL.Services;

public class KeepsService
{
  private readonly VaultsRepository _vr;
  private readonly KeepsRepository _kr;

  public KeepsService(VaultsRepository vr, KeepsRepository kr)
  {
    _vr = vr;
    _kr = kr;
  }

  internal Keep CreateKeep(Keep keepData)
  {
    return _kr.CreateKeep(keepData);
  }

  internal List<Keep> GetAllKeeps()
  {
    return _kr.GetAllKeeps();
  }

  internal Keep GetKeepById(int keepId, string userId)
  {
    var keep = _kr.GetKeepById(keepId);
    if (keep == null)
    {
      throw new Exception("Bad ID.");
    }
    if(keep.CreatorId != userId)
    {
      keep.Views++;
      UpdateKeep(keep);
    }
    return keep;
  }

  public void UpdateKeep(Keep k)
  {
    _kr.EditKeep(k);
  }

  internal Keep EditKeep(Keep keepData, string accountId)
  {
    var original = GetKeepById(keepData.Id, accountId);
    if (original.CreatorId != accountId)
    {
      throw new Exception("Unauthorized.");
    }
    // Keep original = GetKeepById(keepData.Id);
    original.Name = keepData.Name ?? original.Name;
    original.Description = keepData.Description ?? original.Description;
    original.Img = keepData.Img ?? original.Img;

    Keep updatedKeep = _kr.EditKeep(original);
    return updatedKeep;
  }

  internal void DeleteKeep(int keepId, string userId)
  {
    var keep = GetKeepById(keepId, userId);
    if (keep.CreatorId != userId)
    {
      throw new Exception("Bad ID.");
    }
    _kr.DeleteKeep(keepId);
  }
}