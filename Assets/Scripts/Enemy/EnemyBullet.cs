using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class EnemyBullet : MonoBehaviour
{
    [SerializeField] private int dmg = 17;
    [SerializeField] private float destructionAfterCollision = 0.01f;
    [SerializeField] private GameObject poof;
    private Rigidbody rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Setup(Vector3 direction)
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(direction, ForceMode.Impulse);
        Destroy(gameObject, 3f);
    }
    private void OnCollisionEnter(Collision collision)
    {
        StartCoroutine(CollisionSequence());
        Debug.Log($"{gameObject} collided with {collision.collider.gameObject}");
    }
    public void Hit()
    {
        Destroy(gameObject);
        Debug.Log("Bullet Hit");
    }

    public int DealDmg()
    {
        return dmg;
    }
    private IEnumerator CollisionSequence()
    {
        yield return new WaitForSeconds(destructionAfterCollision);
        GameObject poofing = Instantiate(poof, transform.position, Quaternion.identity);
        Debug.Log("Bullet collision sequence done");
        Destroy(poofing, 0.5f);
        Destroy(gameObject);
    }
}