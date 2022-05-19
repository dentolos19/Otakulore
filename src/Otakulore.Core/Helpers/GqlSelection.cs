namespace Otakulore.Core.Helpers;

public class GqlSelection
{

    public string Name { get; }
    public IList<GqlSelection> Selections { get; } = new List<GqlSelection>();
    public IDictionary<string, object?> Parameters { get; } = new Dictionary<string, object?>();

    public GqlSelection(string name, IList<GqlSelection>? selections = null, IDictionary<string, object?>? parameters = null)
    {
        Name = name;
        if (selections != null)
            Selections = selections;
        if (parameters != null)
            Parameters = parameters;
    }

}