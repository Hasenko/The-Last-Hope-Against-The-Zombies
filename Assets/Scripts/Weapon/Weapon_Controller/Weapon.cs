using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.Burst.CompilerServices;
using Unity.Mathematics;
using UnityEngine;


public class Weapon : MonoBehaviour
{

    public float range = 100f;
    public float shotDelay = 0.2f;
    public Transform muzzlePoint;
    public Transform extractorPoint;
    public GameObject impactPrefab;
    public GameObject smokePrefab;
    public GameObject bulletCase;

    public int currentAmmoInClip = 20;
    public int maxAmmoInClip = 30;
    public int currentAmmoReserve = 200;
    public int maxAmmoReserve = 200;

    public GameObject bloodPrefab;

    private float timer = 0f;
    private MuzzleFlash muzzleFlashScript;

    private void Start()
    {
        timer = Time.time;
        muzzleFlashScript = GetComponent<MuzzleFlash>();
    }

    public void Shoot()
    {
        if (Time.time > timer && currentAmmoInClip > 0)
        {
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, range))
            {
                // Si le joueur vise un zombie et tire
                if (hittingEnemy(hit))
                {
                    CharacterStats enemyStats = hit.transform.GetComponent<CharacterStats>();
                    enemyStats.TakeDamage(3);

                    // Particule de sang

                    SpawnBloodParticules(hit.point, hit);
                }
                else
                {
                    GameObject go = Instantiate(impactPrefab, hit.point, Quaternion.identity) as GameObject;
                    Destroy(go.gameObject, 3f);

                    GameObject smokey = Instantiate(smokePrefab, hit.point, Quaternion.FromToRotation(Vector3.forward, hit.normal)) as GameObject;
                    Destroy(smokey.gameObject, 1f);
                }
            }
            muzzleFlashScript.Displat();
            GameObject tmp = (GameObject)Instantiate(bulletCase, extractorPoint.position, extractorPoint.rotation);
            Rigidbody rb = tmp.GetComponent<Rigidbody>();
            rb.AddForce(Vector3.forward * 3f);
            rb.AddRelativeTorque(new Vector3(UnityEngine.Random.Range(0f, 90f), UnityEngine.Random.Range(0f, 90f), UnityEngine.Random.Range(0f, 90f)));
            Destroy(tmp, 4f);

            currentAmmoInClip--;
            timer = Time.time + shotDelay;
        }       
    }

    private bool hittingEnemy(RaycastHit hit)
    {
        if (hit.transform.tag == "Enemy")
            return true;
        return false;
    }

    public void Reload()
    {
        if (Time.time > timer)
        {
            if(currentAmmoInClip < maxAmmoInClip && currentAmmoReserve > 0)
            {
                //tant qu'il y a de la place dans le chargeur

                while (currentAmmoInClip < maxAmmoInClip)
                {
                    if(currentAmmoReserve > 0)
                    {
                        currentAmmoReserve--;
                        currentAmmoInClip++;
                    }
                    else
                    {
                        break;
                    }
                }

                //jouer un son/animation de rechargement
                timer = Time.time + 2f;
            }
        }
    }

    private void SpawnBloodParticules(Vector3 position, RaycastHit hit)
    {
        GameObject bloodey = Instantiate(bloodPrefab, position, Quaternion.FromToRotation(Vector3.forward, hit.normal)) as GameObject;
        Destroy(bloodey.gameObject, 0.5f);
    }
}
