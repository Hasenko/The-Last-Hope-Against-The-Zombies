using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.TextCore.Text;

public class ZombieAI : MonoBehaviour
{
    private NavMeshAgent zombie = null;
    [SerializeField] private Transform target;
    Animator animator;
    public float zombie_speed = 1;
    public float zombieViewRange = 15;

    // Start is called before the first frame update
    private void Start()
    {
        GetReference();
    }

    private void Update()
    {
        MoveToTarget();
    }

    private void MoveToTarget ()
    {
        animator.SetInteger("Distance", (int) Vector3.Distance(transform.position, target.position));

        if (Vector3.Distance(transform.position, target.position) >= zombieViewRange)
        {
            zombie_speed = 1;
        }
        else if (Vector3.Distance(transform.position, target.position) < zombieViewRange && Vector3.Distance(transform.position, target.position) >= (zombieViewRange/100 * ((100/3) * 2)))
        {
            zombie_speed = 3;
        }
        else if (Vector3.Distance(transform.position, target.position) < (zombieViewRange / 100 * ((100 / 3) * 2)))
        {
            zombie_speed = 5;

        }
        if (Vector3.Distance(transform.position, target.position) < zombieViewRange)
        {
            zombie.SetDestination(target.position);
            zombie.speed = zombie_speed;
        }
    }

    private void GetReference()
    {
        zombie = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }


}
