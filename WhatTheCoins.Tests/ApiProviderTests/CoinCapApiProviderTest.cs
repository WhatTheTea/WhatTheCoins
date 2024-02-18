using WhatTheCoins.API;
using WhatTheCoins.API.ApiProviders;

namespace WhatTheCoins.Tests.ApiProviderTests;

[TestFixture(typeof(CoinCapApiProvider))]
public class CoinCapApiProviderTest<TApiProvider> : ApiProviderTest<TApiProvider> where TApiProvider : IApiProvider
{
    public override void SetUpFixture()
    {
        GetIdResponse = "{\"data\":{\"id\":\"bitcoin\",\"rank\":\"1\",\"symbol\":\"BTC\",\"name\":\"Bitcoin\",\"supply\":\"17193925.0000000000000000\",\"maxSupply\":\"21000000.0000000000000000\",\"marketCapUsd\":\"119179791817.6740161068269075\",\"volumeUsd24Hr\":\"33970260124\",\"priceUsd\":\"49418\",\"changePercent24Hr\":\"-2.72424\",\"vwap24Hr\":\"7175.0663247679233209\"},\"timestamp\":1533581098863}";
        GetCandlesResponse = "{\"data\":[{\"open\":\"43606\",\"high\":\"44176\",\"low\":\"43606\",\"close\":\"44165\",\"volume\":\"1818.1433015300000000\",\"period\":1707350400000},{\"open\":\"44318\",\"high\":\"44692\",\"low\":\"44318\",\"close\":\"44600\",\"volume\":\"1649.2534471200000000\",\"period\":1707364800000}],\"timestamp\":1533581190540}";
        SearchResponse = "{\"data\":[{\"id\":\"bitcoin\",\"rank\":\"1\",\"symbol\":\"BTC\",\"name\":\"Bitcoin\",\"supply\":\"19630712.0000000000000000\",\"maxSupply\":\"21000000.0000000000000000\",\"marketCapUsd\":\"1015179869704.2257117215909080\",\"volumeUsd24Hr\":\"10027512791.3218497429326016\",\"priceUsd\":\"51713.8588607599006965\",\"changePercent24Hr\":\"-0.9182717693863066\",\"vwap24Hr\":\"51507.1700390699688247\",\"explorer\":\"https://blockchain.info/\"},{\"id\":\"ethereum\",\"rank\":\"2\",\"symbol\":\"ETH\",\"name\":\"Ethereum\",\"supply\":\"120165121.6508740600000000\",\"maxSupply\":null,\"marketCapUsd\":\"335242495284.5000243764643503\",\"volumeUsd24Hr\":\"5078960158.0493288124647713\",\"priceUsd\":\"2789.8485906626761137\",\"changePercent24Hr\":\"-0.5047115768874633\",\"vwap24Hr\":\"2776.4520300861228291\",\"explorer\":\"https://etherscan.io/\"},{\"id\":\"tether\",\"rank\":\"3\",\"symbol\":\"USDT\",\"name\":\"Tether\",\"supply\":\"97598187376.1713300000000000\",\"maxSupply\":null,\"marketCapUsd\":\"97723109942.7373951400678916\",\"volumeUsd24Hr\":\"11031845393.9009602707660055\",\"priceUsd\":\"1.0012799681010937\",\"changePercent24Hr\":\"0.0285931393331406\",\"vwap24Hr\":\"1.0011489918434506\",\"explorer\":\"https://www.omniexplorer.info/asset/31\"},{\"id\":\"binancecoin\",\"rank\":\"4\",\"symbol\":\"BNB\",\"name\":\"BNB\",\"supply\":\"166801148.0000000000000000\",\"maxSupply\":\"166801148.0000000000000000\",\"marketCapUsd\":\"58889765698.2788020753834404\",\"volumeUsd24Hr\":\"316787018.5928638256497986\",\"priceUsd\":\"353.0537193801496023\",\"changePercent24Hr\":\"-2.2354469529530456\",\"vwap24Hr\":\"355.8295808669297486\",\"explorer\":\"https://etherscan.io/token/0xB8c77482e45F1F44dE1745F52C74426C631bDD52\"},{\"id\":\"solana\",\"rank\":\"5\",\"symbol\":\"SOL\",\"name\":\"Solana\",\"supply\":\"440599136.6489792000000000\",\"maxSupply\":null,\"marketCapUsd\":\"48001585538.6507167290236194\",\"volumeUsd24Hr\":\"422953296.1405285963118391\",\"priceUsd\":\"108.9461634076988357\",\"changePercent24Hr\":\"-1.1973829233030588\",\"vwap24Hr\":\"108.2494620784033420\",\"explorer\":\"https://explorer.solana.com/\"},{\"id\":\"ripple\",\"rank\":\"6\",\"symbol\":\"USDC\",\"name\":\"USDC\",\"supply\":\"28113943640.9067880000000000\",\"maxSupply\":null,\"marketCapUsd\":\"28138223573.5723662556981178\",\"volumeUsd24Hr\":\"545129145.0058853853186702\",\"priceUsd\":\"1.0008636259991021\",\"changePercent24Hr\":\"0.0594726238675354\",\"vwap24Hr\":\"1.0003818805579701\",\"explorer\":\"https://etherscan.io/token/0xa0b86991c6218b36c1d19d4a2e9eb0ce3606eb48\"},{\"id\":\"usd-coin\",\"rank\":\"7\",\"symbol\":\"XRP\",\"name\":\"XRP\",\"supply\":\"45404028640.0000000000000000\",\"maxSupply\":\"100000000000.0000000000000000\",\"marketCapUsd\":\"25150671245.5612782204236480\",\"volumeUsd24Hr\":\"350582282.8420757404462355\",\"priceUsd\":\"0.5539303889744282\",\"changePercent24Hr\":\"-1.8720931128344779\",\"vwap24Hr\":\"0.5557136203286471\",\"explorer\":\"https://xrpcharts.ripple.com/#/graph/\"},{\"id\":\"staked-ether\",\"rank\":\"8\",\"symbol\":\"ADA\",\"name\":\"Cardano\",\"supply\":\"35454873255.9400000000000000\",\"maxSupply\":\"45000000000.0000000000000000\",\"marketCapUsd\":\"21654196845.8866771491990180\",\"volumeUsd24Hr\":\"216460235.6715948774433662\",\"priceUsd\":\"0.6107537513833532\",\"changePercent24Hr\":\"2.0258687292711477\",\"vwap24Hr\":\"0.5895219482289102\",\"explorer\":\"https://cardanoexplorer.com/\"},{\"id\":\"cardano\",\"rank\":\"9\",\"symbol\":\"AVAX\",\"name\":\"Avalanche\",\"supply\":\"367515707.7170313600000000\",\"maxSupply\":\"720000000.0000000000000000\",\"marketCapUsd\":\"14633947101.2213593305206389\",\"volumeUsd24Hr\":\"130590303.9433432504906123\",\"priceUsd\":\"39.8185622925504010\",\"changePercent24Hr\":\"-1.1342571809195991\",\"vwap24Hr\":\"39.6997732494373525\",\"explorer\":\"https://avascan.info/\"},{\"id\":\"avalanche-2\",\"rank\":\"10\",\"symbol\":\"TRX\",\"name\":\"TRON\",\"supply\":\"88052120604.7119600000000000\",\"maxSupply\":null,\"marketCapUsd\":\"11977705826.4472642103020751\",\"volumeUsd24Hr\":\"172753904.9794594541753788\",\"priceUsd\":\"0.1360297258508763\",\"changePercent24Hr\":\"2.1379249497416398\",\"vwap24Hr\":\"0.1358390754659418\",\"explorer\":\"https://tronscan.org/#/\"}],\"timestamp\":1708214657279}";
        Top10Response = SearchResponse;
    }

    private const string GetExchangesResponse =
        "{\"data\":[{\"id\":\"united-states-dollar\",\"symbol\":\"USD\",\"currencySymbol\":\"$\",\"type\":\"fiat\",\"rateUsd\":\"1.0000000000000000\"}],\"timestamp\":1536347807471}";

    [Test]
    public override async Task GetByIdIdeal()
    {
        HttpClientFactory = new HttpClientFactoryMockBuilder()
            .AddMessage("https://api.coincap.io/v2/assets/bitcoin", GetIdResponse)
            .AddMessage("https://api.coincap.io/v2/rates", GetExchangesResponse)
            .Build();
        await base.GetByIdIdeal();
    }

    [Test]
    public override Task SearchByCode()
    {
        HttpClientFactory = new HttpClientFactoryMockBuilder()
            .AddMessage("https://api.coincap.io/v2/assets", SearchResponse)
            .AddMessage("https://api.coincap.io/v2/assets/bitcoin", GetIdResponse)
            .Build();
        return base.SearchByCode();
    }
}