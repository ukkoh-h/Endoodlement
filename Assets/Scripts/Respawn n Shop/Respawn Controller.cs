using UnityEngine;

public class RespawnController : MonoBehaviour
{
    [SerializeField] private float respawnTrigger = 8f;
    public Transform respawnpoint;
    private GameObject _player;

    public static RespawnController Instance;
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    private void FixedUpdate()
    {
        if(Physics.Raycast(transform.position, _player.transform.position - transform.position, out RaycastHit hit, Mathf.Infinity, layerMask))
        {
            if(hit.collider.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                hit.transform.position = respawnpoint.position;
            }
        }
    }

}
