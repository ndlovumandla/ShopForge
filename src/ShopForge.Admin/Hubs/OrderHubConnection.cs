using Microsoft.AspNetCore.SignalR.Client;

namespace ShopForge.Admin.Hubs;

public class OrderHubConnection : IAsyncDisposable
{
    private HubConnection? _connection;
    public event Action<string>? OnNewOrder;
    public event Action<int, string>? OnOrderStatusChanged;

    public async Task StartAsync(string apiBaseUrl, string token)
    {
        _connection = new HubConnectionBuilder()
            .WithUrl($"{apiBaseUrl}/hubs/orders", options =>
            {
                options.AccessTokenProvider = () => Task.FromResult<string?>(token);
            })
            .WithAutomaticReconnect()
            .Build();

        _connection.On<string>("NewOrder", orderNumber => OnNewOrder?.Invoke(orderNumber));
        _connection.On<int, string>("OrderStatusChanged", (orderId, status) => OnOrderStatusChanged?.Invoke(orderId, status));

        try { await _connection.StartAsync(); }
        catch { /* Hub connection failure should not crash admin */ }
    }

    public async Task StopAsync()
    {
        if (_connection != null) await _connection.StopAsync();
    }

    public async ValueTask DisposeAsync()
    {
        if (_connection != null) await _connection.DisposeAsync();
    }
}
