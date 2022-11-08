namespace KeeperFINAL.Controllers;

[ApiController]
[Route("api/[controller]")]

public class VaultKeepsController : ControllerBase
{
  private readonly VaultsService _vs;
  private readonly KeepsService _ks;
  private readonly VaultKeepsService _vks;
  private readonly Auth0Provider _ape;

  public VaultKeepsController(VaultsService vs, KeepsService ks, VaultKeepsService vks, Auth0Provider ape)
  {
    _vs = vs;
    _ks = ks;
    _vks = vks;
    _ape = ape;
  }
  // 🦧

 [Authorize]
  [HttpPost]
  public async Task<ActionResult<VaultKeep>> CreateVaultKeep([FromBody] VaultKeep vaultKeepData)
  {
    try
    {
      var userInfo = await _ape.GetUserInfoAsync<Account>(HttpContext);
      vaultKeepData.CreatorId = userInfo.Id;
      VaultKeep vaultKeep = _vks.CreateVaultKeep(vaultKeepData, userInfo?.Id);
      return Ok(vaultKeep);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }


  [HttpDelete("{vaultKeepId}")]
  [Authorize]
  public async Task<ActionResult<string>> DeleteVaultKeep(int vaultKeepId)
  {
    try
    {
      Account userInfo = await _ape.GetUserInfoAsync<Account>(HttpContext);
      _vks.DeleteVaultKeep(vaultKeepId, userInfo.Id);
      return Ok("VaultKeep deleted");
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }


}