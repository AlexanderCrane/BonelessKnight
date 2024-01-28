using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SimpleEnemy : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform player;

    public bool countingDown;

    public GameObject ThrownObject;

    public LayerMask whatIsGround, whatIsPlayer;

    public GameManager gameManager;

    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    //Animation
    public Animator animator;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        gameManager = FindObjectOfType<GameManager>();

        if(gameManager == null)
        {
            Debug.LogError("Cannot find Game Manager!");
        }
        // Die();
    }

    private void Update()
    {
        //Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange && !countingDown) StartCoroutine(PatrolCountDown());
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInSightRange && playerInAttackRange) AttackPlayer();

    }
    private void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //WalkPoint reached
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }

    private void SearchWalkPoint()
    {
        //Calculate random point in range
        float randomX = Random.Range(-walkPointRange, walkPointRange);
        float randomZ = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    private void AttackPlayer()
    {
        //Make sure enemy doesn't move
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            //Attack code here
            Rigidbody rb = Instantiate(ThrownObject, new Vector3(transform.position.x, transform.position.y+2f, transform.position.z), Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 700f, ForceMode.Impulse);
            rb.AddForce(transform.up * 200f, ForceMode.Impulse);

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }

    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    private void OnCollisionEnter(Collision other) {
        Debug.Log("Collided with " + other.gameObject.name);
        if(other.gameObject.layer == LayerMask.NameToLayer("BonelessKnight"))
        {
            Debug.Log("Collided with sword");
            Die();
        }
    }

    public void Die()
    {
        Transform[] children = GetComponentsInChildren<Transform>(true); // the (true) parameter means we include even inactive objects in the search
        foreach(Transform t in children) {
            Debug.Log("Checking transform for " + t.gameObject.name);
            if(t != t.root)
            {
                t.gameObject.SetActive(!t.gameObject.activeInHierarchy); // flip the active state of the object
            }
            else
            {
                agent.enabled = false;
                t.GetComponent<CapsuleCollider>().enabled = false;
                animator.enabled = false;
            }
        }
        StopAllCoroutines();

        gameManager.ReduceSkeletonCount();

        this.enabled = false;
    }

    private IEnumerator PatrolCountDown()
    {
        yield return new WaitForSeconds(Random.Range(5, 10));
        Patroling();
        countingDown = false;
    }
}