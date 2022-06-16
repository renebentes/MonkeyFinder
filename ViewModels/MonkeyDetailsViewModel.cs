namespace MonkeyFinder.ViewModels;

[QueryProperty(nameof(Monkey), nameof(Monkey))]
public partial class MonkeyDetailsViewModel : BaseViewModel
{
    [ObservableProperty]
    private Monkey monkey;
}
