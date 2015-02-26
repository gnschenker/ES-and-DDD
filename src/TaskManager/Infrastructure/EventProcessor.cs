using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EventStore.ClientAPI;
using log4net;
using Newtonsoft.Json;

namespace TaskManager.Infrastructure
{
    public class EventProcessor
    {
        private readonly IEventStoreConnectionFactory eventStoreConnectionFactory;
        private readonly IEventDispatcher eventDispatcher;
        private readonly ILog logService;
        private IEventStoreConnection eventStoreConnection;
        private readonly IMongoDbEventPositionRepository mongoDbEventPositionRepository;

        public EventProcessor(IEventStoreConnectionFactory eventStoreConnectionFactory, IEventDispatcher eventDispatcher,
            IMongoDbEventPositionRepository mongoDbEventPositionRepository, ILog logService)
        {
            this.eventStoreConnectionFactory = eventStoreConnectionFactory;
            this.eventDispatcher = eventDispatcher;
            this.mongoDbEventPositionRepository = mongoDbEventPositionRepository;
            this.logService = logService;
        }

        public void Start(string connectionName)
        {
            eventStoreConnection = eventStoreConnectionFactory.Create(connectionName);
            var lastProcessedPosition = mongoDbEventPositionRepository.Get();
            eventStoreConnection.SubscribeToAllFrom(lastProcessedPosition, false, (subs, re) => DispatchEvent(re.ToResolvedEventWrapper()));
        }

        protected void DispatchEvent(IResolvedEvent resolvedEvent)
        {
            var eventType = resolvedEvent.EventType;

            Type type;
            if (CanDispatch(eventType, resolvedEvent.Metadata, out type))
            {
                var deserializedEvent = JsonConvert.DeserializeObject(Encoding.UTF8.GetString(resolvedEvent.Event), type);
                eventDispatcher.Dispatch(deserializedEvent);
            }
            try
            {
                mongoDbEventPositionRepository.Save(resolvedEvent.Position.Value);
            }
            catch (Exception e)
            {
                logService.Error("Cannot persist position of last processed event.", e);
            }
        }

        private bool CanDispatch(string eventType, byte[] metaData, out Type type)
        {
            type = null;
            if (eventType.StartsWith("$")) return false;

            var eventHeaders = JsonConvert.DeserializeObject<Dictionary<string, object>>(
                Encoding.UTF8.GetString(metaData));

            if (eventHeaders.Any() == false)
            {
                logService.Error(string.Format("No meta data for event found ({0})", eventType));
                return false;
            }

            if (eventHeaders.ContainsKey(EventHeaders.EventClrType) == false)
            {
                logService.Error(string.Format("Event type not found in meta data ({0})", eventType));
                return false;
            }

            type = Type.GetType(eventHeaders[EventHeaders.EventClrType].ToString());
            if (type == null)
            {
                logService.Error(string.Format("Could not resolve EventClrType ({0})",
                    eventHeaders[EventHeaders.EventClrType]));
                return false;
            }
            return true;
        }
    }
}