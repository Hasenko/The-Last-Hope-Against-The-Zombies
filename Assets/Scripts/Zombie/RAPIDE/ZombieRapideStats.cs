using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieRapideStats : CharacterStats
{
    [SerializeField] private int damage;
    [SerializeField] public float attackSpeed;
    [SerializeField] public float actualSpeed;
    [SerializeField] public float defaultSpeed;

    private void Start()
    {
        InitVariables();
    }
    public void DealDamage(CharacterStats statsToDamage)
    {
        // POSSIBILITE DE FAIRE DES DEGATS
        statsToDamage.TakeDamage(damage);
    }

    public override void Die()
    {
        base.Die();
        Destroy(gameObject);
    }

    public override void InitVariables()
    {
        base.InitVariables();
        maxHealth = 7;
        SetHealthTo(maxHealth);
        isDead = false;

        damage = 5;
        attackSpeed = 1.5f;
        defaultSpeed = 4f;
        actualSpeed = defaultSpeed;
    }
}
