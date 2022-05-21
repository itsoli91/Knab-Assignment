using Knab.Identity.Api.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Knab.Identity.Api.Infrastructure.Persistence.Context;

public class IdentityContext : IdentityDbContext<KnabUser>


{
    public IdentityContext(DbContextOptions<IdentityContext> options)
        : base(options)
    {
    }
}