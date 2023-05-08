using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public override void Die()
    {
        base.Die();
        SceneManager.LoadScene("death menu", LoadSceneMode.Single);
        Cursor.lockState = CursorLockMode.Confined;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y)) // TEST A RETIRER
        {
            TakeDamage(10);
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            Heal(10);
        }
    }
}
