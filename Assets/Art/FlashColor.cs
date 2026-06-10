using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.WSA;

public class FlashColor : MonoBehaviour
{
    public SpriteRenderer color;
    //public Render meshColor;
    public Color flashColor = Color.red;
    public float flashDuration = 0.1f;

    private Color originalColor;
    private Material rend;

    private void Start()
    {
        /*if(color != null)*/rend = color.material;
        //else if(meshColor != null)rend = meshColor.material;
        //rend = gameObject.GetComponent(SpriteRenderer);
        originalColor = rend/*.material*/.color;
    }

    private IEnumerator DoFlash()
    {
        rend/*.material*/.color = flashColor;
        yield return new WaitForSeconds(flashDuration);
        rend/*.material*/.color = originalColor;
    }
    private IEnumerator DoFlashing()
    {
        for(int i = 0; i < 1000; i++)
        {
            rend/*.material*/.color = flashColor;
            yield return new WaitForSeconds(flashDuration);
            rend/*.material*/.color = originalColor;
            yield return new WaitForSeconds(flashDuration);
        }
    }

    public void Flash()
    {
        //Debug.Log("Flashing");
        StopAllCoroutines();
        StartCoroutine(DoFlash());
    }
    public void Flashing()
    {
        //Debug.Log("Flashing");
        StopAllCoroutines();
        StartCoroutine(DoFlashing());
    }
    public void StopFlashing()
    {
        StopAllCoroutines();
    }

    /*private void Update()
    {
        if(Keyboard.current.digit1Key.wasPressedThisFrame)
        {
            //Debug.Log("was Pressed");
            Flash();
        }
    }*/
}
