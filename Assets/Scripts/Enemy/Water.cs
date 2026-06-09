using UnityEngine;
using StarterAssets;

public class Water : MonoBehaviour
{
    private Transform player;
    private void Awake()
    {
        player = GameObject.Find("PlayerCapsule").transform;
    }
    private void OnTriggerEnter(Collider collision)
    {
        //Debug.Log("entered");
        player.GetComponent<FirstPersonController>().TakeDmg(100);
    }
}
