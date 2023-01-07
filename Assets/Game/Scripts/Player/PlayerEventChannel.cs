using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu]
public class PlayerEventChannel : ScriptableObject
{
    public UnityEvent onDestroyPlayer = new UnityEvent();
       
    public void DestroyPlayer()
    {
        onDestroyPlayer?.Invoke();
    }
}
