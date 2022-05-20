using System.Windows.Input;
using AniListNet.Objects;
using Otakulore.Pages;

namespace Otakulore.Components;

public class ViewMediaCommand : ICommand
{

    public event EventHandler CanExecuteChanged;

    public bool CanExecute(object parameter)
    {
        return parameter is Media;
    }

    public async void Execute(object parameter)
    {
        if (parameter is Media media)
            await Shell.Current.GoToAsync(nameof(DetailsPage), new Dictionary<string, object>
            {
                { "id", media.Id }
            });
    }

}