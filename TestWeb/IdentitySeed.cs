using Microsoft.AspNetCore.Identity;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace TestWeb
{
  public static class CustomRoleTypes
  {
    public const string Administrator = "Administrator";
  }

  public static class CustomClaimTypes
  {
    public const string Permission = "Permission";
  }

  public static class PermissionClaimValues
  {
    public const string HasAdministrationRights = "HasAdministrationRights";
  }

  public class IdentitySeed
  {
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public IdentitySeed(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
    {
      _userManager = userManager;
      _roleManager = roleManager;
    }

    public async Task SeedRolesAndRoleClaims()
    {
      IdentityRole adminRole = await _roleManager.FindByNameAsync(CustomRoleTypes.Administrator);
      if (adminRole == null)
      {
        adminRole = new IdentityRole(CustomRoleTypes.Administrator);
        await _roleManager.CreateAsync(adminRole);
        await _roleManager.AddClaimAsync(adminRole, new Claim(CustomClaimTypes.Permission, PermissionClaimValues.HasAdministrationRights));
      }
    }

    public async Task SeedUsers()
    {
      IdentityUser adminUser = await _userManager.FindByNameAsync("admin");
      if (adminUser == null)
      {
        adminUser = new IdentityUser
        {
          UserName = "admin",
        };
        await _userManager.CreateAsync(adminUser, "P@ssw0rd");
        string roleName = CustomRoleTypes.Administrator;
        await _userManager.AddToRoleAsync(adminUser, roleName);
        await _userManager.AddClaimAsync(adminUser, new Claim(ClaimTypes.NameIdentifier, adminUser.Id.ToString()));
        await _userManager.AddClaimAsync(adminUser, new Claim(ClaimTypes.Role, roleName));
        await _userManager.AddClaimAsync(adminUser, new Claim(ClaimTypes.Name, adminUser.UserName));
      }
    }
  }
}
