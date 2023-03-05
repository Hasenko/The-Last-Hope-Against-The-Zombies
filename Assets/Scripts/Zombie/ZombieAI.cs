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
    public Animator animator;
    public float zombie_speed = 1.0f;

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

        if (Vector3.Distance(transform.position, target.position) >= 15)
        {
            zombie_speed = 1.0f;
        }
        else if (Vector3.Distance(transform.position, target.position) < 15 && Vector3.Distance(transform.position, target.position) >= 10)
        {
            zombie_speed = 3.0f;
        }
        else if (Vector3.Distance(transform.position, target.position) < 10)
        {
            zombie_speed = 5.0f;
        }
        if (Vector3.Distance(transform.position, target.position) < 15)
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
