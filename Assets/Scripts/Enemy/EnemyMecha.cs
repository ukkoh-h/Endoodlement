using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class EnemyMecha : MonoBehaviour
{
[SerializeField] private NavMeshAgent navAgent;
    [SerializeField] private Transform player;
    //[SerializeField] private CopterBillboarding sprite;
    [SerializeField] private Animator mechaAnimator;
    [SerializeField] private FlashColor flash;
    [SerializeField] private EnemyManager enemyManager;
    //[SerializeField] public GobAttack gobAttack;
    [SerializeField] private GameObject projectile;
    [SerializeField] private Transform canonTransform1;
    [SerializeField] private Transform canonTransform2;
    [SerializeField] private float attackRangeUpper;
    [SerializeField] private float attackRangeLower;
    [SerializeField] private float timeBetweenAttacks;
    [SerializeField] private int hitPoints;
    [SerializeField] private LayerMask groundLayer, playerLayer;
    [SerializeField] private bool isActive;
    [SerializeField] private GameObject body;
    [SerializeField] private GameObject ball;
    [SerializeField] private GameObject shot;
    [SerializeField] private GameObject poof;
    [SerializeField] private GameObject ammoDrop;
    [SerializeField] private GameObject healthDrop;
    [SerializeField] private GameObject moneyDrop;
    


    private bool isAttacking;
    private bool retreating;
    private bool approaching;
    private bool rotating;
    //private bool rotDirSet;
    private bool isDying;
    private Vector3 escapeDirection;
    //float rotation;
    //private Vector3 rotationDirection;
    
    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        navAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 relativePos = Vector3.Normalize(player.position - transform.position);
        Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.time * 0.05f);

        bool playerInAttackRange = Physics.CheckSphere(transform.position, attackRangeUpper, playerLayer);
        bool playerTooClose = Physics.CheckSphere(transform.position, attackRangeLower, playerLayer);

        bool playerInSweetSpot = Physics.CheckSphere(transform.position, (attackRangeUpper-attackRangeLower)/2+attackRangeLower, playerLayer);
        if(rotating)
        {
            //Debug.Log("rotating");
            //if(!rotDirSet)SetRotationDirection();
            //RotateAroundPlayer();
        }
        if (isActive && playerTooClose)
        {
            //Debug.Log("escaping");
            SetEscapeDirection();
            Retreat();
            if(approaching)
            {
                approaching = false;
            }
            if(!retreating)
            {
                retreating = true;
            }
        }
        else if (isActive && playerInAttackRange)
        {
            if (retreating)
            {
                retreating = false;
            }
            else if (approaching)
            {
                approaching = false;
            }
            TryAttackPlayer();
            if (playerInSweetSpot) 
            {
                Retreat();
                if(approaching)
                {
                    approaching = false;
                }
                if(!retreating)
                {
                    retreating = true;
                }
            }
        }
        else if (isActive)
        {
            ChasePlayer();
            if(!approaching)
            {
                approaching = true;
            }
            if(retreating)
            {
                retreating = false;
            }
        }
        
    }
    public void TakeDamage(int dmg)
    {
        hitPoints -= dmg;
        flash.Flash();
        if (hitPoints <= 0 && isActive) StartCoroutine(DyingSequence());
        if (hitPoints <= -5 && !isActive) Death(false);
    }
    public void TakeMeleeDamage(int dmg)
    {
        hitPoints -= dmg;
        flash.Flash();
        if (hitPoints <= 0) StartCoroutine(DyingSequence());
        if (hitPoints <= 0 && !isActive) Death(true);
    }
    private void ChasePlayer()
    {
        navAgent.SetDestination(player.position);
       
    }


    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {

            //AudioManager.Instance.PlayAmb("Chopper");
            
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            //AudioManager.Instance.StopAmb();
            return;
        }
        
    }


    private void SetEscapeDirection()
    {
        //escapeDirection = new Vector3(player.position.x + transform.position.x * 3, transform.position.y, player.position.z + transform.position.z * 3);
        escapeDirection = Vector3.Normalize(player.position - transform.position) * -3f;
        Debug.DrawRay(transform.position, escapeDirection, Color.magenta);
    }
    /*private void SetRotationDirection()
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
    }*/
    private void Retreat()
    {
        //escapeDirection = new Vector3(player.position.x + transform.position.x * 3, transform.position.y, player.position.z + transform.position.z * 3);
        navAgent.SetDestination(transform.position + escapeDirection);
    }
    /*private void RotateAroundPlayer()
    {
        navAgent.SetDestination( rotationDirection);
    }*/
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
        mechaAnimator.Play("Armature|shoot");
        yield return new WaitForSeconds(0.5f);
        //bool playerInAttackRange = Physics.CheckSphere(transform.position, attackRangeUpper, playerLayer);
        Vector3 forward1 = canonTransform1.forward * 20f;
        GameObject bullet1 = Instantiate(projectile, canonTransform1.position, Quaternion.identity);
        bullet1.GetComponent<EnemyBullet>().Setup(forward1);
        GameObject shooting1 = Instantiate(shot, canonTransform1.position, canonTransform1.rotation);
        Destroy(shooting1, 1f);

        Vector3 forward2 = canonTransform2.forward * 20f;
        GameObject bullet2 = Instantiate(projectile, canonTransform2.position, Quaternion.identity);
        bullet2.GetComponent<EnemyBullet>().Setup(forward2);
        GameObject shooting2 = Instantiate(shot, canonTransform2.position, canonTransform2.rotation);
        Destroy(shooting2, 1f);
        rotating = true;
        //if(playerInAttackRange) player.TakeDmg();
        yield return new WaitForSeconds(timeBetweenAttacks);

        rotating = false;
        isAttacking = false;
    }
    private IEnumerator DyingSequence()
    {
        isActive = false;
        float deathTimer = Random.Range(5f, 7f);
        yield return new WaitForSeconds(deathTimer);

        Death(false);
    }
    private IEnumerator DeathSequence()
    {
        isDying = true;
        GameObject poofing = Instantiate(poof, body.transform.position, Quaternion.identity);
        Destroy(poofing, 0.5f);
        body.SetActive(false);
        GameObject balling = Instantiate(ball, transform.position, Quaternion.identity);
        Destroy(balling, 3f);
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
    private void Death(bool byMelee)
    {
        //Tänne loot dropit ja kuolema animaatiot
        //if (byMelee) ;
        if (enemyManager != null)enemyManager.CopterDead();
        AudioManager.Instance.StopAmb();
        if(byMelee)
        {
            Instantiate(ammoDrop, GetRandomPosition(), Quaternion.identity);
            Instantiate(healthDrop, GetRandomPosition(), Quaternion.identity);
            Instantiate(moneyDrop, GetRandomPosition(), Quaternion.identity);
        }
        Instantiate(ammoDrop, GetRandomPosition(), Quaternion.identity);
        Instantiate(healthDrop, GetRandomPosition(), Quaternion.identity);
        Instantiate(moneyDrop, GetRandomPosition(), Quaternion.identity);

        if(!isDying)StartCoroutine(DeathSequence());
        
    }
    private Vector3 GetRandomPosition()
    {   
        float spreadX = Random.Range(-3f, 3f);
        float spreadz = Random.Range(-3f, 3f);

        return new Vector3(transform.position.x + spreadX, transform.position.y+1f, transform.position.z + spreadz);
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
