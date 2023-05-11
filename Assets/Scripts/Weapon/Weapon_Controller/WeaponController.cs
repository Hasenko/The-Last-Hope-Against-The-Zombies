using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public Transform weaponHold;
    public Weapon[] weaponList;
    public Weapon currentWeapon;
    public int currentWeaponIndex = 0;
    public PlayerHUD hud;

    public List<GameObject> inventory = new List<GameObject>();

    public AudioSource pickHealSound, useHealSound;

    void Start()
    {
        if (weaponList != null && weaponList.Length > 0)
        {
            EquipWeapon(0);
        }

        hud = GetComponent<PlayerHUD>();
        hud.UpdateAmmo(currentWeapon.currentAmmoInClip, currentWeapon.maxAmmoInClip);
        Debug.Log("1");
    }

    void Update()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll > 0f)
        {
            currentWeaponIndex = 1;
            EquipWeapon(currentWeaponIndex);
            hud.UpdateAmmo(currentWeapon.currentAmmoInClip, currentWeapon.maxAmmoInClip);
        }
        else if (scroll < 0f)
        {
            currentWeaponIndex = 0;
            EquipWeapon(currentWeaponIndex);
            hud.UpdateAmmo(currentWeapon.currentAmmoInClip, currentWeapon.maxAmmoInClip);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            PickUpHealingItem();
        }

        else if (Input.GetKeyDown(KeyCode.Q))
        {
            UseHealingItem();
        }
    }

    private void EquipWeapon(int index)
    {
        if (index < weaponList.Length && index >= 0)
        {
            if (currentWeapon != null)
            {
                Destroy(currentWeapon.gameObject);
            }
            currentWeapon = Instantiate(weaponList[index], weaponHold.position, weaponHold.rotation) as Weapon;
            currentWeapon.transform.parent = weaponHold;
        }
    }

    public void FireWeapon()
    {
        currentWeapon.Shoot();
        hud.UpdateAmmo(currentWeapon.currentAmmoInClip, currentWeapon.maxAmmoInClip);
        Debug.Log("3");
    }

    void PickUpHealingItem()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 2.0f))
        {
            HealingItem healingItem = hit.collider.GetComponent<HealingItem>();
            if (healingItem != null)
            {
                inventory.Add(healingItem.gameObject);
                Debug.Log("Healing item picked up and added to inventory.");
                healingItem.GetComponent<MeshRenderer>().enabled = false; // désactiver le MeshRenderer
                hud.UpdateHeal(true);
                pickHealSound.Play();
            }
        }
    }

    void UseHealingItem()
    {
        if (inventory.Count > 0)
        {
            GameObject healingItem = inventory[0];

            // Vérifier si l'objet de soin existe toujours avant d'y accéder
            if (healingItem == null)
            {
                Debug.LogWarning("Healing item is null");
                inventory.RemoveAt(0);
                return;
            }

            HealingItem itemScript = healingItem.GetComponent<HealingItem>();
            GetComponent<CharacterStats>().Heal(itemScript.healingAmount);
            useHealSound.Play();
            // Retirer l'objet de soin de la liste avant de le détruire
            inventory.RemoveAt(0);
            hud.UpdateHeal(false);

            // Détruire l'objet de soin
            Destroy(healingItem.gameObject);
            Debug.Log("Healing item used and removed from inventory.");
        }
        else
        {
            Debug.Log("No healing items in inventory.");
        }
    }



}
