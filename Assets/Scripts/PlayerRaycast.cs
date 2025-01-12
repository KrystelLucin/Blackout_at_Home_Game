using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IInteractable
{
    void Interact();
    void ShowText(bool flag);
}

public class PlayerRaycast : MonoBehaviour
{
    public GameObject crosshair;
    public float interactionDistance;
    public LayerMask layers; // layers to hit

    private IInteractable lastInteractable; // To track the last interactable object

    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, interactionDistance, layers))
        {
            if (hit.collider.gameObject.TryGetComponent(out IInteractable interactObj))
            {
                if (lastInteractable != null && lastInteractable != interactObj)
                {
                    lastInteractable.ShowText(false);
                }
                interactObj.ShowText(true);
                crosshair.SetActive(true);
                lastInteractable = interactObj;

                if (Input.GetKeyDown(KeyCode.E))
                {
                    interactObj.Interact();
                }
            }
            else
            {
                crosshair.SetActive(false);
                if (lastInteractable != null)
                {
                    lastInteractable.ShowText(false);
                    lastInteractable = null;
                }
            }
        }
        else
        {
            crosshair.SetActive(false);
            if (lastInteractable != null)
            {
                lastInteractable.ShowText(false);
                lastInteractable = null;
            }
        }
    }
}
