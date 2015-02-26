using System;
using System.Collections.Generic;
using System.Net;
using EventStore.ClientAPI;
using EventStore.ClientAPI.SystemData;

namespace TaskManager.Infrastructure
{
    public interface IEventStoreConnectionFactory
    {
        IEventStoreConnection Create(string connectionName);
    }

    public class EventStoreConnectionFactory : IEventStoreConnectionFactory
    {
        private static readonly Dictionary<string, IEventStoreConnection> connections = 
            new Dictionary<string, IEventStoreConnection>();

        public IEventStoreConnection Create(string connectionName)
        {
            if (connections.ContainsKey(connectionName) == false)
            {
                var settings = ConnectionSettings
                    .Create()
                    .SetReconnectionDelayTo(TimeSpan.FromSeconds(2))
                    .KeepReconnecting()
                    .KeepRetrying()
                    .SetDefaultUserCredentials(new UserCredentials("admin", "changeit"));
                var connection = EventStoreConnection.Create(settings, new IPEndPoint(IPAddress.Loopback, 1113), connectionName);
                connection.ConnectAsync().Wait();
                connections.Add(connectionName, connection);
            }
            return connections[connectionName];
        }
    }
}