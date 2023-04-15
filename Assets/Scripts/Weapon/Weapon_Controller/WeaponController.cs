using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour {

    public Transform weaponHold;
    public Weapon[] weaponList;

    private Weapon currentWeapon;

    void Start()
    {
        if (weaponList != null && weaponList.Length > 0)
        {
            EquipWeapon(0);
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
    }

}
