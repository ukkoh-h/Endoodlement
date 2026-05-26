using UnityEngine;


public class RespawnManager : MonoBehaviour
{
    public static RespawnManager Instance;

    [SerializeField] private Transform defaultSpawnPoint;

    private Vector3 currentSpawnPoint;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        currentSpawnPoint = defaultSpawnPoint.position;
    }

    public void SetSpawnPoint(Vector3 newSpawnPoint)
    {
        currentSpawnPoint = newSpawnPoint;

        Debug.Log("New Spawn Point Set: " + currentSpawnPoint);
    }

    public void RespawnPlayer(GameObject player)
    {
        player.transform.position = currentSpawnPoint;

        Rigidbody rb = player.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }
}