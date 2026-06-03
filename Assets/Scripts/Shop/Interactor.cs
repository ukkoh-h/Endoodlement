using StarterAssets;
using UnityEngine;

public class Interactor : MonoBehaviour
{

    private StarterAssetsInputs _input;
    public static Interactor instance;
    [SerializeField]
    private float _CastDistance = 5f;
    [SerializeField]
    private Vector3 _raycastOffset = new Vector3(0, 1f, 0);


    private void Update()
    {
        if (_input.interract)
        {
            if(DoInteractionTest(out IInteractable interactable))
            {
              if(interactable.CanInteract())
                {
                    interactable.Interact(this);
                }

            }
        }
    }


    private bool DoInteractionTest(out IInteractable interactable)
    {
        interactable = null;

        Ray ray = new Ray(transform.position + _raycastOffset, transform.forward);


        if (Physics.Raycast(ray, out RaycastHit hitInfo, _CastDistance))
        {
            interactable = hitInfo.collider.GetComponent<IInteractable>();

            if(interactable != null)
            {
                return true;
            }
            return false;
        }

        return false;
    }

}
