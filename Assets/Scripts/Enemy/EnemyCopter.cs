using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyCopter : MonoBehaviour
{
    [SerializeField] private NavMeshAgent navAgent;
    [SerializeField] private Transform player;
    [SerializeField] private CopterBillboarding sprite;
    //[SerializeField] public GobAttack gobAttack;
    [SerializeField] private GameObject projectile;
    [SerializeField] private Transform slingshotTransform;
    //[SerializeField] private GameObject bodyHitbox;
    //[SerializeField] private GameObject headHitbox;
    [SerializeField] private float attackRangeUpper;
    [SerializeField] private float attackRangeLower;
    [SerializeField] private float timeBetweenAttacks;
    [SerializeField] private int hitPoints;
    [SerializeField] private LayerMask groundLayer, playerLayer;
    [SerializeField] private bool isActive;

    private bool isAttacking;
    private bool escaping;
    private bool approaching;
    private bool rotating;
    private bool rotDirSet;
    private Vector3 escapeDirection;
    //float rotation;
    private Vector3 rotationDirection;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        navAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        bool playerInAttackRange = Physics.CheckSphere(transform.position, attackRangeUpper, playerLayer);
        bool playerTooClose = Physics.CheckSphere(transform.position, attackRangeLower, playerLayer);

        bool playerInSweetSpot = Physics.CheckSphere(transform.position, (attackRangeUpper-attackRangeLower)/2+attackRangeLower, playerLayer);
        if(rotating)
        {
            //Debug.Log("rotating");
            if(!rotDirSet)SetRotationDirection();
            RotateAroundPlayer();
        }
        if (isActive && playerTooClose)
        {
            //Debug.Log("escaping");
            SetEscapeDirection();
            EscapePlayer();
            if(approaching)
            {
                approaching = false;
                sprite.Approach();
            }
            if(!escaping)
            {
                escaping = true;
                sprite.Escape();
            }
        }
        else if (isActive && playerInAttackRange)
        {
            if (escaping)
            {
                escaping = false;
                sprite.Escape();
            }
            else if (approaching)
            {
                approaching = false;
                sprite.Approach();
            }
            TryAttackPlayer();
            if (playerInSweetSpot) 
            {
                EscapePlayer();
                if(approaching)
                {
                    approaching = false;
                    sprite.Approach();
                }
                if(!escaping)
                {
                    escaping = true;
                    sprite.Escape();
                }
            }
            /*else if (!playerInSweetSpot)
            {
                ChasePlayer();
                if(!approaching)
                {
                    approaching = true;
                    sprite.Approach();
                }
                if(escaping)
                {
                    escaping = false;
                    sprite.Escape();
                }
            }*/
        }
        else if (isActive)
        {
            ChasePlayer();
            if(!approaching)
            {
                approaching = true;
                sprite.Approach();
            }
            if(escaping)
            {
                escaping = false;
                sprite.Escape();
            }
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
        AudioManager.Instance.PlaySFX("GobFly");
    }
    private void SetEscapeDirection()
    {
        //escapeDirection = new Vector3(player.position.x + transform.position.x * 3, transform.position.y, player.position.z + transform.position.z * 3);
        escapeDirection = Vector3.Normalize(player.position - transform.position) * -3f;
        Debug.DrawRay(transform.position, escapeDirection, Color.magenta);
    }
    private void SetRotationDirection()
    {
        //escapeDirection = new Vector3(player.position.x + transform.position.x * 3, transform.position.y, player.position.z + transform.position.z * 3)
        
        //rotationDirection = new Vector3(transform.position.x-player.position.z , transform.position.y, transform.position.z-player.position.x);
        rotDirSet = true;

        float randomZ = Random.Range(-5f, 5f);
        float randomX = Random.Range(-5f, 5f);
        rotationDirection = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
        //Debug.Log(rotationDirection);
        //sprite.Rotating(randomX, randomZ, timeBetweenAttacks);

        
        Debug.DrawRay(transform.position, rotationDirection, Color.orangeRed);
    }
    private void EscapePlayer()
    {
        //escapeDirection = new Vector3(player.position.x + transform.position.x * 3, transform.position.y, player.position.z + transform.position.z * 3);
        navAgent.SetDestination(transform.position + escapeDirection);
    }
    private void RotateAroundPlayer()
    {
        navAgent.SetDestination(/*transform.position +*/ rotationDirection);
    }
    private void TryAttackPlayer()
    {
        //navAgent.SetDestination(transform.position);

        if (!isAttacking)
        {
            StartCoroutine(AttackSequence());
        }
    }

    private IEnumerator AttackSequence()
    {
        isAttacking = true;
        navAgent.SetDestination(transform.position);
        yield return new WaitForSeconds(0.5f);
        //bool playerInAttackRange = Physics.CheckSphere(transform.position, attackRangeUpper, playerLayer);
        sprite.Attack();
        Vector3 forward = slingshotTransform.forward * 20f;
        GameObject bullet = Instantiate(projectile, slingshotTransform.position, Quaternion.identity);
        bullet.GetComponent<EnemyBullet>().Setup(forward);
        yield return new WaitForSeconds(0.5f);
        rotating = true;
        //if(playerInAttackRange) player.TakeDmg();
        yield return new WaitForSeconds(timeBetweenAttacks);

        rotDirSet = false;
        rotating = false;
        isAttacking = false;
    }
        private IEnumerator DyingSequence()
    {
        isActive = false;
        sprite.Dying();
        float deathTimer = Random.Range(5f, 7f);
        yield return new WaitForSeconds(deathTimer);

        Death(false);
    }
        private void Death(bool byMelee)
    {
        //Tänne loot dropit ja kuolema animaatiot
        //if (byMelee) ;
        Destroy(gameObject);
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRangeLower);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRangeUpper);
    }
    public void Activate()
    {
        isActive = true;
    }
}

