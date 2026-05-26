using UnityEngine;

public class RespawnPoint : MonoBehaviour
{
    [SerializeField] private string playerTag = "Player";

    private bool activated = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!activated && other.CompareTag(playerTag))
        {
            RespawnManager.Instance.SetSpawnPoint(transform.position);

            activated = true;

            Debug.Log("Checkpoint Activated");
        }
    }
}
