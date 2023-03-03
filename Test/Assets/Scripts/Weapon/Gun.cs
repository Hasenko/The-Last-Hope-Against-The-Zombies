using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{

    [Header("References")]
    [SerializeField] private GunData gunData;
    [SerializeField] private Transform Muzzle;

    float timeSinceLastShot;

    private void Start()
    {
        Player_Shoot.shootInput += Shoot;
        Player_Shoot.reloadInput += StartReload;
    }

    private void OnDisable() => gunData.Reloading = false;

    public void StartReload()
    {
        if (!gunData.Reloading && this.gameObject.activeSelf)
            StartCoroutine(Reload());
    }

    private IEnumerator Reload()
    {
        gunData.Reloading = true;

        yield return new WaitForSeconds(gunData.Reload_Time);

        gunData.Current_Ammo = gunData.Magazine_Size;

        gunData.Reloading = false;
    }

    private bool CanShoot() => !gunData.Reloading && timeSinceLastShot > 1f / (gunData.Fire_Rate / 60f);

    private void Shoot()
    {
        if (gunData.Current_Ammo > 0)
        {
            if (CanShoot())
            {
                if (Physics.Raycast(Muzzle.position, Muzzle.forward, out RaycastHit hitInfo, gunData.Max_Distance))
                {
                    IDamageable damageable = hitInfo.transform.GetComponent<IDamageable>();
                    damageable?.TakeDamage(gunData.Damage);
                }

                gunData.Current_Ammo--;
                timeSinceLastShot = 0;
                OnGunShot();
            }
        }
    }

    private void Update()
    {
        timeSinceLastShot += Time.deltaTime;

        Debug.DrawRay(Muzzle.position, Muzzle.forward * gunData.Max_Distance);
    }

    private void OnGunShot() { }
}