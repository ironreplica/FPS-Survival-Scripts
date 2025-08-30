using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieController : MonoBehaviour
{
    public float damage = 5f;
    public float maxHealth = 100f;
    private NavMeshAgent agent;
    private float curHealth;
    public float moveSpeed = 3f;
    public bool isTouchingEnemy;
    public float attackDelay = 1f;
    /*public GameObject bloodParticlesPrefab;*/
    /*public bool seesPlayer;*/
    private bool isAttacking;
    public GameObject curTarget;
    private Vector3 randomLocationGoal;
    public GameObject damageParticles;
    private Vector3 lookAt;
    private GameObject hordeBeacon;
    private Animator animator;
    public RaycastHit visHit;
    private AudioSource audioSource;
    private int totalAttacks;
    private bool moving;
    public Vector3 randomLocation;
    void Start()
    {
        hordeBeacon = GameObject.FindGameObjectWithTag("Campfire");
        /*seesPlayer = false;*/
        agent = GetComponent<NavMeshAgent>();
        isAttacking = false;
        isTouchingEnemy = false;
        curHealth = maxHealth;
        totalAttacks = 0;
        lookAt = transform.position;
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        /*if (other.CompareTag("Player") || other.CompareTag("Building") || other.CompareTag("Campfire"))
        {
            curTarget = other.gameObject;
            isTouchingEnemy = true;
        }*/
    }
    private void OnTriggerExit(Collider other)
    {
        /*if (other.CompareTag("Player") || other.CompareTag("Building") || other.CompareTag("Campfire"))
        {
            curTarget = null;
            isTouchingEnemy = false;
            isAttacking = false;
        }*/
    }
    void Update()
    {
        if (curTarget == null)
        {
            isTouchingEnemy = false;
            if (GameObject.FindGameObjectWithTag("Campfire"))
            {
                curTarget = GameObject.FindGameObjectWithTag("Campfire");
            }
            else if(moving == false)
            {
                /*StartCoroutine(Wander());*/
            }
        }
        Vector3 destination;
        if(curTarget!= null)
        {
            destination = curTarget.transform.position;
        MoveTo(destination);
        }
        
    }

    private void MoveTo(Vector3 goal)
    {
        if((new Vector3(transform.position.x, transform.position.y, transform.position.z) - goal).sqrMagnitude <= 3f)
        {
            agent.velocity = Vector3.zero;
            moving = false;
        }
        else
        {
            moving = true;
            lookAt = new Vector3(goal.x, transform.position.y, goal.z);
            transform.LookAt(lookAt);

            if (!isAttacking && isTouchingEnemy)
            {
                agent.isStopped = true;
                isAttacking = true;
                animator.SetBool("Attack", true);
                animator.SetBool("ChaseTarget", false);
            }
            else if (!isTouchingEnemy)
            {
                agent.isStopped = false;
                isAttacking = false;
                animator.SetBool("ChaseTarget", true);
                animator.SetBool("Attack", false);

                if (NavMesh.SamplePosition(goal, out NavMeshHit hit, 1.0f, NavMesh.AllAreas))
                {
                    agent.SetDestination(hit.position);
                }
                else
                {
                    // If the goal is not on the NavMesh, find a nearby point that is
                    if (NavMesh.FindClosestEdge(goal, out hit, NavMesh.AllAreas))
                    {
                        agent.SetDestination(hit.position);
                    }
                }
            }
        } 
    }

    private IEnumerator Wander()
    {
        yield return new WaitForSeconds(Random.Range(2, 15));
        /*randomLocation = transform.position + Random.insideUnitSphere * 10f;
        randomLocation.y = transform.position.y; // Keep the same y position*/
    }
    public void Attack()
    {
        /*isTouchingEnemy = false;*/
        if (curTarget != null)
        {
            animator.SetBool("ChaseTarget", false);
            // Triggered by animation
            if (curTarget.GetComponent<PlayerHealth>())
            {
                curTarget.GetComponent<PlayerHealth>().TakeDamage(damage);
            }
            else if (curTarget.GetComponent<ObjectHealth>())
            {
                GameObject part = Instantiate(damageParticles);
                part.transform.parent = null;
                part.transform.position = visHit.point;
                part.transform.LookAt(transform.position);
                curTarget.GetComponent<ObjectHealth>().TakeDamage(damage);
            }

        }
    }
    /*public IEnumerator attackTarget()
    {
        *//* Instead, trigger the attack through an animation event *//*
        if(animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !animator.IsInTransition(0) && isTouchingEnemy)
        {
            yield return new WaitForSeconds(attackDelay);
            if (curTarget.GetComponent<PlayerHealth>())
            {
                curTarget.GetComponent<PlayerHealth>().TakeDamage(5f);
            }
            totalAttacks++;
            Debug.Log("Attack " + totalAttacks);
            animator.SetTrigger("Attack");
        }
        
    }*/
    /*public void TakeDamage(float damage)
    {

        Debug.Log(curHealth + "/" + maxHealth);
        curHealth -= damage;
        if (curHealth <= 0)
        {
            // Ragdoll
            Destroy(gameObject);
        }
    }*/
}
