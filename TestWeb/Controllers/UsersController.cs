using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using TestWeb.Services;

namespace TestWeb.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class UsersController : ControllerBase
  {
    private readonly UserService _userService;

    public UsersController(UserService userService)
    {
      _userService = userService;
    }

    [HttpPost("Register")]
    public async Task<IActionResult> RegisterAsync([FromBody]RegisterRequest request)
    {
      IdentityResult result = await _userService.RegisterAsync(request);
      if (result.Succeeded)
      {
        return await LoginAsync(new LoginRequest
        {
          Username = request.Username,
          Password = request.Password
        });
      }
      return BadRequest(result.Errors);
    }

    [HttpPost("Login")]
    public async Task<IActionResult> LoginAsync([FromBody]LoginRequest request)
    {
      Microsoft.AspNetCore.Identity.SignInResult result = await _userService.LoginAsync(request);
      if (result.Succeeded)
      {
        IdentityUser user = await _userService.FindByNameAsync(request.Username);
        IList<string> roles = await _userService.GetRolesAsync(user);
        string token = await _userService.GetUserTokenAsync(user);
        return Ok(new
        {
          User = user,
          Roles = roles,
          Token = token
        });
      }
      return Unauthorized();
    }

    [Authorize]
    [HttpGet("WhoAmI")]
    public async Task<IActionResult> WhoAmIAsync()
    {
      IdentityUser user = await _userService.GetCurrentUserAsync(User);
      IList<string> roles = await _userService.GetRolesAsync(user);
      return Ok(new
      {
        User = user,
        Roles = roles
      });
    }

    [Authorize(PermissionClaimValues.HasAdministrationRights)]
    [HttpGet("IsAdmin")]
    public IActionResult IsAdmin()
    {
      return Ok();
    }
  }
}
