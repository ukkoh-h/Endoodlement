using StarterAssets;
using UnityEngine;

public class PlayerHitBox : MonoBehaviour
{
    [SerializeField] private FirstPersonController player;
    private int dmg;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("EnemyBullet"))
        {
            Debug.Log("I got hit");
            if (other.TryGetComponent(out EnemyBullet bullet))
            {
                dmg = bullet.DealDmg();
                bullet.Hit();
                player.TakeDmg(dmg);
            }
        }
    }
}
