namespace MonkeyFinder.ViewModels;

[QueryProperty(nameof(Monkey), nameof(Monkey))]
public partial class MonkeyDetailsViewModel : BaseViewModel
{
    [ObservableProperty]
    private Monkey monkey;

    private readonly IMap _map;

    public MonkeyDetailsViewModel(IMap map) => _map = map;

    [RelayCommand]
    private async Task OpenMapAsync()
    {
        try
        {
            await _map.OpenAsync(
                new Location(Monkey.Latitude, Monkey.Longitude),
                new MapLaunchOptions { Name = Monkey.Name, NavigationMode = NavigationMode.None }
);

        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            await Shell.Current.DisplayAlert("ERROR!", "Unable to open map!", "Ok");
        }
    }
}
