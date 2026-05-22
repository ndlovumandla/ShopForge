using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ShopForge.Mobile.Services;
using ShopForge.Shared.DTOs.Reviews;

namespace ShopForge.Mobile.ViewModels;

[QueryProperty(nameof(ProductId), "productId")]
[QueryProperty(nameof(OrderId), "orderId")]
public partial class WriteReviewViewModel : BaseViewModel
{
    private readonly IApiService _api;
    [ObservableProperty] private int _productId;
    [ObservableProperty] private int _orderId;
    [ObservableProperty] private int _rating = 5;
    [ObservableProperty] private string? _title;
    [ObservableProperty] private string? _body;
    [ObservableProperty] private bool _submitted;

    public WriteReviewViewModel(IApiService api) { _api = api; Title = "Write Review"; }

    [RelayCommand]
    private async Task SubmitAsync()
    {
        await ExecuteSafelyAsync(async () =>
        {
            var result = await _api.CreateReviewAsync(ProductId, new CreateReviewRequest { Rating = Rating, Title = Title, Body = Body, OrderId = OrderId > 0 ? OrderId : null });
            if (result?.Success == true) { Submitted = true; await Task.Delay(1500); await Shell.Current.GoToAsync(".."); }
            else ErrorMessage = result?.Message ?? "Failed to submit review.";
        });
    }

    [RelayCommand]
    private void SetRating(int r) => Rating = r;
}
