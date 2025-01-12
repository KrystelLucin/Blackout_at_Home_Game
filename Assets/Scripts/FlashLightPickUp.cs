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

                // Asignar las coordenadas específicas
                transform.localPosition = new Vector3(0.2160001f, 0.08500004f, -0.4340004f); // Coordenadas locales
                transform.localRotation = Quaternion.Euler(-90f, 0f, 172.338f); // Rotación específica

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
