using GSK.DAL;
using Microsoft.AspNetCore.Http;
using System;
using System.IdentityModel.Tokens.Jwt;

namespace Core.DataAccess
{
    public class ApplicationUnitOfWork : UnitOfWork<EntityContext>
    {
        public ApplicationUnitOfWork(EntityContext context, IServiceProvider serviceProvider, IHttpContextAccessor httpAccessor) : base(context, serviceProvider)
        {
            if (httpAccessor.HttpContext != null)
            {
                string userId = httpAccessor.HttpContext?.User.FindFirst(JwtRegisteredClaimNames.Jti)?.Value?.Trim();
                if (!string.IsNullOrEmpty(userId))
                {
                    Guid parsedUserId = Guid.Empty;
                    Guid.TryParse(userId, out parsedUserId);
                    this.context.CurrentUserId = parsedUserId;
                }
            }
        }
    }
}
