using System.Collections;
using UnityEngine;

public class BreakableBox : MonoBehaviour
{
    [SerializeField] private GameObject wholeBox;
    [SerializeField] private GameObject boxBroken;
    [SerializeField] private Transform poofSpot;
    [SerializeField] private GameObject poof;
    [SerializeField] private GameObject ammo;
    [SerializeField] private GameObject health;
    [SerializeField] private GameObject money;
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("got hit");
        if(other.CompareTag("Bullet"))
        {
            BreakBox();
            if (other.TryGetComponent(out Bullet bullet))
            {
                bullet.Hit();
            }
        }
    }
    public void TakeMeleeDamage(int dmg)
    {
        BreakBox();
    }
    private void BreakBox()
    {
        GameObject poofing = Instantiate(poof, poofSpot.position, Quaternion.identity);
        Destroy(poofing, 1f);
        wholeBox.SetActive(false);
        boxBroken.SetActive(true);
        StartCoroutine(CleanUpSequence());
    }
    private Vector3 GetRandomPosition()
    {   
        float spreadX = Random.Range(-3f, 3f);
        float spreadz = Random.Range(-3f, 3f);

        return new Vector3(poofSpot.position.x + spreadX, poofSpot.position.y, poofSpot.position.z + spreadz);
    }
    private IEnumerator CleanUpSequence()
    {
        yield return new WaitForSeconds(0.5f);
        wholeBox.SetActive(false);
        boxBroken.SetActive(true);
        Instantiate(ammo, GetRandomPosition(), Quaternion.identity);
        Instantiate(health, GetRandomPosition(), Quaternion.identity);
        Instantiate(money, GetRandomPosition(), Quaternion.identity);
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
