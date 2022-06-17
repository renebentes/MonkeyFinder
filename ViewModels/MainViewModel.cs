namespace MonkeyFinder.ViewModels;

public partial class MainViewModel : BaseViewModel
{
    private readonly IConnectivity _connectivity;
    private readonly MonkeyService _monkeyService;
    private readonly IGeolocation _geolocation;

    public MainViewModel(MonkeyService monkeyService, IConnectivity connectivity, IGeolocation geolocation)
    {
        Title = "Monkey Finder";
        _monkeyService = monkeyService;
        _connectivity = connectivity;
        _geolocation = geolocation;
    }

    public ObservableCollection<Monkey> Monkeys { get; } = new();

    [RelayCommand]
    private async Task GetMonkeysAsync()
    {
        if (IsBusy)
        {
            return;
        }

        try
        {
            if (_connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await Shell.Current.DisplayAlert("No connectivity!", "Please check internet and try again!", "Ok");
                return;
            }

            IsBusy = true;

            if (Monkeys.Count != 0)
            {
                Monkeys.Clear();
            }

            var monkeys = await _monkeyService.GetMonkeys();

            foreach (var monkey in monkeys)
            {
                Monkeys.Add(monkey);
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            await Shell.Current.DisplayAlert("ERROR!", "Unable to get monkeys!", "Ok");
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    private async Task GetClosestMonkeyAsync()
    {
        if (IsBusy || Monkeys.Count == 0)
        {
            return;
        }

        try
        {
            var location = await _geolocation.GetLastKnownLocationAsync();

            if (location is null)
            {
                location = await _geolocation.GetLocationAsync(new GeolocationRequest
                {
                    DesiredAccuracy = GeolocationAccuracy.Medium,
                    Timeout = TimeSpan.FromSeconds(30)
                });
            }

            var closestMonkey = Monkeys
                .OrderBy(m => location.CalculateDistance(new Location(m.Latitude, m.Longitude), DistanceUnits.Kilometers))
                .FirstOrDefault();

            await Shell.Current.DisplayAlert("Closest Monkey!", $"{closestMonkey.Name} in {closestMonkey.Location}", "Ok");
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            await Shell.Current.DisplayAlert("ERROR!", "Unable to guery location!", "Ok");
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    private async Task GoToDetailsAsync(Monkey monkey)
    {
        if (monkey is null)
        {
            return;
        }

        var parameters = new Dictionary<string, object>
        {
            {nameof(Monkey), monkey }
        };

        await Shell.Current.GoToAsync(nameof(MonkeyDetailsPage), true, parameters);
    }
}