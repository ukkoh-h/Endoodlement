using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private GameObject goblinPrefab;
    [SerializeField] private Transform copterSpawnPoint;
    [SerializeField] private GameObject goblinCopterPrefab;
    // Update is called once per frame
    void Update()
    {
        
    }
    public void SpawnGoblin()
    {
        GameObject goblin = Instantiate(goblinPrefab, spawnPoint.position, Quaternion.identity);
        goblin.GetComponent<Enemy>().Activate();
    }
    public void SpawnGoblinCopter()
    {
        GameObject goblinCopter = Instantiate(goblinCopterPrefab, copterSpawnPoint.position, Quaternion.identity);
        goblinCopter.GetComponent<EnemyCopter>().Activate();
    }
}
