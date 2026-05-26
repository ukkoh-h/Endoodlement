using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    public void Respawn()
    {
        RespawnManager.Instance.RespawnPlayer(gameObject);
    }
}
