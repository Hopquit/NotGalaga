using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SimpleEvent : UnityEvent<GameObject>
{

}
public class EventBus : MonoBehaviour
{
    private Dictionary <string, SimpleEvent> eventDictionary;
    private static EventBus eventManager;
    public static EventBus instance
    {
        get
        {
            if(!eventManager)
            {
                eventManager = FindObjectOfType (typeof (EventBus)) as EventBus;
                if (!eventManager)
                {
                    Debug.LogError ("There needs to be one active EventManger script on a GameObject in your scene.");
                }
                else
                {
                    eventManager.Init ();
                }
            }
            return eventManager;
        }
    }
    void Init ()
    {
        if (eventDictionary == null)
        {
            eventDictionary = new Dictionary<string, SimpleEvent>();
        }
    }
    public static void StartListening (string eventName, UnityAction<GameObject> listener) {
        SimpleEvent thisEvent = null;
        if (instance.eventDictionary.TryGetValue (eventName, out thisEvent))
        {
            thisEvent.AddListener (listener);
        }
        else{
            thisEvent = new SimpleEvent ();
            thisEvent.AddListener (listener);
            instance.eventDictionary.Add (eventName, thisEvent);
        }
        
    }
    public static void StopListening (string eventName, UnityAction<GameObject> listener)
    {
        if (eventManager == null) return;
        SimpleEvent thisEvent = null;
        if (instance.eventDictionary.TryGetValue (eventName, out thisEvent))
        {
            thisEvent.RemoveListener (listener);
        }
    }

    public static void TriggerEvent (string eventName, GameObject data = null)
    {
        SimpleEvent thisEvent = null;
        if (instance.eventDictionary.TryGetValue (eventName, out thisEvent))
        {
            thisEvent.Invoke (data);
        }
    }
}
