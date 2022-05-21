using Knab.Exchange.Infrastructure.Repositories.CryptoPriceRepository;
using Knab.Exchange.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Knab.Exchange.Api.Controllers;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
[ApiVersion("1.0")]
public class CryptoController : ControllerBase
{
    private readonly ICryptoPriceService _cryptoPriceService;

    public CryptoController(ICryptoPriceService cryptoPriceService)
    {
        _cryptoPriceService = cryptoPriceService;
    }

    [HttpGet("Prices/Latest")]
    [Authorize]
    public async Task<CryptoPriceList> GetCryptoPriceList(string symbol)
    {
        return await _cryptoPriceService.GetPrices(symbol);
    }
}