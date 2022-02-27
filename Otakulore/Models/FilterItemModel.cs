using Microsoft.UI.Xaml.Controls;

namespace Otakulore.Models;

public class FilterItemModel
{

    public Symbol Symbol { get; }
    public string Text { get; }
    public object Data { get; }

    public FilterItemModel(Symbol symbol, string text, object data)
    {
        Symbol = symbol;
        Text = text;
        Data = data;
    }

}