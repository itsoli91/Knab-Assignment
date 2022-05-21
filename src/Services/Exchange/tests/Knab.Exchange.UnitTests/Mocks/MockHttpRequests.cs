using System.Net;
using Knab.Exchange.Domain.Exchange;
using Knab.Exchange.Infrastructure.Repositories.CryptoPriceRepository.External;
using Knab.Exchange.Infrastructure.Repositories.ExchangeRateRepository.External;

namespace Knab.Exchange.UnitTests.Mocks
{
    public static class MockHttpRequests
    {
        public static Mock<ICoinMarketCapApi> CoinMarketCapApiBadRequestMock()
        {
            var mockHttpClientWrapper = new Mock<ICoinMarketCapApi>();
            mockHttpClientWrapper.Setup(w => w.GetLatestQuotes(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.BadRequest));

            return mockHttpClientWrapper;
        }

        public static Mock<IExchangeRatesApi> ExchangeRatesApiEmptyMock()
        {
            var mockHttpClientWrapper = new Mock<IExchangeRatesApi>();
            mockHttpClientWrapper.Setup(w => w.GetLatest(It.IsAny<string[]>(), It.IsAny<string>()))
                .ReturnsAsync(new RatesDto());

            return mockHttpClientWrapper;
        }

        public static Mock<ICoinMarketCapApi> CoinMarketCapOkMock(string symbol, double outputPrice)
        {
            symbol = symbol.ToUpper();
            var mockHttpClientWrapper = new Mock<ICoinMarketCapApi>();
            var httpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(
                    @$"{{""status"":{{""timestamp"":""2021 - 02 - 21T19: 34:53.703Z"",""error_code"":0,""error_message"":null,""elapsed"":33,""credit_count"":1,""notice"":null}},""data"":{{""{symbol}"":{{""id"":1,""name"":""Bitcoin"",""symbol"":""{symbol}"",""slug"":""bitcoin"",""num_market_pairs"":9715,""date_added"":""2013 - 04 - 28T00: 00:00.000Z"",""tags"":[""mineable"",""pow"",""sha - 256"",""store - of - value"",""state - channels"",""coinbase - ventures - portfolio"",""three - arrows - capital - portfolio"",""polychain - capital - portfolio""],""max_supply"":21000000,""circulating_supply"":18634987,""total_supply"":18634987,""is_active"":1,""platform"":null,""cmc_rank"":1,""is_fiat"":0,""last_updated"":""2021 - 02 - 21T19: 33:02.000Z"",""quote"":{{""USD"":{{""price"":{outputPrice},""volume_24h"":57413332130.00193,""percent_change_1h"":0.99715411,""percent_change_24h"":2.06562598,""percent_change_7d"":19.86497157,""percent_change_30d"":78.89905885,""market_cap"":1085721307479.0636,""last_updated"":""2021 - 02 - 21T19: 33:02.000Z""}}}}}}}}}}")
            };
            mockHttpClientWrapper.Setup(w => w.GetLatestQuotes(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(httpResponseMessage);

            return mockHttpClientWrapper;
        }

        public static Mock<IExchangeRatesApi> ExchangeRatesApiOkMock(ExchangeRates outputRates)
        {
            var mockHttpClientWrapper = new Mock<IExchangeRatesApi>();
            var rateDto = new RatesDto(true, 1653070863, "USD", "2022-05-20",
                new Rates(1.1, 1.2, 1.3, 1.4));
            mockHttpClientWrapper.Setup(w => w.GetLatest(It.IsAny<string[]>(), It.IsAny<string>()))
                .ReturnsAsync(rateDto);

            return mockHttpClientWrapper;
        }
    }
}