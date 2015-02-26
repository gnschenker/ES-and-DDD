using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using log4net;

namespace TaskManager.Infrastructure
{
    public class EventDispatcher : IEventDispatcher
    {
        private readonly ILog logService;
        private Dictionary<Type, Info[]> dictionary = new Dictionary<Type, Info[]>();

        public EventDispatcher(IEnumerable<object> projections, ILog logService)
        {
            this.logService = logService;
            WireUpProjections(projections);
        }

        public void Dispatch(object e)
        {
            var eventType = e.GetType();
            if (dictionary.ContainsKey(eventType) == false)
                return;

            foreach (var item in dictionary[eventType])
            {
                try
                {
                    item.MethodInfo.Invoke(item.Projection, new[] { e });
                }
                catch (Exception ex)
                {
                    logService.Error(string.Format("Could not dispatch event {0} to projection {1}", eventType.Name,
                        item.Projection.GetType().Name), ex);
                }
            }
        }

        private void WireUpProjections(IEnumerable<object> projections)
        {
            dictionary = projections.Select(p => new { Projection = p, Type = p.GetType() })
                .Select(x => new
                {
                    x.Projection,
                    MethodInfos = x.Type
                        .GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                        .Where(m => m.Name == "When" && m.GetParameters().Count() == 1)
                })
                .SelectMany(x => x.MethodInfos.Select(
                    y => new { x.Projection, MethodInfo = y, y.GetParameters().First().ParameterType }))
                .GroupBy(x => x.ParameterType)
                .ToDictionary(g => g.Key,
                    g => g.Select(y => new Info { Projection = y.Projection, MethodInfo = y.MethodInfo }).ToArray());
        }

        public class Info
        {
            public MethodInfo MethodInfo;
            public object Projection;
        }
    }
}