using StarterAssets;
using Unity.VisualScripting;
using UnityEngine;

public class Drops : MonoBehaviour
{

    //[SerializeField] private Gun gun;
    [SerializeField] private bool ammo;
    [SerializeField] private bool heal;
    [SerializeField] private bool money;
    private GameObject gun;
    private void Awake()
    {
        if (ammo)gun = GameObject.Find("GunFunctional");
    }
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("got hit");
        if(other.CompareTag("Player"))
        {
            if (!ammo && other.TryGetComponent(out FirstPersonController player))
            {
                /*if(heal) player.Heal();
                else player.GetMoney();*/
            }
            else if (ammo)
            {
                gun.GetComponent<Gun>().GetRandomAmmo();
            }
            Destroy(gameObject);
        }
    }
}
