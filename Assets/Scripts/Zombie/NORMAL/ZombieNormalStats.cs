using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieNormalStats : CharacterStats
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


    public bool isZombieNormalDead()
    {
        if (base.isDead)
            return true;
        return false;
    }

    public override void InitVariables()
    {
        base.InitVariables();
        maxHealth = 50;
        SetHealthTo(maxHealth);
        isDead = false;

        damage = 5;
        attackSpeed = 1.5f;
        defaultSpeed = 1.5f;
        actualSpeed = defaultSpeed;
    }

}
