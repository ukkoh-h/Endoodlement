using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    private Rigidbody rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Setup(Vector3 direction)
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(direction, ForceMode.Impulse);
        Destroy(gameObject, 3f);
    }

    /*private void OggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Target target))
        {
            target.Damage();
            Destroy(gameObject);
        }
    }*/
}
