using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieTankyStats : CharacterStats
{
    [SerializeField] private int damage;
    [SerializeField] public float attackSpeed;
    [SerializeField] public float actualSpeed;
    [SerializeField] public float defaultSpeed;
    [SerializeField] public float zombieViewRange;

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
        maxHealth = 20;
        SetHealthTo(maxHealth);
        isDead = false;

        damage = 10;
        attackSpeed = 3f;
        defaultSpeed = 1.5f;
        actualSpeed = defaultSpeed;
        zombieViewRange = 10;
    }
}
