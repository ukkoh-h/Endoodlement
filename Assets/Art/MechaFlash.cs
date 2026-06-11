using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MechaFlash : MonoBehaviour
{
    public Renderer[] meshes;
    public List<Material> materials;
    public Color flashColor = Color.red;
    public float flashDuration = 0.1f;

    void Start()
    {
        foreach (Renderer rend in meshes)
        {
            foreach (Material mat in rend.materials)
            {
                materials.Add(mat);
            }
        }
    }
    private IEnumerator DoFlash()
    {
        FlashMaterialColor(flashColor);
        yield return new WaitForSeconds(flashDuration);
        FlashMaterialColor(Color.white);
    }
    private IEnumerator DoFlashing()
    {
        for(int i = 0; i < 1000; i++)
        {
            FlashMaterialColor(flashColor);
            yield return new WaitForSeconds(flashDuration);
            FlashMaterialColor(Color.white);
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

    void FlashMaterialColor(Color col)
    {
        foreach (Material mat in materials)
        {
            mat.color = col;
        }
    }

    /*private void Update()
    {
        if(Keyboard.current.digit1Key.wasPressedThisFrame)
        {
            //Debug.Log("was Pressed");
            Flash();
        }
    } */
}
