namespace KeeperFINAL.Controllers;

[ApiController]
[Route("api/[controller]")]

public class KeepsController : ControllerBase
{
  private readonly VaultsService _vs;
  private readonly KeepsService _ks;
  private readonly Auth0Provider _ape;
  // 🦧

  public KeepsController(VaultsService vs, KeepsService ks, Auth0Provider ape)
  {
    _vs = vs;
    _ks = ks;
    _ape = ape;
  }


  [HttpPost]
  [Authorize]
  public async Task<ActionResult<Keep>> CreateKeep([FromBody] Keep keepData)
  {
    try
    {
      var userInfo = await _ape.GetUserInfoAsync<Account>(HttpContext);
      if (userInfo == null || userInfo.Id == null)
      {
        throw new Exception("Unauthorized.");
      }
      keepData.CreatorId = userInfo.Id;
      Keep createdKeep = _ks.CreateKeep(keepData);
      createdKeep.Creator = userInfo;
      return Ok(createdKeep);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpGet]
  public ActionResult<List<Keep>> GetAllKeeps()
  {
    try
    {
      List<Keep> keeps = _ks.GetAllKeeps();
      return Ok(keeps);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }


  [HttpGet("{keepId}")]
  public ActionResult<Keep> GetKeepById(int keepId)
  {
    try
    {
      Keep keep = _ks.GetKeepById(keepId);
      return Ok(keep);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpPut("{keepId}")]
  [Authorize]
  public async Task<ActionResult<Keep>> EditKeep([FromBody] Keep keepData, int keepId)
  {
    try
    {
      Account userInfo = await _ape.GetUserInfoAsync<Account>(HttpContext);
      keepData.Id = keepId;
      keepData.Creator = userInfo;
      keepData.CreatorId = userInfo.Id;
      Keep keep = _ks.EditKeep(keepData, userInfo?.Id);
      return Ok(keep);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }


  [HttpDelete("{keepId}")]
  [Authorize]
  public async Task<ActionResult<string>> DeleteKeep(int keepId)
  {
    try
    {
      Account userInfo = await _ape.GetUserInfoAsync<Account>(HttpContext);
      _ks.DeleteKeep(keepId, userInfo?.Id);
      return Ok("Keep deleted.");
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

}