using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private int dmg = 3;
    [SerializeField] private float destructionAfterCollision = 0.2f;
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
    }

    public void SetDmg(int weaponDmg)
    {
        dmg = weaponDmg;
    }
    public void Hit()
    {
        Destroy(gameObject);
    }

    public int DealDmg()
    {
        return dmg;
    }
    private IEnumerator CollisionSequence()
    {
        yield return new WaitForSeconds(destructionAfterCollision);
        Destroy(gameObject);
    }
}
