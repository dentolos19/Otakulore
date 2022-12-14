using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;

namespace Otakulore.Utilities;

public partial class AccumulableCollection<TObject> : ObservableCollection<TObject>
{

    public int AccumulationIndex { get; private set; }
    public Func<int, Task<(bool, IList<TObject>)>>? AccumulationFunc;
    public bool HasMoreItems { get; private set; } = true;

    [RelayCommand]
    private async Task Accumulate()
    {
        if (!HasMoreItems || AccumulationFunc is null)
            return;
        var (hasMoreItems, items) = await AccumulationFunc(++AccumulationIndex);
        HasMoreItems = hasMoreItems;
        foreach (var item in items)
            Add(item);
    }

}