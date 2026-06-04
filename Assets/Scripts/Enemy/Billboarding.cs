using System.Collections;
using UnityEngine;

public class Billboarding : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private float rotateX;
    [SerializeField] private float rotateY;
    [SerializeField] private float rotateZ;
    /*public Renderer rend;
    public Color flashColor = Color.red;
    private Color originalColor;
    public float flashDuration = 0.1f;*/
    private float rotatedX;
    private float rotatedY;
    private float rotatedZ;
    private bool walking;
    /*private bool escaping;
    private bool escapeStarted;
    private bool approaching;
    private bool approachStarted;*/
    private bool step;
    private bool steping;

    private void Awake()
    {
        rotatedX = rotateX;
        rotatedY = rotateY;
        rotatedZ = rotateZ;
    }
    /*void Start()
    {
        originalColor = rend.material.color;
    }*/
    private void LateUpdate()
    {
        Vector3 cameraPosition = mainCamera.transform.position;
        cameraPosition.y = transform.position.y;
        transform.LookAt(cameraPosition);
        transform.Rotate(rotatedX, rotatedY + 180f, rotatedZ);
    }

    private void Update()
    {
        //Debug.Log(escaping);
        if (walking && !steping)
        {
            steping = true;
            StartCoroutine(WalkingSequence());
        }
        /*if (escaping && !escapeStarted)
        {
            escapeStarted = true;
            rotatedX -= 20f;
        }
        else if (!escaping && escapeStarted)
        {
            escapeStarted = false;
            rotatedX += 20f;
        }
        if (approaching && !approachStarted)
        {
            approachStarted = true;
            rotatedX += 20f;
        }
        else if (!approaching && approachStarted)
        {
            approachStarted = false;
            rotatedX -= 20f;
        }*/
    }

    public void Walk()
    {
        walking = !walking;
    }
    /*public void Escape()
    {
        escaping = !escaping;
    }*/
    /*public void Approach()
    {
        approaching = !approaching;
    }*/
    public void Hit()
    {
        StartCoroutine(HitSequence());
    }
    public void Attack()
    {
         StartCoroutine(AttackSequence());
    }
    public void Dying()
    {
        rotatedX -= 20f;
    }

    private IEnumerator HitSequence()
    {
        rotatedX += 10f;
        yield return new WaitForSeconds(0.1f);
        rotatedX -= 10f;
    }

    private IEnumerator AttackSequence()
    {
        rotatedY += 20f;
        yield return new WaitForSeconds(0.1f);
        rotatedY = rotateY;
    }
    private IEnumerator WalkingSequence()
    {
        if(step)
        {
            rotatedZ += 5f;
            step = false;
        } 
        else
        {
            rotatedZ -= 5f;
            step = true;
        }
        yield return new WaitForSeconds(0.1f);
        rotatedZ = rotateZ;
        steping = false;
    }
    /*private IEnumerator DoFlash()
    {
        rend.material.color = flashColor;
        yield return new WaitForSeconds(flashDuration);
        rend.material.color = originalColor;
    }
    public void Flash()
    {
        StopAllCoroutines();
        StartCoroutine(DoFlash());
    }*/
    
}
