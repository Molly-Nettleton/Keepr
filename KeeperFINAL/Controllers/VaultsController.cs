namespace KeeperFINAL.Controllers;

[ApiController]
[Route("api/[controller]")]

public class VaultsController : ControllerBase
{
  private readonly VaultsService _vs;
  private readonly KeepsService _ks;
  private readonly Auth0Provider _ape;

  public VaultsController(VaultsService vs, KeepsService ks, Auth0Provider ape)
  {
    _vs = vs;
    _ks = ks;
    _ape = ape;
  }

  [Authorize]
  [HttpPost]
  public async Task<ActionResult<Vault>> CreateVault([FromBody] Vault vaultData)
  {
    try
    {
      var userInfo = await _ape.GetUserInfoAsync<Account>(HttpContext);
      vaultData.CreatorId = userInfo.Id;
      if (userInfo == null || userInfo.Id == null)
      {
        throw new Exception("Bad token.");
      }
      Vault vault = _vs.CreateVault(vaultData);
      vault.Creator = userInfo;
      return Ok(vault);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }


  [HttpGet("{id}")]
  public ActionResult<Vault> GetVaultById(int id)
  {
    try
    {
      var foundVault = _vs.GetVaultById(id);
      return Ok(foundVault);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }


}