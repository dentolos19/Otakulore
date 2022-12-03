namespace Otakulore.Helpers;

public class AttachModelAttribute : Attribute
{

    public Type? Type { get; }

    public AttachModelAttribute(Type? type = null)
    {
        Type = type;
    }

}