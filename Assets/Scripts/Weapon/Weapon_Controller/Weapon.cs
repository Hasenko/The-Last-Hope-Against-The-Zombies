using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor.PackageManager;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    public float range = 100f;
    public float shotDelay = 0.2f;
    public Transform muzzlePoint;
    public GameObject impactPrefab;
    public GameObject smokePrefab;

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
                if (hit.transform.tag == "Enemy")
                {
                    CharacterStats enemyStats = hit.transform.GetComponent<CharacterStats>();
                    enemyStats.TakeDamage(3);
                }
                GameObject go = Instantiate(impactPrefab, hit.point, Quaternion.identity) as GameObject;
                Destroy(go.gameObject, 3f);

                GameObject smokey = Instantiate(smokePrefab, hit.point, Quaternion.identity) as GameObject;
                Destroy(smokey.gameObject, 1f);
            }

            else
            {
                Debug.Log("Tir dans le vide");
            }

            timer = Time.time + shotDelay;
        }       
    }

    public void Reload()
    {

    }

}
