using System;
using System.Collections.Generic;

namespace Imperium.Ecs.Systems.TimeManagement
{
    public class Time : System
    {
        private readonly Dictionary<object, TimeSpan> _timeCache = new Dictionary<object, TimeSpan>();
        
        
        
        public TimeSpan After(TimeSpan delay, Action action) => After(delay, action, action);
        
        public TimeSpan After(TimeSpan delay, Action action, object key)
        {
            if (!_timeCache.ContainsKey(key))
            {
                _timeCache[key] = delay;
            }

            _timeCache[key] -= Ecs.UpdateDelay;

            if (_timeCache[key] < TimeSpan.Zero)
            {
                action();
            }

            return _timeCache[key];
        }
    }
}