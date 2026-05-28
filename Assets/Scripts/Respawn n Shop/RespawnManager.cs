using StarterAssets;
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
        CharacterController controller = player.GetComponent<CharacterController>();

        if (controller != null)
        {
            controller.enabled = false;
        }

        player.transform.position = currentSpawnPoint + Vector3.up + Vector3.right + Vector3.right;
        FirstPersonController.instance.hitPoints = 5;

        if (controller != null)
        {
            controller.enabled = true;
        }
    }
}