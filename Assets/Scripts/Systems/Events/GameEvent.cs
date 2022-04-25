using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game Event")]
public class GameEvent : ScriptableObject
{
  private readonly List<GameEventListener> eventListeners = new List<GameEventListener>();

  public void Raise()
  {
    // iterate in reverse so listeners can remove themselves after they respond
    for (int i = eventListeners.Count - 1; i >= 0; i--)
    {
      //eventListeners[i].OnEventRaised();
    }
  }

  public void AddListener(GameEventListener listener)
  {
    if (!eventListeners.Contains(listener))
    {
      eventListeners.Add(listener);
    }
  }

  public void RemoveListener(GameEventListener listener)
  {
    if (eventListeners.Contains(listener))
    {
      eventListeners.Remove(listener);
    }
  }
}
