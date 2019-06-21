using GSK.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Identity
{
    public class Role : IdentityRole<Guid>, IEntityBase
    {
        [Column(TypeName = "text")]
        public string Description { get; set; }
    }
}
