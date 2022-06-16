namespace MonkeyFinder.ViewModels;

public partial class MainViewModel : BaseViewModel
{
    private readonly IConnectivity _connectivity;
    private readonly MonkeyService _monkeyService;

    public MainViewModel(MonkeyService monkeyService, IConnectivity connectivity)
    {
        Title = "Monkey Finder";
        _monkeyService = monkeyService;
        _connectivity = connectivity;
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