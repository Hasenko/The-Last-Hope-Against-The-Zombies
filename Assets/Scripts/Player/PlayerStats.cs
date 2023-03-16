using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats
{
    private PlayerHUD hud;

    private void Start()
    {
        GetReference();
        InitVariables();
    }

    private void GetReference()
    {
        hud = GetComponent<PlayerHUD>();
    }

    public override void CheckHealth()
    {
        base.CheckHealth();
        hud.UpdateHealth(health, maxHealth);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T)) // TEST A RETIRER
        {
            TakeDamage(10);
        }
    }
}
