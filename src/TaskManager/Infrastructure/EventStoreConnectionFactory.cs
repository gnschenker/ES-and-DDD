using System;
using System.Net;
using EventStore.ClientAPI;
using EventStore.ClientAPI.SystemData;

namespace TaskManager.Infrastructure
{
    public class EventStoreConnectionFactory
    {
        private static IEventStoreConnection connection;

        public IEventStoreConnection GetOrCreate()
        {
            if (connection == null)
            {
                var settings = ConnectionSettings
                    .Create()
                    .SetReconnectionDelayTo(TimeSpan.FromSeconds(2))
                    .KeepReconnecting()
                    .KeepRetrying()
                    .SetDefaultUserCredentials(new UserCredentials("admin", "changeit"));
                connection = EventStoreConnection.Create(settings, new IPEndPoint(IPAddress.Loopback, 1113), "TaskManager");
                connection.ConnectAsync().Wait();
            }
            return connection;
        }
    }
}