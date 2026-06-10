using UnityEngine;

public class MechaSpawner : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private EnemyManager enemyManager;
    [SerializeField] private GameObject mechaPrefab;
    [SerializeField] private LayerMask groundLayer, playerLayer;
    private bool spawnerActivated;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void SpawnMecha()
    {
        //bool playerTooClose = Physics.CheckSphere(transform.position, 5, playerLayer);
        //if (!playerTooClose) {
            GameObject mecha = Instantiate(mechaPrefab, spawnPoint.position, Quaternion.identity);
            mecha.GetComponent<EnemyMecha>().Activate();
            enemyManager.MechaSpawned();
        //}
    }
}
