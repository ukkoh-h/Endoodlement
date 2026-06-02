using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private EnemyManager enemyManager;
    [SerializeField] private GameObject goblinPrefab;
    [SerializeField] private Transform copterSpawnPoint;
    [SerializeField] private GameObject goblinCopterPrefab;
    //[SerializeField] private int managerZone;
    [SerializeField] private bool autoSpawnerActive;
    [SerializeField] private bool copterSpawn;
    [SerializeField] private float spawnCooldown = 5f;
    [SerializeField] private LayerMask groundLayer, playerLayer;
    private bool spawnerActivated;
    // Update is called once per frame
    void Start()
    {
        
    }
    void Update() {
        if (autoSpawnerActive && !spawnerActivated) 
        {
            spawnerActivated=true;
            StartCoroutine(AutoSpawnSequence());
        }
    }
    public void SpawnGoblin()
    {
        bool playerTooClose = Physics.CheckSphere(transform.position, 2, playerLayer);
        if (!playerTooClose) {
            GameObject goblin = Instantiate(goblinPrefab, spawnPoint.position, Quaternion.identity);
            goblin.GetComponent<Enemy>().Activate();
            enemyManager.GoblinSpawned();
        }
    }
    public void SpawnGoblinCopter()
    {
        bool playerTooClose = Physics.CheckSphere(transform.position, 2, playerLayer);
        if (!playerTooClose) {
            GameObject goblinCopter = Instantiate(goblinCopterPrefab, copterSpawnPoint.position, Quaternion.identity);
            goblinCopter.GetComponent<EnemyCopter>().Activate();
            enemyManager.CopterSpawned();
        }
    }
    private IEnumerator AutoSpawnSequence() {
        SpawnGoblin();
        yield return new WaitForSeconds(spawnCooldown/2);
        if(copterSpawn) SpawnGoblinCopter();
        yield return new WaitForSeconds(spawnCooldown/2);
        spawnerActivated = false;
    }
}
