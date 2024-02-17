using System.Net;
using Moq;
using Moq.Protected;

namespace WhatTheCoins.Tests;

public class HttpClientMockBuilder
{
    public const string Any = "*";
    private Dictionary<string, HttpResponseMessage> _messages = new();
    private Mock<HttpMessageHandler> _messageHandler = new();
    
    public HttpClientMockBuilder AddMessage(string request, string response)
    {
        var httpResponse = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent(response)
        };
        _messages.Add(request, httpResponse);
        UpdateMessageHandlerMock(request == Any);
        return this;
    }

    private void UpdateMessageHandlerMock(bool any)
    {
        var specificMessage = (HttpRequestMessage m, CancellationToken c) =>
            _messages[m.Content!.ReadAsStringAsync(c).Result];
        var anyMessage = (HttpRequestMessage m, CancellationToken _) => _messages[Any];
        
        _messageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(any ? anyMessage : specificMessage);
    }

    public HttpClient Build() => new(_messageHandler.Object);
}