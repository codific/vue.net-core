using GSK.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using System;

namespace Entities.Identity
{
    public class User : IdentityUser<Guid>, IEntityBase
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
