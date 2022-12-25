using UnityEngine;
using Interfaces;


public class PlayerController : MonoBehaviour
{
    public int x = 0;
    public int donmeMiktari;

    

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "collectable" && other.TryGetComponent(out IInteract interactable))
        {
            interactable.Interact();
            
        }
        /*
                if (other.TryGetComponent(out IInteract interactable))
                {
                    interactable.Interact();
                }
        */
    }
}
   

