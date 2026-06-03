using StarterAssets;
using UnityEngine;
using UnityEngine.Windows;

public class Shop : MonoBehaviour, IInteractable
{

    [SerializeField]
    public GameObject ShopWindow;

    public void Start()
    {
        ShopWindow.SetActive(false);
    }


    private bool _isOpen = false;
    public bool CanInteract()
    {
        return true;
    }

    public bool Interact(Interactor interactor)
    {
       if (_isOpen)
        {
            ShopWindow.SetActive(true);
        }
       else
        {
            ShopWindow.SetActive(false);
        }
        _isOpen = !_isOpen;
        return true;
    }


    /*public GameObject ShopWindow;
    private StarterAssetsInputs _input;
    private void Interact()
    {
        if (_input.interract)
        {

        }

    } */
}
