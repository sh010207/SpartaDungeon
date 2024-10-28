using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICondition : MonoBehaviour
{
    public Conditions hp;
    public Conditions stamina;

    private void Start()
    {
        CharacterManager.Instance.Player.condition.uICondition = this;
    }
}
