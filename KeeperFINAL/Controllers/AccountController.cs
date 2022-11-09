namespace KeeperFINAL.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{
  private readonly AccountService _accountService;
  private readonly Auth0Provider _auth0Provider;

  public AccountController(AccountService accountService, Auth0Provider auth0Provider)
  {
    _accountService = accountService;
    _auth0Provider = auth0Provider;
  }

  [HttpGet]
  [Authorize]
  public async Task<ActionResult<Account>> Get()
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      return Ok(_accountService.GetOrCreateProfile(userInfo));
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }


  [HttpGet("{vaults}")]
  public async Task<ActionResult<List<Vault>>> GetMyVaults()
  {
    try
    {
      var userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      List<Vault> vault = _accountService.GetMyVaults(userInfo.Id);
      return Ok(vault);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }


  [HttpGet("keeps")]
  public async Task<ActionResult<List<Keep>>> GetAccountKeeps()
  {
    try
    {
      var userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      List<Keep> accountKeeps = _accountService.GetMyKeeps(userInfo?.Id);
      return Ok(accountKeeps);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpPut]
  [Authorize]
  public async Task<ActionResult<Account>> EditAccountInfo([FromBody] Account accountData)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      Account info = _accountService.Edit(accountData, userInfo.Email);
      return Ok(info);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }



}
