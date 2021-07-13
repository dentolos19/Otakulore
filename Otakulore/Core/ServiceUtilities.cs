using System;
using System.Collections.Generic;
using System.Reflection;
using Otakulore.Core.Services.Anime;
using Otakulore.Core.Services.Manga;

namespace Otakulore.Core
{

    public class ServiceUtilities
    {

        public static IAnimeProvider[] GetAnimeProviders()
        {
            var providerList = new List<IAnimeProvider>();
            foreach (var type in Assembly.GetExecutingAssembly().GetTypes())
            {
                if (type.GetInterface(nameof(IAnimeProvider)) == null)
                    continue;
                var provider = (IAnimeProvider)Activator.CreateInstance(type);
                providerList.Add(provider);
            }
            return providerList.ToArray();
        }

        public static IMangaProvider[] GetMangaProviders()
        {
            var providerList = new List<IMangaProvider>();
            foreach (var type in Assembly.GetExecutingAssembly().GetTypes())
            {
                if (type.GetInterface(nameof(IMangaProvider)) == null)
                    continue;
                var provider = (IMangaProvider)Activator.CreateInstance(type);
                providerList.Add(provider);
            }
            return providerList.ToArray();
        }

    }

}