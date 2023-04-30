using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.TextCore.Text;

public class ZombieAIRapide : MonoBehaviour
{
    private NavMeshAgent zombie = null;
    [SerializeField] private Transform target;
    private ZombieRapideStats stats = null;
    Animator anim;
    public float zombieViewRange = 20;
    private float timeOfLastAttack = 0;
    [SerializeField] private PlayerHUD hud;
    private bool waitToDie = false;

    // Start is called before the first frame update
    private void Start()
    {
        GetReference();
    }

    private void Update()
    {
        MoveToTarget();
    }

    private void MoveToTarget()
    {

        float distanceToTarget = Vector3.Distance(target.position, transform.position);

        if (distanceToTarget <= zombieViewRange)
        {
            zombie.SetDestination(target.position);
            stats.actualSpeed = stats.defaultSpeed;
            zombie.speed = stats.actualSpeed;
        }
        else
        {
            stats.actualSpeed = 0f;
            zombie.speed = stats.actualSpeed;
        }
        if (distanceToTarget <= zombie.stoppingDistance)
        {
            stats.actualSpeed = 0f;
            zombie.speed = stats.actualSpeed;

            if (Time.time >= timeOfLastAttack + stats.attackSpeed)
            {
                timeOfLastAttack = Time.time;
                CharacterStats targetStats = target.GetComponent<CharacterStats>();
                AttackTarget(targetStats);
            }
        }

        if (ZombieRapideDead() && !waitToDie)
        {
            zombie.speed = 0;
            anim.Play("die");
            Destroy(gameObject, anim.GetCurrentAnimatorStateInfo(0).length);
            hud.UpdateCptZombie(1);
            waitToDie = true;
        }
        else if (ZombieRapideDead())
        {
            zombie.speed = 0;
            anim.Play("die");
            Destroy(gameObject, anim.GetCurrentAnimatorStateInfo(0).length);
        }
        if (gameObject == null)
        {
            zombie.speed = 0;
            waitToDie = false;
        }



        anim.SetFloat("Speed", stats.actualSpeed);
    }

    private void AttackTarget(CharacterStats statsToDamage)
    {
        anim.SetTrigger("attack");
        stats.DealDamage(statsToDamage);
    }

    private void GetReference()
    {
        zombie = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        stats = GetComponent<ZombieRapideStats>();
    }
    private bool ZombieRapideDead()
    {
        if (stats.isZombieRapideDead())
        {
            return true;
        }
        return false;
    }
}
