namespace Otakulore.Models;

public class FilterItemModel
{

    public string Glyph { get; }
    public string Text { get; }
    public object Data { get; }

    public FilterItemModel(string glyph, string text, object data)
    {
        Glyph = glyph;
        Text = text;
        Data = data;
    }

}