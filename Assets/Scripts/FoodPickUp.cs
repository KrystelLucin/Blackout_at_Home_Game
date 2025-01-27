using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FoodPickUp : MonoBehaviour, IInteractable
{
    public Transform handPosition; // Posición de la mano del jugador donde irá la comida
    public TMP_Text interactionText; // Texto que aparece para la interacción
    public bool isHeld = false; // Indica si la comida ya está tomada

    public string overlayLayer = "Overlay"; // Nombre de la capa "Overlay"

    public void Interact()
    {
        if (!isHeld)
        {
            if (handPosition != null)
            {
                // Configura el objeto como hijo de la posición de la mano
                transform.SetParent(handPosition);

                // Ajusta la posición y rotación para que se vea bien en primera persona
                transform.localPosition = new Vector3(1.0f, -0.5f, 0.5f);
                transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
                transform.localScale = Vector3.one;

                ChangeLayerRecursively(gameObject, LayerMask.NameToLayer(overlayLayer));


                isHeld = true;
                Debug.Log("Comida recogida.");
            }
            else
            {
                Debug.LogWarning("Hand position not set. Please assign it in the Inspector.");
            }
        }
    }

    public void ShowText(bool flag)
    {
        // Muestra u oculta el texto de interacción
        interactionText.gameObject.SetActive(flag);
    }

    private void ChangeLayerRecursively(GameObject obj, int newLayer)
    {
        obj.layer = newLayer; // Cambia la capa del objeto actual

        foreach (Transform child in obj.transform)
        {
            ChangeLayerRecursively(child.gameObject, newLayer); // Cambia la capa de los hijos
        }
    }
}
