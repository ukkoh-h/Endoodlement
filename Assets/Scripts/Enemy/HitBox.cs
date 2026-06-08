using UnityEngine;

public class HitBox : MonoBehaviour
{
    [SerializeField] private bool isHead;
    [SerializeField] private Enemy goblin;
    [SerializeField] private EnemyCopter goblinCopter;
    [SerializeField] private EnemyMecha goblinMecha;
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
                if(goblinMecha != null)goblinMecha.TakeDamage(dmg);
            }
            if (other.TryGetComponent(out MeleeBullet meleeBullet))
            {
                dmg = meleeBullet.DealMeleeDmg();
                meleeBullet.Hit();
                if(isHead) dmg *= 2;
                if(goblin != null)goblin.TakeMeleeDamage(dmg);
                if(goblinCopter != null)goblinCopter.TakeMeleeDamage(dmg);
                if(goblinMecha != null)goblinMecha.TakeMeleeDamage(dmg);
            }
        }
    }
}
