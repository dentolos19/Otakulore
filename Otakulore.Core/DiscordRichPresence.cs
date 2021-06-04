using System;
using DiscordRPC;

namespace Otakulore.Core
{

    public class DiscordRichPresence : IDisposable
    {
        
        private DiscordRpcClient? _client;

        public void InitializeRpc(string id)
        {
            _client = new DiscordRpcClient(id);
            _client.Initialize();
            SetInitialState();
        }

        public void SetInitialState()
        {
            _client?.SetPresence(new RichPresence());
        }

        public void SetWatchingState(string title, string episode)
        {
            _client?.SetPresence(new RichPresence
            {
                Details = title,
                State = episode,
            });
        }

        public void Dispose()
        {
            _client?.Dispose();
        }

    }

}