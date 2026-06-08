using UnityEngine;

public class Melee : MonoBehaviour
{
    [SerializeField] private Transform projectileSpawnTransform;
    [SerializeField] private Transform slashSpawnTransform;
    [SerializeField] private GameObject meleeProjectilePrefab;
    [SerializeField] private GameObject slashPrefab;
    //[SerializeField] private MeleeBullet meleeProjectile;

    public void MeleeAttack() {
        Vector3 forward = projectileSpawnTransform.forward * 10f;
        GameObject bullet = Instantiate(meleeProjectilePrefab, projectileSpawnTransform.position, Quaternion.identity);
        bullet.GetComponent<MeleeBullet>().Setup(forward);
        GameObject slashInstance = Instantiate(slashPrefab, slashSpawnTransform.position, slashSpawnTransform.rotation);
        Destroy(slashInstance, 0.5f);
    }
}
