using Microsoft.UI.Xaml.Media;

namespace Otakulore.Core;

public interface IProvider
{

    ImageSource Image { get; }
    string Name { get; }
    string Url { get; }

}