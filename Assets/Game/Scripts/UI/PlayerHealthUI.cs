using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthUI : MonoBehaviour
{
    [SerializeField] private PlayerHealthEventChannel playerHealthEventChannel;
    [SerializeField] private List<HealthContainer> hpContainers;

    private void OnEnable()
    {
        playerHealthEventChannel.onCurrentHPUpdated.AddListener(OnHealthUpdated);
    }

    private void OnDisable()
    {
        playerHealthEventChannel.onCurrentHPUpdated.RemoveListener(OnHealthUpdated);
    }

    private void OnHealthUpdated(int newHP)
    {
        for (int i = 0; i < hpContainers.Count; i++)
        {
            if(i < newHP)
            {
                hpContainers[i]?.Enable();
            }
            else
            {
                hpContainers[i]?.Disable();
            }
        }
    }
}
