using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace TestWeb.Services
{
  public class RegisterRequest
  {
    public string Username { get; set; }
    public string Password { get; set; }
  }

  public class LoginRequest
  {
    public string Username { get; set; }
    public string Password { get; set; }
  }

  public class UserService
  {
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public UserService(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager)
    {
      _userManager = userManager;
      _signInManager = signInManager;
      _roleManager = roleManager;
    }

    public async Task<IdentityResult> RegisterAsync(RegisterRequest request)
    {
      IdentityUser user = new IdentityUser
      {
        Id = Guid.NewGuid().ToString(),
        UserName = request.Username
      };
      var result = await _userManager.CreateAsync(user, request.Password);
      if (result.Succeeded)
      {
        await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.NameIdentifier, user.Id));
        await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Name, user.UserName));
      }
      return result;
    }

    public async Task<SignInResult> LoginAsync(LoginRequest request)
    {
      IdentityUser user = await _userManager.FindByNameAsync(request.Username);

      if (user == null)
      {
        return null;
      }

      return await _signInManager.PasswordSignInAsync(user, request.Password, false, false);
    }

    public async Task<string> GetUserTokenAsync(IdentityUser user)
    {
      IList<Claim> claims = await _userManager.GetClaimsAsync(user);
      IList<string> roleNames = await _userManager.GetRolesAsync(user);
      foreach (string roleName in roleNames)
      {
        IdentityRole role = await _roleManager.FindByNameAsync(roleName);
        foreach (Claim claim in await _roleManager.GetClaimsAsync(role))
        {
          claims.Add(claim);
        }
      }

      JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
      SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Startup.Configuration["JWT:SecurityKey"]));
      SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
      SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
      {
        Issuer = Startup.Configuration["JWT:Issuer"],
        Audience = Startup.Configuration["JWT:Audience"],
        Subject = new ClaimsIdentity(claims),
        Expires = DateTime.Now.AddMinutes(Convert.ToDouble(Startup.Configuration["JWT:MinutesLifetime"])),
        SigningCredentials = creds
      };
      SecurityToken token = handler.CreateToken(tokenDescriptor);

      return handler.WriteToken(token);
    }

    public async Task<IdentityUser> FindByNameAsync(string username)
    {
      return await _userManager.FindByNameAsync(username);
    }

    public async Task<IdentityUser> GetCurrentUserAsync(ClaimsPrincipal user)
    {
      string id = user.FindFirstValue(ClaimTypes.NameIdentifier);
      return await _userManager.FindByIdAsync(id);
    }

    public async Task<IList<string>> GetRolesAsync(IdentityUser user)
    {
      return await _userManager.GetRolesAsync(user);
    }
  }
}
