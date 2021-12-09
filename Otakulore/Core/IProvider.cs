using System;

namespace Otakulore.Core;

public interface IProvider
{

    Uri ImageUrl { get; }
    string Name { get; }
    Uri Url { get; }

}