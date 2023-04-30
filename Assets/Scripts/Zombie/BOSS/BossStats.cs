using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStats : CharacterStats
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


    public bool isBossDead()
    {
        if (base.isDead)
            return true;
        return false;
    }

    public override void InitVariables()
    {
        base.InitVariables();
        maxHealth = 2000;
        SetHealthTo(maxHealth);
        isDead = false;

        damage = 15;
        attackSpeed = 3.75f;
        defaultSpeed = 3f;
        actualSpeed = defaultSpeed;
        zombieViewRange = 50;
    }

    public int GetHealthBoss()
    {
        return GetHealth();
    }

    public void SecondeBossPhase()
    {
        damage = 25;
    }
}
