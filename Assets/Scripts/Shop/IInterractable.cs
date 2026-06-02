using UnityEngine;

public class IInterractable : MonoBehaviour
{
  public interface IInteractable
    {
        public string InterractMessage { get; }
        public void Interact();
    }
    
}
