using System;
using Otakulore.Core;

namespace Otakulore.Models;

public class ProviderItemModel
{

    public Uri ImageUrl { get; }
    public string Name { get; }
    public IProvider Data { get; }

    public ProviderItemModel(IProvider data)
    {
        ImageUrl = new Uri(data.ImageUrl);
        Name = data.Name;
        Data = data;
    }

}