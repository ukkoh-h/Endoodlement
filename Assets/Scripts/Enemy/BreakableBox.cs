using System.Collections;
using UnityEngine;

public class BreakableBox : MonoBehaviour
{
    [SerializeField] private GameObject wholeBox;
    [SerializeField] private GameObject boxBroken;
    [SerializeField] private Transform poofSpot;
    [SerializeField] private GameObject poof;
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
    private IEnumerator CleanUpSequence()
    {
        yield return new WaitForSeconds(0.5f);
        wholeBox.SetActive(false);
        boxBroken.SetActive(true);
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
}
