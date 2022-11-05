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

  internal Keep GetKeepById(int keepId)
  {
    var keep = _kr.GetKeepById(keepId);
    if (keep == null)
    {
      throw new Exception("Bad ID.");
    }
    return keep;
  }

  internal Keep EditKeep(Keep keepData, string accountId)
  {
    if (keepData.CreatorId != accountId)
    {
      throw new Exception("Unauthorized.");
    }
    Keep original = GetKeepById(keepData.Id);
    original.Name = keepData.Name ?? original.Name;
    original.Description = keepData.Description ?? original.Description;
    original.Img = keepData.Img ?? original.Img;
    original.Views = keepData.Views;

    Keep keep = _kr.EditKeep(original);
    return keep;
  }

  internal void DeleteKeep(int keepId, string userId)
  {
    var keep = GetKeepById(keepId);
    if (keep.CreatorId != userId)
    {
      throw new Exception("Bad ID.");
    }
    _kr.DeleteKeep(keepId);
  }
}