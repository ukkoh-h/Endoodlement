/*using System.Collections;
using UnityEngine.InputSystem;
using UnityEngine;
using UnityEngine.XR.WSA;

public class FlashColor : MonoBehaviour
{
    public Renderer rend;
    public Color flashColor = Color.red;
    public float flashDuration = 0.1f;

    private Color originalColor;
    private InputAction hurtAction;


    private void Start()
    {
        originalColor = rend.material.color;
        InputActionMap playerActionMap = actionMap.FindActionMap("Player");
    }

    private IEnumerator DoFlash()
    {
        rend.material.color = flashColor;
        yield return new WaitForSeconds(flashDuration);
        rend.material.color = originalColor;
    }

    public void Flash()
    {
        StopAllCoroutines();
        StartCoroutine(DoFlash());
    }

    private void Update()
    {
        if(playerActionMap.FindAction("Jump"))
        {
            Flash();
        }
    }
}*/
