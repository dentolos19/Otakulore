namespace Otakulore.Helpers;

public class PageServiceAttribute : Attribute
{

    public PageServiceType Type { get; }
    public Type? ModelType { get; }

    public PageServiceAttribute(PageServiceType type = PageServiceType.Transient, Type? modelType = null)
    {
        Type = type;
        ModelType = modelType;
    }

}