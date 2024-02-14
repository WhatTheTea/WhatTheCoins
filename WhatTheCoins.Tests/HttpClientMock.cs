using System.Net;
using Moq;
using Moq.Protected;

namespace WhatTheCoins.Tests;

public static class HttpClientMock
{
    public static HttpClient MockHttpClient(string expectedResponse)
    {
        var mockMessageHandler = new Mock<HttpMessageHandler>();
        mockMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(expectedResponse)
            });
        return new HttpClient(mockMessageHandler.Object);
    }
}