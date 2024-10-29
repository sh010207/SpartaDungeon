using Palmmedia.ReportGenerator.Core.Reporting.Builders;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public interface IDamagable
{
    void TakeDamage(int damageAmount);
}



public class PlayerCondition :MonoBehaviour, IDamagable
{
    public UICondition uICondition;

    public InputActionAsset inputActions;
    private InputAction  inputAction;

    public float staminaAmount;
    public float orginSpeed;

    Conditions hp { get { return uICondition.hp; } }
    Conditions stamina { get { return uICondition.stamina; } }

    public event Action onTakeDamage;

    void Update()
    {
        if (CharacterManager.Instance.Player.controller.sprint)
        {
            StaminaSubtract(staminaAmount * Time.deltaTime);
        }
        else
        {
            stamina.Add(stamina.passiveValue * Time.deltaTime);
        }

        NotSprint();

        if (hp.curValue < 0f)
        {
            Die();
        }
    }

    public void StaminaSubtract(float amount)
    {
        stamina.Subtract(amount);
    }

    public void Heal(float amount)
    {
        hp.Add(amount); 
    }

    public void PlusStamina(float amount)
    {
        stamina.Add(amount);
    }

    public void PlusSpeed(float speed, float duration)
    {
        StartCoroutine(SpeedUP(speed, duration));
    }

    public void NotSprint()
    {
        if(stamina.curValue <= 0f)
        {
            CharacterManager.Instance.Player.controller.sprint = false;
            inputAction = inputActions.FindAction("Sprint");
            inputAction.Disable();
        }
        else
        {
            inputAction = inputActions.FindAction("Sprint");
            inputAction.Enable();
        }
    }

    public void TakeDamage(int damageAmount)
    {
        hp.Subtract(damageAmount);
        onTakeDamage?.Invoke();
    }

    private void Die()
    {
        Debug.Log("Die");
    }
    
    IEnumerator SpeedUP(float speed, float duration)
    {
        orginSpeed = CharacterManager.Instance.Player.personController.MoveSpeed;

        CharacterManager.Instance.Player.personController.MoveSpeed = speed;

        yield return new WaitForSeconds(duration);

        CharacterManager.Instance.Player.personController.MoveSpeed = orginSpeed;
    }

}
