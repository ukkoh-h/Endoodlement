using UnityEngine;
using StarterAssets;

public class Water : MonoBehaviour
{
    private Transform player;
    private void Awake()
    {
        player = GameObject.Find("Player").transform;
    }
    private void OnCollisionEnter(Collision collision)
    {
        player.GetComponent<FirstPersonController>().TakeDmg(100);
    }
}
