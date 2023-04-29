using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerHUD : MonoBehaviour
{
    [SerializeField] private Text currentHealthText;
    [SerializeField] private Text maxHealthText;

    [SerializeField] private Text currentAmmoText;
    [SerializeField] private Text maxAmmoText;

    public void UpdateHealth(int currentHealth, int maxHealth)
    {
        currentHealthText.text = currentHealth.ToString();
        maxHealthText.text = maxHealth.ToString();
    }

    public void UpdateAmmo(int currentAmmo, int maxAmmo)
    {
        currentAmmoText.text = currentAmmo.ToString();
        maxAmmoText.text = maxAmmo.ToString();
    }

}