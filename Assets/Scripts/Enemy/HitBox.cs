using UnityEngine;

public class HitBox : MonoBehaviour
{
    [SerializeField] private bool isHead;
    [SerializeField] private Enemy goblin;
    private int dmg;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("got hit");
        if(other.CompareTag("Bullet"))
        {
            if (other.TryGetComponent(out Bullet bullet))
            {
                dmg = bullet.DealDmg();
                if(isHead) dmg *= 2;
                goblin.TakeDamage(dmg);
            }
        }
    }
}
