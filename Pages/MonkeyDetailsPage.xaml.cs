namespace MonkeyFinder.Pages;

public partial class MonkeyDetailsPage : ContentPage
{
    public MonkeyDetailsPage(MonkeyDetailsViewModel viewModel)
    {
        InitializeComponent();

        BindingContext = viewModel;
    }
}