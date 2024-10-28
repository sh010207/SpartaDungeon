using StarterAssets;
using UnityEngine;

public class Player :MonoBehaviour
{
    public ThirdPersonController controller;
    private void Awake()
    {
        CharacterManager.Instance.Player = this; 
        controller = GetComponent<ThirdPersonController>();
    }
}