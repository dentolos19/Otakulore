namespace Otakulore.Helpers;

public class PageServiceAttribute : Attribute
{

    public PageServiceType Type { get; }
    public Type? ModelType { get; }

    public PageServiceAttribute(PageServiceType type, Type? modelType = null)
    {
        Type = type;
        ModelType = modelType;
    }

}