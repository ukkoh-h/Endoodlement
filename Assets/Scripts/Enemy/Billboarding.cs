using System.Collections;
using UnityEngine;

public class Billboarding : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private float rotateX;
    [SerializeField] private float rotateY;
    [SerializeField] private float rotateZ;
    private bool walking;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void LateUpdate()
    {
        Vector3 cameraPosition = mainCamera.transform.position;
        //cameraPosition.y = transform.position.y;
        transform.LookAt(cameraPosition);
        transform.Rotate(rotateX, rotateY + 180f, rotateZ);
    }

    public void Walk()
    {
        walking = !walking;
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
        rotateX += 10f;
        yield return new WaitForSeconds(0.1f);
        rotateX = 0f;
    }

        private IEnumerator AttackSequence()
    {
        rotateY += 10f;
        yield return new WaitForSeconds(0.1f);
        rotateY = 0f;
    }
}
