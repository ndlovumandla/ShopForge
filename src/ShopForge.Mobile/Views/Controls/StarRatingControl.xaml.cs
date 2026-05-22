namespace ShopForge.Mobile.Views.Controls;

public partial class StarRatingControl : ContentView
{
    public static readonly BindableProperty RatingProperty =
        BindableProperty.Create(nameof(Rating), typeof(double), typeof(StarRatingControl), 0.0);

    public double Rating
    {
        get => (double)GetValue(RatingProperty);
        set => SetValue(RatingProperty, value);
    }

    public StarRatingControl()
    {
        InitializeComponent();
    }
}