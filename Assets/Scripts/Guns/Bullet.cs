using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private int dmg = 3;
    private Rigidbody rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Setup(Vector3 direction)
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(direction, ForceMode.Impulse);
        Destroy(gameObject, 3f);
    }

    /*private void OggerEnter(BoxCollider collision)
    {
        if (collision.TryGetComponent(out Enemy target))
        {
            target.TakeDamage(dmg);
            Destroy(gameObject);
        }
    }*/

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
}
