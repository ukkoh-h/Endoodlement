using System.Collections;

using UnityEngine;
using UnityEngine.InputSystem;

public class Dissolver : MonoBehaviour
{
    public float dissolveDuration = 2;
    public float dissolveStrenght;

    public void StartDissolver()
    {
        StartCoroutine(dissolver());
    }

    public IEnumerator dissolver()
    {
        float elapsedTime = 0;

        Material dissolveMaterial = GetComponent<Renderer>().material;

        while ( elapsedTime < dissolveDuration)
        {
            elapsedTime += Time.deltaTime;

            dissolveStrenght = Mathf.Lerp(0, 1, elapsedTime / dissolveDuration);
            dissolveMaterial.SetFloat("_DissolveStrenght", dissolveStrenght);

            yield return null;
        }

        Destroy(gameObject);
    }
    /*private void Update()
    {
        if (Keyboard.current.digit1Key.wasPressedThisFrame)
        {
            //Debug.Log("was Pressed");
            StartDissolver();
        }
    }*/
}
