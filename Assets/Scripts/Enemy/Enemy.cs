using System.Collections;
using StarterAssets;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.VFX;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private NavMeshAgent navAgent;
    [SerializeField] private Transform player;
    [SerializeField] private Transform slashPlacement;
    [SerializeField] private FirstPersonController playerC;
    [SerializeField] private Billboarding sprite;
    //[SerializeField] public GobAttack gobAttack;
    //[SerializeField] private GameObject bodyHitbox;
    //[SerializeField] private GameObject headHitbox;
    [SerializeField] private float attackRange;
    [SerializeField] private float timeBetweenAttacks;
    [SerializeField] private int hitPoints;
    [SerializeField] private LayerMask groundLayer, playerLayer;
    [SerializeField] private bool isActive;
    [SerializeField] private GameObject slash;

    private bool isAttacking;
    private bool isWalking;
    private int eMNum;

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
            TryAttackPlayer();
        } 
        else if (isActive)
        {
            Debug.Log("chasing");
            ChasePlayer();
        }
        
    }
    public void TakeDamage(int dmg)
    {
        hitPoints -= dmg;
        sprite.Hit();
        if (hitPoints <= 0 && isActive) StartCoroutine(DyingSequence());
        if (hitPoints <= -5 && !isActive) Death(false);
    }
    public void TakeMeleeDamage(int dmg)
    {
        hitPoints -= dmg;
        sprite.Hit();
        if (hitPoints <= 0) StartCoroutine(DyingSequence());
        if (hitPoints <= 0 && !isActive) Death(true);
    }
    private void ChasePlayer()
    {
        navAgent.SetDestination(player.position);
        Debug.Log(player.position);
        if(!isWalking) 
        {
            isWalking = true;
            sprite.Walk();
        }
    }
    private void TryAttackPlayer()
    {
        navAgent.SetDestination(transform.position);
        if(isWalking) 
        {
            isWalking = false;
            sprite.Walk();
        }

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
        GameObject slashing = Instantiate(slash, slashPlacement);
        Destroy(slashing, Mathf.Min(0.03f));
        if(playerInAttackRange) playerC.TakeDmg(2);
        yield return new WaitForSeconds(timeBetweenAttacks);
        AudioManager.Instance.PlaySwing();
        isAttacking = false;
    }
    private IEnumerator DyingSequence()
    {
        isActive = false;
        if(isWalking) 
        {
            isWalking = false;
            sprite.Walk();
        }
        sprite.Dying();
        float deathTimer = Random.Range(3f, 5f);
        yield return new WaitForSeconds(deathTimer);

        Death(false);
    }
    private void Death(bool byMelee)
    {
        //Tänne loot dropit ja kuolema animaatiot
        //if (byMelee) ;
        Destroy(gameObject);
    }
    public void Activate(int num)
    {
        isActive = true;
        eMNum = num;
    }
}
