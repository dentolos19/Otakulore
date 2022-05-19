using System.Runtime.Serialization;

namespace Otakulore.Core.Helpers;

public enum GqlType
{

    [EnumMember(Value = "query")] Query,
    [EnumMember(Value = "mutation")] Mutation

}