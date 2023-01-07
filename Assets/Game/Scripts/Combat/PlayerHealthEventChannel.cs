using System.Collections;
using UnityEngine;
using UnityEngine.Events;


[CreateAssetMenu]
public class PlayerHealthEventChannel : ScriptableObject
{
    public UnityEvent<int> onCurrentHPUpdated = new UnityEvent<int>();

    public void UpdateHealth(int hp)
    {
        onCurrentHPUpdated.Invoke(hp);
    }
}
