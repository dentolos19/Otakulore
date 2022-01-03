using System;

namespace Otakulore.Core;

public class MediaContent
{

    public string Name { get; init; }

    public Uri Url { get; set; }
    public bool IsUrlVideo { get; set; }

}