using Microsoft.AspNetCore.Identity;
using SISAPI.Domain.Entities;
using SISAPI.Domain.Entities.Common;
using System;
using System.Threading.Tasks;

namespace SISAPI.Persistence
{
    public class IdentityInitilazer
    {
        private UserIdentityModel _model;

        public IdentityInitilazer(UserIdentityModel model)
        {
            _model = model;
        }

        public async Task<bool> CreateUser(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            if(await userManager.FindByNameAsync(_model.User.UserName) == null)
            {
                await userManager.CreateAsync(_model.User, _model.Password);

                IdentityRole role = new IdentityRole() { Name = _model.Role };

                if (roleManager.FindByNameAsync(_model.Role).Result == null)
                {
                    await roleManager.CreateAsync(role);
                }

                await userManager.AddToRoleAsync(_model.User, role.Name);

                return true;
            }
            return false;
        }
    }
}
