using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    public float range = 100f;
    public float shotDelay = 0.2f;
    public Transform Muzzle;

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
