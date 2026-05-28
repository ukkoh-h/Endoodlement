using System.Collections;
using UnityEditor.Rendering;
using UnityEngine;

public class CopterBillboarding : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private float rotateX;
    [SerializeField] private float rotateY;
    [SerializeField] private float rotateZ;
    [SerializeField] private Transform backCopter;
    private float rotatedX;
    private float rotatedY;
    private float rotatedZ;
    private float floatTime;
    private bool walking;
    private bool escaping;
    private bool escapeStarted;
    private bool approaching;
    private bool approachStarted;
    private bool floating;
    private bool dying;
    /*private bool step;
    private bool steping;*/
    private void Awake()
    {
        rotatedX = rotateX;
        rotatedY = rotateY;
        rotatedZ = rotateZ;
    }
    private void LateUpdate()
    {
        Vector3 cameraPosition = mainCamera.transform.position;
        //cameraPosition.y = transform.position.y;
        transform.LookAt(cameraPosition);
        transform.Rotate(rotatedX, rotatedY + 180f, rotatedZ);
    }

    private void Update()
    {
        if (floatTime < 1 && floating)
        {
            floatTime += Time.deltaTime*1.5f;
        } 
        else if (floatTime > 0 && !floating)
        {
            floatTime -= Time.deltaTime*1.5f;
        }
        if (floatTime>=1 && floating || floatTime <= 0 && !floating) floating = !floating;
        backCopter.position = new Vector3(transform.position.x, floatTime*0.7f + 4.95f, transform.position.z);
        //Debug.Log(escaping);
        /*if (walking && !steping)
        {
            steping = true;
            StartCoroutine(WalkingSequence());
        }*/
        if (escaping && !escapeStarted)
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
        }
    }

    public void Walk()
    {
        walking = !walking;
    }
    public void Escape()
    {
        escaping = !escaping;
    }
    public void Approach()
    {
        approaching = !approaching;
    }
    public void Hit()
    {
        StartCoroutine(HitSequence());
    }
    public void Attack()
    {
         StartCoroutine(AttackSequence());
    }

    private IEnumerator HitSequence()
    {
        rotatedX += 10f;
        yield return new WaitForSeconds(0.1f);
        rotatedX -= 10f;
    }

    private IEnumerator AttackSequence()
    {
        rotatedY += 10f;
        yield return new WaitForSeconds(0.1f);
        rotatedY = rotateY;
    }
    /*private IEnumerator WalkingSequence()
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
    }*/
}
