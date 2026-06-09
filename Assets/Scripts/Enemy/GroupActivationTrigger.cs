using UnityEngine;

public class GroupActivationTrigger : MonoBehaviour
{
    [SerializeField] private Enemy goblin1;
    [SerializeField] private Enemy goblin2;
    [SerializeField] private Enemy goblin3;
    [SerializeField] private Enemy goblin4;
    [SerializeField] private Enemy goblin5;
    [SerializeField] private EnemyCopter copter1;
    [SerializeField] private EnemyCopter copter2;
    [SerializeField] private EnemyCopter copter3;
    [SerializeField] private EnemyMecha mecha;

    private bool isActive = true;

    private void OnTriggerEnter(Collider other)
    {
        if(isActive && other.CompareTag("Player"))
        {
            isActive = false;
            if(goblin1 != null) goblin1.Activate();
            if(goblin2 != null) goblin2.Activate();
            if(goblin3 != null) goblin3.Activate();
            if(goblin4 != null) goblin4.Activate();
            if(goblin5 != null) goblin5.Activate();
            if(copter1 != null) copter1.Activate();
            if(copter2 != null) copter2.Activate();
            if(copter3 != null) copter3.Activate();
            if(mecha != null) mecha.Activate();
        }
    }
}
