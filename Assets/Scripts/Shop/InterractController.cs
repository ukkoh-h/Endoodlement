 using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

/*public class InterractController : MonoBehaviour
{
    [SerializeField]
    Camera playerCamera;

    [SerializeField]
    TextMeshProUGUI interactionText;

    [SerializeField]
    float interactionDistance = 5f;

    IInterractable currentTargetedInteractable;


    public void Update()
    {
        UpdateCurrentInteractable();

        UpdateInteractionText();

        CheckForInteractionInput();
    }

    void UpdateCurrentInteractable()
    {
        var ray = playerCamera.ViewportPointToRay(new Vector2(0.5f, 0.5f));

        Physics.Raycast(ray, out var hit, interactionDistance);

        currentTargetedInteractable = hit.collider?.GetComponent<IInterractable>();
    }

    void UpdateInteractionText()
    {

        if(currentTargetedInteractable == null)
        {
            interactionText.text = string.Empty;
            return;

        }
        interactionText.text = currentTargetedInteractable.InterractMessage;
    }

    void CheckForInteractionInput()
    {
        if(Keyboard.current.eKey.wasPressedThisFrame && currentTargetedInteractable != null)
        {
            currentTargetedInteractable.Interact();
        }
    }

} */
