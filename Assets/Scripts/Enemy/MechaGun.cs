using UnityEngine;

public class MechaGun : MonoBehaviour
{
    [SerializeField] private float rotateX;
    [SerializeField] private float rotateY;
    [SerializeField] private float rotateZ;
    private float rotatedX;
    private float rotatedY;
    private float rotatedZ;
    private Transform player;
    private void Awake()
    {
        rotatedX = rotateX;
        rotatedY = rotateY;
        rotatedZ = rotateZ;
        player = GameObject.Find("Player").transform;
    }
    private void FixedUpdate()
    {
        Vector3 cameraPosition = player.transform.position;
        cameraPosition.y = transform.position.y;
        transform.LookAt(cameraPosition);
        transform.Rotate(rotatedX + 3f, rotatedY, rotatedZ);
    }

}
