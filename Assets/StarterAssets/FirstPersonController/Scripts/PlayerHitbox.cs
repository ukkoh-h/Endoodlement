using StarterAssets;
using UnityEngine;

public class PlayerHitBox : MonoBehaviour
{
    [SerializeField] private FirstPersonController player;
    private int dmg;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("I got hit");
        if(other.CompareTag("EnemyBullet"))
        {
            if (other.TryGetComponent(out EnemyBullet bullet))
            {
                dmg = bullet.DealDmg();
                bullet.Hit();
                player.TakeDmg(dmg);
            }
        }
    }
}
