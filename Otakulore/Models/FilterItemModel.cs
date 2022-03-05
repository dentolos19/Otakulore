namespace Otakulore.Models;

public class FilterItemModel
{

    public string Text { get; }
    public object Data { get; }

    public FilterItemModel(string text, object data)
    {
        Text = text;
        Data = data;
    }

}