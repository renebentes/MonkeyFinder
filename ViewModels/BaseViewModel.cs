namespace MonkeyFinder.ViewModels;

public abstract partial class BaseViewModel : ObservableObject
{
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsNotBusy))]
    private readonly bool _IsBusy;

    [ObservableProperty]
    private readonly string _title;

    public bool IsNotBusy => !IsBusy;
}