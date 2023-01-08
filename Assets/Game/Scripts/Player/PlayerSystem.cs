using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSystem : MonoBehaviour
{
    [SerializeField] private LevelEventChannel levelEventChannel;
    [SerializeField] private PlayerEventChannel playerEventChannel;

    [SerializeField] private Player playerInstance;

    private void Awake()
    {
        DestroyPlayer();
    }

    private void OnEnable()
    {
        levelEventChannel.onStartNewLevel.AddListener(SpawnPlayer);
        levelEventChannel.onStopLevel.AddListener(DestroyPlayer);
        levelEventChannel.onCompleteLevel.AddListener(DestroyPlayer);
        playerEventChannel.onDestroyPlayer.AddListener(DestroyPlayer);
    }

    private void OnDisable()
    {
        levelEventChannel.onStartNewLevel.RemoveListener(SpawnPlayer);
        levelEventChannel.onStopLevel.RemoveListener(DestroyPlayer);
        levelEventChannel.onCompleteLevel.RemoveListener(DestroyPlayer);
        playerEventChannel.onDestroyPlayer.RemoveListener(DestroyPlayer);
    }

    public void SpawnPlayer()
    {
        playerInstance.Health = 3;
        playerInstance.GetComponent<PlayerMovement>().enabled = true;
        playerInstance.GetComponent<PlayerWeaponController>().enabled = true;
        playerInstance.transform.position = Vector3.zero;
        playerInstance.gameObject.SetActive(true);
    }

    public void DestroyPlayer()
    {
        playerInstance.gameObject.SetActive(false);
    }
}
