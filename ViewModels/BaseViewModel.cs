namespace MonkeyFinder.ViewModels;

public abstract partial class BaseViewModel : ObservableObject
{
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsNotBusy))]
    private bool _IsBusy;

    [ObservableProperty]
    private string _title;

    public bool IsNotBusy => !IsBusy;
}