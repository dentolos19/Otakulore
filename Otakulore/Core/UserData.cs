using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Windows.Storage;
using Otakulore.Core.Services.Anime.Providers;

namespace Otakulore.Core
{

    public class UserData
    {
        
        public string DefaultAnimeProvider { get; set; } = new AnimeKisaProvider().Id;
        public List<string> FavoriteList { get; set; } = new List<string>();

        public void SaveData()
        {
            var type = GetType();
            var propertyList = type.GetProperties();
            var settings = ApplicationData.Current.LocalSettings;
            foreach (var property in propertyList) // TODO: fix userdata saving issues
                settings.Values[property.Name] = type.GetProperty(property.Name).GetValue(this);
        }

        public static UserData LoadData()
        {
            var type = typeof(UserData);
            var instance = Activator.CreateInstance(type);
            var settings = ApplicationData.Current.LocalSettings;
            foreach (var value in settings.Values)
                type.GetProperty(value.Key).SetValue(instance, value);
            return (UserData)instance;
        }

    }

}