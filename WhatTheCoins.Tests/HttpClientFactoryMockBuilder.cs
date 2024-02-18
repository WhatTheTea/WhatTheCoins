using System.Net;
using Moq;
using Moq.Protected;

namespace WhatTheCoins.Tests;

public class HttpClientFactoryMockBuilder
{
    public const string Any = "*";
    private readonly Mock<HttpMessageHandler> _messageHandler = new();
    private readonly Dictionary<string, HttpResponseMessage> _messages = new();

    public HttpClientFactoryMockBuilder AddMessage(string request, string response)
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
            _messages[m.RequestUri!.ToString()];
        var anyMessage = (HttpRequestMessage m, CancellationToken _) => _messages[Any];

        _messageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(any ? anyMessage : specificMessage);
    }

    public IHttpClientFactory Build()
    {
        var mock = new Mock<IHttpClientFactory>();
        mock.Setup(f => f.CreateClient(It.IsAny<string>()))
            .Returns(new HttpClient(_messageHandler.Object));
        return mock.Object;
    }
}