using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour {

    public Transform weaponHold;
    public Weapon[] weaponList;

    public Weapon currentWeapon;
    public int currentWeaponIndex = 0;

    public PlayerHUD hud;

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
    }

    private void EquipWeapon(int index)
    {
        if (index < weaponList.Length && index >= 0)
        {
            if(currentWeapon != null)
            {
                Destroy(currentWeapon.gameObject);
            }
            currentWeapon = Instantiate(weaponList[index], weaponHold.position, weaponHold.rotation) as Weapon;
            currentWeapon.transform.parent= weaponHold;
        }
    }

    public void FireWeapon()
    {
        currentWeapon.Shoot();
        hud.UpdateAmmo(currentWeapon.currentAmmoInClip, currentWeapon.maxAmmoInClip);
        Debug.Log("3");
    }

}
