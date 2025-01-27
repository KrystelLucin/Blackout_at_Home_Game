using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Linterna : MonoBehaviour, IInteractable
{
    public CharacterController controller;
    public Transform handPosition;
    public TMP_Text interactionText;
    private bool isHeld = false;

    public void Interact()
    {
        if (!isHeld)
        {
            if (handPosition != null)
            {
                transform.SetParent(handPosition);

                transform.SetParent(handPosition);
                transform.localPosition = new Vector3(0.3f, -0.2f, 0.6f); // Ajustar según la visibilidad
                transform.localRotation = Quaternion.Euler(15f, 45f, 0f); // Orientación visible
                transform.localScale = Vector3.one * 0.8f; // Escalar si es necesario

                // Desactiva la física
                var rigidbody = GetComponent<Rigidbody>();
                if (rigidbody != null)
                {
                    rigidbody.isKinematic = true;
                }
                isHeld = true;
            }
            else
            {
                Debug.LogWarning("Hand position not set. Please assign it in the Inspector.");
            }
        }
    }

    public void ShowText(bool flag)
    {
        interactionText.gameObject.SetActive(flag);
    }
}
