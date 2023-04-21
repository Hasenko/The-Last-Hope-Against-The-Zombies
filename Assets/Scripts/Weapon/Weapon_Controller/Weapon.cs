using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.Burst.CompilerServices;
using Unity.Mathematics;
using UnityEditor.PackageManager;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    public float range = 100f;
    public float shotDelay = 0.2f;
    public Transform muzzlePoint;
    public GameObject impactPrefab;
    public GameObject smokePrefab;

    public GameObject bloodPrefab;

    private float timer = 0f;

    private void Start()
    {
        timer = Time.time;
    }

    public void Shoot()
    {
        if(Time.time > timer)
        {
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, range))
            {
                Debug.Log("Tir sur :" + hit.collider.name);
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

            else
            {
                Debug.Log("Tir dans le vide");
            }

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

    }
    private void SpawnBloodParticules(Vector3 position, RaycastHit hit)
    {
        GameObject bloodey = Instantiate(bloodPrefab, position, Quaternion.FromToRotation(Vector3.forward, hit.normal)) as GameObject;
        Destroy(bloodey.gameObject, 0.5f);
    }
}
