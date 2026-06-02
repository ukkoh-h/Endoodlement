using UnityEngine;

public class HitBox : MonoBehaviour
{
    [SerializeField] private bool isHead;
    [SerializeField] private Enemy goblin;
    [SerializeField] private EnemyCopter goblinCopter;
    private int dmg;
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("got hit");
        if(other.CompareTag("Bullet"))
        {
            if (other.TryGetComponent(out Bullet bullet))
            {
                dmg = bullet.DealDmg();
                bullet.Hit();
                if(isHead) dmg *= 2;
                if(goblin != null)goblin.TakeDamage(dmg);
                if(goblinCopter != null)goblinCopter.TakeDamage(dmg);
            }
        }
    }
}
