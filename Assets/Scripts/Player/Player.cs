using StarterAssets;
using System;
using UnityEngine;

public class Player :MonoBehaviour
{
    public StarterAssetsInputs controller;
    public PlayerCondition condition;
    public ThirdPersonController personController;
    public CamController camController;

    public ItemData itemData;
    public Action addItem;

    public Transform dropPosition;

    private void Awake()
    {
        CharacterManager.Instance.Player = this; 
        controller = GetComponent<StarterAssetsInputs>();
        condition = GetComponent<PlayerCondition>();
        personController = GetComponent<ThirdPersonController>();
    }
}
