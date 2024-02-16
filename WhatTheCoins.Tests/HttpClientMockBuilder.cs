using System.Net;
using Moq;
using Moq.Protected;

namespace WhatTheCoins.Tests;

public class HttpClientMockBuilder
{
    public const string Any = "*";
    private static readonly HttpRequestMessage RequestAny = new(HttpMethod.Get, Any); 
    private Dictionary<HttpRequestMessage, HttpResponseMessage> _messages = new();
    private Mock<HttpMessageHandler> _messageHandler = new();
    
    public HttpClientMockBuilder AddMessage(string request, string response)
    {
        var httpRequest = new HttpRequestMessage(HttpMethod.Get, request);
        var httpResponse = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent(response)
        };
        UpdateMessageHandlerMock(request == Any);
        return this;
    }

    private void UpdateMessageHandlerMock(bool any = false)
    {
        _messageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync((HttpRequestMessage m) => _messages[any ? m : RequestAny]);
    }

    public HttpClient Build() => new(_messageHandler.Object);
}