using UnityEngine;

public class MeleeBullet : MonoBehaviour
{
    [SerializeField] private int dmg = 2;
    private Rigidbody rb;

    public void Setup(Vector3 direction)
    {
        //Debug.Log("melee shot");
        rb = GetComponent<Rigidbody>();
        rb.AddForce(direction, ForceMode.Impulse);
        Destroy(gameObject, 0.15f);
    }

    public void Hit()
    {
        Destroy(gameObject);
    }

    public int DealMeleeDmg()
    {
        return dmg;
    }
}
