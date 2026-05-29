using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private GameObject goblinPrefab;
    [SerializeField] private Transform copterSpawnPoint;
    [SerializeField] private GameObject goblinCopterPrefab;
    [SerializeField] private int managerZone;
    [SerializeField] private bool autoSpawnerActive;
    [SerializeField] private bool copterSpawn;
    [SerializeField] private float spawnCooldown = 5f;
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
        GameObject goblin = Instantiate(goblinPrefab, spawnPoint.position, Quaternion.identity);
        goblin.GetComponent<Enemy>().Activate(managerZone);
    }
    public void SpawnGoblinCopter()
    {
        GameObject goblinCopter = Instantiate(goblinCopterPrefab, copterSpawnPoint.position, Quaternion.identity);
        goblinCopter.GetComponent<EnemyCopter>().Activate(managerZone);
    }
    private IEnumerator AutoSpawnSequence() {
        SpawnGoblin();
        yield return new WaitForSeconds(spawnCooldown/2);
        if(copterSpawn) SpawnGoblinCopter();
        yield return new WaitForSeconds(spawnCooldown/2);
        spawnerActivated = false;
    }
}
