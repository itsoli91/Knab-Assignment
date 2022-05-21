using Knab.Exchange.Domain.Exchange;
using Knab.Exchange.Infrastructure.Repositories.CryptoPriceRepository;
using Knab.Exchange.Infrastructure.Services;
using Knab.Exchange.UnitTests.Mocks;

namespace Knab.Exchange.UnitTests.InfrastructureTests
{
    public class CryptoPriceServiceTests
    {
        [Fact]
        public async Task GetPrices_CalculatesCorrectly()
        {
            var symbol = "CRYPTO";
            var cryptoPrice = 100.0;
            var exchangeRates = new ExchangeRates(1, 1.1, 1.2, 1.3, 1.4);
            var resultPriceList = new CryptoPriceList(
                symbol,
                cryptoPrice * exchangeRates.Usd, cryptoPrice * exchangeRates.Eur,
                cryptoPrice * exchangeRates.Brl, cryptoPrice * exchangeRates.Gbp, cryptoPrice * exchangeRates.Aud);

            var mockCryptoPriceRepo = MockICryptoPriceRepository.ValidCryptoPriceRepository(cryptoPrice);
            var mockExchangeRateRepo = MockIExchangeRateRepository.ValidExchangeRatesRepository(exchangeRates);

            var provider = new CryptoPriceService(mockExchangeRateRepo.Object, mockCryptoPriceRepo.Object);
            var result = await provider.GetPrices(symbol);

            Assert.Equal(resultPriceList.Symbol, result.Symbol);
            Assert.Equal(resultPriceList.Usd, result.Usd);
            Assert.Equal(resultPriceList.Eur, result.Eur);
            Assert.Equal(resultPriceList.Brl, result.Brl);
            Assert.Equal(resultPriceList.Gbp, result.Gbp);
            Assert.Equal(resultPriceList.Aud, result.Aud);
        }
    }
}