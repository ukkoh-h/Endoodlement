using UnityEngine;
using System.Collections;
using StarterAssets;

public class ExplosiveEnemyBullet : MonoBehaviour
{
    [SerializeField] private int dmg = 3;
    [SerializeField] private float destructionAfterCollision = 0.01f;
    [SerializeField] private GameObject poof;
    [SerializeField] private LayerMask groundLayer, playerLayer;
    private Rigidbody rb;
    private Transform player;
    private void Awake()
    {
        player = GameObject.Find("PlayerCapsule").transform;
    }
    public void Setup(Vector3 direction)
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(direction, ForceMode.Impulse);
        Destroy(gameObject, 3f);
    }
    private void OnCollisionEnter(Collision collision)
    {
        StartCoroutine(CollisionSequence());
    }
    public void Hit()
    {
        StartCoroutine(CollisionSequence());
    }

    public int DealDmg()
    {
        return dmg;
    }
    private IEnumerator CollisionSequence()
    {
        bool playerInAttackRange = Physics.CheckSphere(transform.position, 3f, playerLayer);
        if(playerInAttackRange) player.GetComponent<FirstPersonController>().TakeDmg(dmg);
        yield return new WaitForSeconds(destructionAfterCollision);
        GameObject poofing = Instantiate(poof, transform.position, Quaternion.identity);
        Destroy(poofing, 0.5f);
        Destroy(gameObject);
    }
}
