using System;
using UnityEngine;

public interface IDamagable
{
    void TakeDamage(int damageAmount);
}

public class PlayerCondition :MonoBehaviour, IDamagable
{
    public UICondition uICondition;

    Conditions hp { get { return uICondition.hp; } }
    Conditions stamina { get { return uICondition.stamina; } }

    public event Action onTakeDamage;

    void Update()
    {
        stamina.Add(stamina.passiveValue * Time.deltaTime);

        if (hp.curValue < 0f)
        {
            Die();
        }
    }

    public void Heal(float amount)
    {
        hp.Add(amount); 
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
}
