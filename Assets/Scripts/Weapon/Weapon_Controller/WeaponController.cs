using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour {

    public Transform weaponHold;
    public Weapon[] weaponList;

    private Weapon currentWeapon;
    private int currentWeaponIndex = 0;

    void Start()
    {
        if (weaponList != null && weaponList.Length > 0)
        {
            EquipWeapon(0);
        }
    }

    void Update()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll > 0f)
        {
            currentWeaponIndex = 1;
            EquipWeapon(currentWeaponIndex);
        }
        else if (scroll < 0f)
        {
            currentWeaponIndex = 0;
            EquipWeapon(currentWeaponIndex);
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
