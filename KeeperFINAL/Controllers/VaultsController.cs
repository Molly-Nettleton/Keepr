namespace KeeperFINAL.Controllers;

[ApiController]
[Route("api/[controller]")]

public class VaultsController : ControllerBase
{
  private readonly VaultsService _vs;
  private readonly KeepsService _ks;
  private readonly VaultKeepsService _vks;
  private readonly Auth0Provider _ape;

  public VaultsController(VaultsService vs, KeepsService ks, VaultKeepsService vks, Auth0Provider ape)
  {
    _vs = vs;
    _ks = ks;
    _vks = vks;
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

  [HttpGet("{vaultId}")]
  public async Task<ActionResult<Vault>> GetVaultById(int vaultId)
  {
    try
    {
      Account userInfo = await _ape.GetUserInfoAsync<Account>(HttpContext);
      Vault vault = _vs.GetVaultById(vaultId);
      return Ok(vault);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpGet("{vaultId}/keeps")]
  public ActionResult<List<KeptKeep>> GetKeepsByVaultId(int vaultId)
  {
    try
    {
      List<KeptKeep> vaultKeeps = _vks.GetKeepsByVaultId(vaultId);
      return Ok(vaultKeeps);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }


  [HttpPut("{vaultId}")]
  [Authorize]
  public async Task<ActionResult<Vault>> EditVault([FromBody] Vault vaultData, int vaultId)
  {
    try
    {
      Account userInfo = await _ape.GetUserInfoAsync<Account>(HttpContext);
      vaultData.Id = vaultId;
      vaultData.Creator = userInfo;
      vaultData.CreatorId = userInfo.Id;
      Vault vault = _vs.EditVault(vaultData, userInfo?.Id);
      return Ok(vault);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }


  [HttpDelete("{vaultId}")]
  [Authorize]
  public async Task<ActionResult<string>> DeleteVault(int vaultId)
  {
    try
    {
      Account userInfo = await _ape.GetUserInfoAsync<Account>(HttpContext);
      _vs.DeleteVault(vaultId, userInfo?.Id);
      return Ok("Vault deleted");
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

}