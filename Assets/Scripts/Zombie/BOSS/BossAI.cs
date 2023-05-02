using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.TextCore.Text;

public class BossAI : MonoBehaviour
{
    private NavMeshAgent zombie = null;
    [SerializeField] private Transform target;
    private BossStats stats = null;
    private Animator anim;
    private float timeOfLastAttack = 0;
    // Start is called before the first frame update

    private bool waitToDie = false;
    private bool animSecPh = false;
    private bool animSecPh2 = false;

    public AudioSource jump, roar;

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

        float distanceToTarget = Vector3.Distance(target.position, transform.position);
        if(stats.GetHealthBoss() <= 1000 && !animSecPh && !animSecPh2)
        {
            SecondePhase();
        }
        if (distanceToTarget <= stats.zombieViewRange && !animSecPh)
        {
            zombie.SetDestination(target.position);
            stats.actualSpeed = stats.defaultSpeed;
            zombie.speed = stats.actualSpeed;
        }
        else if (!animSecPh)
        {
            stats.actualSpeed = 0f;
            zombie.speed = stats.actualSpeed;
        }
        if (distanceToTarget <= zombie.stoppingDistance && !animSecPh)
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

        if (BossDead() && !waitToDie)
        {
            zombie.speed = 0;
            anim.Play("die");
            Destroy(gameObject, anim.GetCurrentAnimatorStateInfo(0).length);
            waitToDie = true;
        }
        else if (BossDead())
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
        stats = GetComponent<BossStats>();
    }

    private bool BossDead()
    {
        if (stats.isBossDead())
            return true;
        return false;
    }

    private void SecondePhase()
    {
        animSecPh2 = true;
        animSecPh = true;
        zombie.speed = 0;
        StartCoroutine(PlayAnimationsScPh());
        stats.SecondeBossPhase();
    }

    IEnumerator PlayAnimationsScPh()
    {
        // Joue la première animation
        anim.Play("jump");
        // Attend la fin de la première animation
        yield return new WaitForSeconds(2);

        jump.enabled = true;
        // Joue la deuxième animation
        anim.Play("roar");
        roar.enabled = true;
        yield return new WaitForSeconds(3);
        animSecPh = false;
        zombie.speed = stats.defaultSpeed;
        anim.Play("Movement");
    }

}
