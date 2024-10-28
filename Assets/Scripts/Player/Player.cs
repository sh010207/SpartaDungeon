using StarterAssets;
using System;
using UnityEngine;

public class Player :MonoBehaviour
{
    public ThirdPersonController controller;
    public PlayerCondition condition;

    public ItemData itemData;
    public Action addItem;

    private void Awake()
    {
        CharacterManager.Instance.Player = this; 
        controller = GetComponent<ThirdPersonController>();
        condition = GetComponent<PlayerCondition>();
    }
}