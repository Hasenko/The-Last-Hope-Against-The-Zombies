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

    [SerializeField] private Text cptNText;
    [SerializeField] private Text cptRText;
    [SerializeField] private Text cptTText;

    private int cptN = 0;
    private int cptR = 0;
    private int cptT = 0;

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

    public void UpdateCptZombie(int cptToUptate)
    {
        switch (cptToUptate)
        {
            case 0:
                cptN++;
                cptNText.text = cptN.ToString();
                break;

            case 1:
                cptR++;
                cptRText.text = cptR.ToString();
                break;

            case 2:
                cptT++;
                cptTText.text = cptT.ToString();
                break;
        }
    }
}