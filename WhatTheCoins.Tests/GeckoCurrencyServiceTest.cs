using WhatTheCoins.API;

namespace WhatTheCoins.Tests;

[TestFixture]
[TestOf(typeof(GeckoCurrencyService))]
public class GeckoCurrencyServiceTest
{
    private ICurrencyService _service = new GeckoCurrencyService();

    private Currency _idealBtcCurrency = new(
        Id : "bitcoin",
        Symbol : "btc",
        PriceChange : -1182.5965131229532,
        Markets : new List<Market>()
        {
            new("usd", 48691)
        },
        Volume: 38938720460
    );
    [Test]
    public void GetByIdIdeal()
    {
        var data = _service.GetById("bitcoin");
        Assert.That(data, Is.EqualTo(_idealBtcCurrency));
    }
}