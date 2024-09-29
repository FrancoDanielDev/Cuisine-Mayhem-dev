using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager instance;

    public delegate void EventReceiver(params object[] parameters);
    public Dictionary<NameEvent, EventReceiver> _events = new Dictionary<NameEvent, EventReceiver>();

    public enum NameEvent
    {
        FinishLevel,
        CustomerLeaves
    }

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    public void Subscribe(NameEvent eventType, EventReceiver listener)
    {
        if (!_events.ContainsKey(eventType))
            _events.Add(eventType, listener);
        else
            _events[eventType] += listener;
    }

    public void Unsubscribe(NameEvent eventType, EventReceiver listener)
    {
        if (_events.ContainsKey(eventType))
        {
            _events[eventType] -= listener;

            if (_events[eventType] == null)
                _events.Remove(eventType);
        }
    }

    public void Trigger(NameEvent eventType, params object[] parameters)
    {
        if (_events.ContainsKey(eventType))
            _events[eventType](parameters);
    }
}
