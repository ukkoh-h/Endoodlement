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
    //[SerializeField] private GameObject bodyHitbox;
    //[SerializeField] private GameObject headHitbox;
    [SerializeField] private float attackRange;
    [SerializeField] private float timeBetweenAttacks;
    [SerializeField] private int hitPoints;
    [SerializeField] private LayerMask groundLayer, playerLayer;
    [SerializeField] private bool isActive;

    private bool isAttacking;
    private bool isWalking;

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
        //Debug.Log(playerInAttackRange);
        if (isActive && playerInAttackRange)
        {
            if(isWalking) sprite.Walk();
            TryAttackPlayer();
        } 
        else if (isActive)
        {
            if(!isWalking) sprite.Walk();
            ChasePlayer();
        }
        
    }
    public void TakeDamage(int dmg)
    {
        hitPoints -= dmg;
        sprite.Hit();
        if (hitPoints <= 0) Destroy(gameObject);
    }
    private void ChasePlayer()
    {
        navAgent.SetDestination(player.position);
        isWalking = true;
    }
    private void TryAttackPlayer()
    {
        navAgent.SetDestination(transform.position);
        isWalking = false;

        if (!isAttacking)
        {
            StartCoroutine(AttackSequence());
        }
    }

    private IEnumerator AttackSequence()
    {
        isAttacking = true;
        yield return new WaitForSeconds(0.5f);
        bool playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, playerLayer);
        sprite.Attack();
        //if(playerInAttackRange) player.TakeDmg();
        yield return new WaitForSeconds(timeBetweenAttacks);

        isAttacking = false;
    }
}
