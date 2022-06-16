namespace MonkeyFinder.ViewModels;

public partial class MainViewModel : BaseViewModel
{
    private readonly MonkeyService _monkeyService;

    public MainViewModel(MonkeyService monkeyService)
    {
        Title = "Monkey Finder";
        _monkeyService = monkeyService;
    }

    public ObservableCollection<Monkey> Monkeys { get; } = new();

    [RelayCommand]
    public async Task GetMonkeysAsync()
    {
        if (IsBusy)
        {
            return;
        }

        try
        {
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
}