namespace Otakulore.Models;

public class BaseItemModel<TData>
{

    public string Name { get; }
    public TData Data { get; }

    public BaseItemModel(string name, TData data)
    {
        Name = name;
        Data = data;
    }

}