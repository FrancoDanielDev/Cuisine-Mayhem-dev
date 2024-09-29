using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionButton : MonoBehaviour, IObservable
{
    public static InteractionButton instance;

    List<IObserver> _allObservers = new List<IObserver>();

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    public void Subscribe(IObserver obs)
    {
        if (!_allObservers.Contains(obs))
            _allObservers.Add(obs);
    }

    public void Unsubscribe(IObserver obs)
    {
        if (_allObservers.Contains(obs))
            _allObservers.Remove(obs);
    }

    public void NotifyToObservers(string action)
    {
        for (int i = 0; i < _allObservers.Count; i++)
        {
            _allObservers[i].Notify(action);
        }
    }

    public void InteractButton()
    {
        NotifyToObservers("Interact");
    }

    //#if UNITY_EDITOR

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            InteractButton();
        }
    }

    //#endif
}
