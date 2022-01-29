namespace Otakulore.Core.Helpers;

public class GqlSelection
{

    public string Name { get; }
    public GqlSelection[]? Selections { get; }
    public Dictionary<string, object> Parameters { get; } = new();

    public GqlSelection(string name, GqlSelection[]? selections = null)
    {
        Name = name;
        Selections = selections;
    }

}