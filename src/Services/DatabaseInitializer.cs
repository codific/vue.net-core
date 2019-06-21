using Core.DataAccess;
using Entities.Identity;
using Entities.Permissions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Services;
using System.Linq;
using System.Threading.Tasks;

namespace Services
{
    public class DatabaseInitializer : IDatabaseInitializer
    {
        private readonly EntityContext context;
        private readonly UserManager<User> userManager;
        private readonly IIdentityService identityService;
        public DatabaseInitializer(EntityContext context, UserManager<User> userManager, IIdentityService identityService)
        {
            this.userManager = userManager;
            this.context = context;
            this.identityService = identityService;
        }

        public async Task SeedAsync()
        {
            await this.context.Database.MigrateAsync().ConfigureAwait(false);

            if (!await this.context.Roles.AnyAsync())
            {
                await EnsureRoleAsync(Roles.Admin, "Admin", ApplicationPermissions.GetAllPermissionValues());
                await EnsureRoleAsync(Roles.User, "User", new string[] { });
            }

            if (!await this.context.Users.AnyAsync())
            {
                await CreateUserAsync("admin@codific.com", "admin", "AdminFirstName", "AdminLastName", new string[] { Roles.Admin });
                await CreateUserAsync("user1@codific.com", "admin", "User1FirstName", "User1LastName", new string[] { Roles.User });
                await CreateUserAsync("user2@codific.com", "admin", "User2FirstName", "User2LastName", new string[] { Roles.User });
            }
        }

        private async Task EnsureRoleAsync(string roleName, string description, string[] claims)
        {
            if (!(await this.context.Roles.Where(x => x.Name == roleName).AnyAsync()))
            {
                Role role = new Role { Name = roleName, Description = description };
                var result = await this.identityService.CreateRoleAsync(role, claims);
            }
        }

        private async Task<User> CreateUserAsync(string email, string password, string firstName, string lastName, string[] roles)
        {
            User user = new User
            {
                UserName = email,
                Email = email,
                FirstName = firstName,
                LastName = lastName,
                EmailConfirmed = true
            };

            var result = await this.userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                await this.userManager.AddToRolesAsync(user, roles);
            }
            return user;
        }
    }
}
