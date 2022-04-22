using System.Windows.Input;
using Otakulore.Models;

namespace Otakulore.Components;

public class ViewMediaCommand : ICommand
{

    public bool CanExecute(object? parameter)
    {
        return true;
    }

    public async void Execute(object? parameter)
    {
        if (parameter is MediaItemModel item)
            await Shell.Current.GoToAsync("details?id=" + item.Id);
    }

    public event EventHandler? CanExecuteChanged;

}