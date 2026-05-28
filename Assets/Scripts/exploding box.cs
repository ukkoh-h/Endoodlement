using UnityEngine;

public class explode : MonoBehaviour
{
    [SerializeField]
    private GameObject brokenCube;

    public void SetToBroken()
    {

        brokenCube.SetActive(true);
        gameObject.SetActive(false);
    }
}