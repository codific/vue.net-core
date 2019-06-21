using Entities.Identity;
using Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Tests.ServicesTests
{
    public class IdentityServiceTests : AbstractTestsConstructor
    {
        [Fact]
        public async Task RegisterUser_ValidData_CreateAnUser()
        {
            var identityService = GetService<IIdentityService>();

            var result = await identityService.CreateUserAsync(TestUser, "admin");

            Assert.True(result.Item1);
            Assert.NotNull(result.Item2);
        }

        private User TestUser
        {
            get
            {
                return new User
                {
                    UserName = "test_user@codific.com",
                    Email = "test_user@codific.com",
                    FirstName = "FirstName",
                    LastName = "LastName",
                    EmailConfirmed = true
                };
            }
        }
    }
}
