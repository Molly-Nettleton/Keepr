namespace KeeperFINAL.Services;

public class ProfilesService
{
  private readonly ProfilesRepository _pr;
  private readonly VaultsRepository _vr;
  private readonly KeepsRepository _kr;


  public ProfilesService(ProfilesRepository pr, VaultsRepository vr, KeepsRepository kr)
  {
    _pr = pr;
    _vr = vr;
    _kr = kr;
  }

  internal Profile GetProfileById(string profileId)
  {
    Profile profile = _pr.GetProfileById(profileId);
    if (profile == null)
    {
      throw new Exception("User not found");
    }
    return profile;
  }

  internal List<Vault> GetVaultsByProfileId(string profileId)
  {
    return _pr.GetVaultsByProfileId(profileId);
  }

  internal List<Keep> GetKeepsByProfileId(string profileId)
  {
    return _pr.GetKeepsByProfileId(profileId);
  }

}