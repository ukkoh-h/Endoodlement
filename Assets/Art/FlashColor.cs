using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.WSA;

public class FlashColor : MonoBehaviour
{
    public SpriteRenderer color;
    public Color flashColor = Color.red;
    public float flashDuration = 0.1f;

    private Color originalColor;
    private Material rend;

    private void Start()
    {
        rend = color.material;
        //rend = gameObject.GetComponent(SpriteRenderer);
        originalColor = rend/*.material*/.color;
    }

    private IEnumerator DoFlash()
    {
        rend/*.material*/.color = flashColor;
        yield return new WaitForSeconds(flashDuration);
        rend/*.material*/.color = originalColor;
    }

    public void Flash()
    {
        //Debug.Log("Flashing");
        StopAllCoroutines();
        StartCoroutine(DoFlash());
    }

    private void Update()
    {
        if(Keyboard.current.digit1Key.wasPressedThisFrame)
        {
            //Debug.Log("was Pressed");
            Flash();
        }
    }
}
