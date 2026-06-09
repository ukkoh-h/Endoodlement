using UnityEngine;

public class CampActivationTrigger : MonoBehaviour
{
    [SerializeField] private bool isActive;
    [SerializeField] private bool isWall;
    [SerializeField] private EnemyManager nextCombat;
    [SerializeField] private Dissolver dissolver;
    private void OnTriggerEnter(Collider other)
    {
        if(isActive && other.CompareTag("Player"))
        {
            isActive = false;
            if(isWall) dissolver.StartDissolver();
            else nextCombat.ActivateCamp();
        }
    }
    public void ActivateTrigger()
    {
        isActive = true;
    }
}
