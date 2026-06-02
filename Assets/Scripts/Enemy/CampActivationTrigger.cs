using UnityEngine;

public class CampActivationTrigger : MonoBehaviour
{
    [SerializeField] private bool isActive;
    [SerializeField] private EnemyManager nextCombat;
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(isActive && other.CompareTag("Player"))
        {
            nextCombat.ActivateCamp();
        }
    }
    public void ActivateTrigger()
    {
        isActive = true;
    }
}
