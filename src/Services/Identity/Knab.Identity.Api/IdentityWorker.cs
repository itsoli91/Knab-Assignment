using Knab.Identity.Api.Infrastructure.Persistence.Context;
using OpenIddict.Abstractions;

namespace Knab.Identity.Api;

public class IdentityWorker : IHostedService
{
    private readonly IServiceProvider _serviceProvider;

    public IdentityWorker(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<IdentityContext>();
        await context.Database.EnsureCreatedAsync(cancellationToken);

        var manager = scope.ServiceProvider.GetRequiredService<IOpenIddictApplicationManager>();

        if (await manager.FindByClientIdAsync("knab-spa", cancellationToken) == null)
            await manager.CreateAsync(new OpenIddictApplicationDescriptor
            {
                ClientId = "knab-spa",
                ConsentType = OpenIddictConstants.ConsentTypes.Explicit,
                DisplayName = "knab spa client",
                Type = OpenIddictConstants.ClientTypes.Public,
                PostLogoutRedirectUris =
                {
                    new Uri("http://localhost:3000/signout-callback-oidc")
                },
                RedirectUris =
                {
                    //new Uri("https://oidcdebugger.com/debug"),
                    new Uri("http://localhost:3000")
                },
                Permissions =
                {
                    OpenIddictConstants.Permissions.Endpoints.Authorization,
                    OpenIddictConstants.Permissions.Endpoints.Logout,
                    OpenIddictConstants.Permissions.Endpoints.Token,
                    OpenIddictConstants.Permissions.GrantTypes.AuthorizationCode,
                    OpenIddictConstants.Permissions.GrantTypes.RefreshToken,
                    OpenIddictConstants.Permissions.ResponseTypes.Code,
                    OpenIddictConstants.Permissions.Scopes.Email,
                    OpenIddictConstants.Permissions.Scopes.Profile,
                    OpenIddictConstants.Permissions.Scopes.Roles
                },
                Requirements =
                {
                    OpenIddictConstants.Requirements.Features.ProofKeyForCodeExchange
                }
            }, cancellationToken);
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}