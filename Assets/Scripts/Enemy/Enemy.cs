using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private NavMeshAgent navAgent;
    [SerializeField] private Transform player;
    [SerializeField] private Billboarding sprite;
    //[SerializeField] public GobAttack gobAttack;
    [SerializeField] private GameObject attackHitbox;
    [SerializeField] private float attackRange;
    [SerializeField] private float timeBetweenAttacks;
    [SerializeField] private LayerMask groundLayer, playerLayer;
    [SerializeField] private bool isActive;

    private bool isAttacking;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        navAgent = GetComponent<NavMeshAgent>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bool playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, playerLayer);
        Debug.Log(playerInAttackRange);
        if (isActive && playerInAttackRange)
        {
            TryAttackPlayer();
        } 
        else if (isActive)
        {
            ChasePlayer();
            //sprite.Walk();
        }
        
    }
    private void ChasePlayer()
    {
        navAgent.SetDestination(player.position);
        //navAgent.isStopped = false; // Add this line?
    }
    private void TryAttackPlayer()
    {
        navAgent.SetDestination(transform.position);

        if (!isAttacking)
        {
            StartCoroutine(AttackSequence());
        }
    }

    private IEnumerator AttackSequence()
    {
        isAttacking = true;
        sprite.Attack();
        attackHitbox.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        attackHitbox.SetActive(false);
        yield return new WaitForSeconds(timeBetweenAttacks);

        isAttacking = false;
    }
}
