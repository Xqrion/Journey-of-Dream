using System.Collections.Generic;
using MyGameSystem.Core;
using UnityEngine;

namespace MyGameSystem.Manager
{
    public class EventManager : Singleton<EventManager>
    {
        public delegate void ProcessEvent(Object obj, int param1, int param2);
        private readonly Dictionary<string, ProcessEvent> _eventDictionary = new Dictionary<string, ProcessEvent>();

        public void RegisterEvent(string eventName, ProcessEvent processEvent)
        {
            if (!_eventDictionary.TryAdd(eventName, processEvent))
            {
                _eventDictionary[eventName] += processEvent;
            }
        }

        public void UnregisterEvent(string eventName, ProcessEvent processEvent)
        {
            if (_eventDictionary.ContainsKey(eventName))
            {
                _eventDictionary[eventName] -= processEvent;
            }
        }

        public void TriggerEvent(string eventName, Object obj, int param1, int param2, bool singleUse = false)
        {
            if (_eventDictionary.ContainsKey(eventName))
            {
                Debug.Log("触发event" + eventName);
                _eventDictionary[eventName].Invoke(obj, param1, param2);

                if (singleUse)
                {
                    _eventDictionary.Remove(eventName);
                }
            }
            else
            {
                //Debug.LogWarning("EventSystem: Trigger event" + eventName + " not found");
            }
        }


    }
}
