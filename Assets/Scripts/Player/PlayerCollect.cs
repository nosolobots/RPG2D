using System.Collections.Generic;
using UnityEngine;

public class PlayerCollect : MonoBehaviour
{
    List<IPlayerCollectObserver> observers = new List<IPlayerCollectObserver>();
    public void AddObserver(IPlayerCollectObserver observer)
    {
        if (!observers.Contains(observer))
        {
            observers.Add(observer);
        }
    }

    public void RemoveObserver(IPlayerCollectObserver observer)
    {
        if (observers.Contains(observer))
        {
            observers.Remove(observer);
        }
    }

    public void NotifyObservers(string itemID)
    {
        foreach (var observer in observers)
        {
            observer.OnNotify(itemID);
        }
    }
}
