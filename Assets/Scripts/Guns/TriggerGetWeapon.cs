using UnityEngine;

public class TriggerGetWeapon : MonoBehaviour
{
    private bool isActive = true;
    private Transform gun;

    private void Awake()
    {
        gun = GameObject.Find("GunFunctional").transform;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(isActive && other.CompareTag("Player"))
        {
            isActive = false;
            gun.GetComponent<Gun>().GetWeapon();
        }
    }
}
