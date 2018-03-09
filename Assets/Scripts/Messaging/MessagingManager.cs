using System;
using System.Collections.Generic;
using UnityEngine;
public class MessagingManager : MonoBehaviour
{
    //Static singleton property
    public static MessagingManager Instance { get; private set; }

    // public property for manager
    private List<Action> subscribers = new List<Action>();

    void Awake()
    {
        Debug.Log("Messaging Manager Started");

        //First, we check if there are any other instances conflicting
        if (Instance != null && Instance != this)
        {
            //Destroy other instances if it's not the same
            Destroy(gameObject);
        }

        //Save our current singleton instance
        Instance = this;

        //Make sure that the instance is not destroyed between scenes
        //(this is optional)
        DontDestroyOnLoad(gameObject);
    }

    //The Subscribe method for manager 
    public void Subscribe(Action subscriber)
    {
        Debug.Log("Subscriber registered");
        subscribers.Add(subscriber);
    }

    //The Unsubscribe method for manager public 
    void UnSubscribe(Action subscriber)
    {
        Debug.Log("Subscriber registered");
        subscribers.Remove(subscriber);
    }

    //Clear subscribers method for manager
    public void ClearAllSubscribers()
    {
        subscribers.Clear();
    }

    public void Broadcast()
    {
        Debug.Log("Broadcast requested, No of Subscribers = " + subscribers.Count);
        foreach (var subscriber in subscribers)
        {
            subscriber();
        }
    }
}