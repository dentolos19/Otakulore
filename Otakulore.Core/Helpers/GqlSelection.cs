namespace Otakulore.Core.Helpers;

public class GqlSelection
{

    public string Name { get; }
    public ICollection<GqlSelection> Selections { get; } = new List<GqlSelection>();
    public IDictionary<string, object> Parameters { get; } = new Dictionary<string, object>();

    public GqlSelection(string name, ICollection<GqlSelection>? selections = null)
    {
        Name = name;
        Selections = selections;
    }

}