namespace KeeperFINAL.Controllers;
[ApiController]
[Route("api/[controller]")]
public class ProfilesController : ControllerBase
{
  private readonly VaultsService _vs;
  private readonly KeepsService _ks;
  private readonly ProfilesService _ps;
  private readonly Auth0Provider _ape;
  // 🦧

  public ProfilesController(ProfilesService ps, VaultsService vs, KeepsService ks, Auth0Provider ape)
  {
    _ps = ps;
    _vs = vs;
    _ks = ks;
    _ape = ape;
  }

  [HttpGet("{profileId}")]
  public ActionResult<Profile> GetProfileById(string profileId)
  {
    try
    {
      Profile profile = _ps.GetProfileById(profileId);
      return Ok(profile);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }


  [HttpGet("{profileId}/vaults")]
  public ActionResult<List<Vault>> GetVaultsByProfileId(string profileId)
  {
    try
    {
      List<Vault> profileVault = _ps.GetVaultsByProfileId(profileId);
      return Ok(profileVault);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpGet("{profileId}/keeps")]
  public ActionResult<List<Keep>> GetKeepsByProfileId(string profileId)
  {
    try
    {
      List<Keep> profileKeep = _ps.GetKeepsByProfileId(profileId);
      return Ok(profileKeep);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }
}