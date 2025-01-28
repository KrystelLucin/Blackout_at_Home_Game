using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LeaveFood : MonoBehaviour, IInteractable 
{
    [Header("Leave Settings")]
    public Transform handTransform; // Transform de la mano que sostiene la comida
    public Transform dropPoint; // Punto donde se colocará la comida
    public MicrowaveController microwaveController; // Referencia al microondas (opcional)
    public Transform doorTransform; // Referencia a la puerta del microondas
    public Vector3 openDoorRotation = new Vector3(0, 90, 0); // Rotación de la puerta cuando está abierta


    public string groundLayer = "Ground"; // Nombre de la capa "Overlay"
    public TMP_Text interactionText;

    public void Interact()
    {
        // Verifica si la puerta está abierta
        if (!IsDoorOpen())
        {
            Debug.Log("La puerta del microondas debe estar abierta para dejar la comida.");
            return;
        }

        // Verifica si hay comida en la mano
        if (handTransform.childCount == 0)
        {
            Debug.Log("No tienes comida en la mano para dejar en el microondas.");
            return;
        }

        // Toma el primer objeto hijo (comida)
        Transform food = handTransform.GetChild(0);

        // Cambia el parent de la comida al dropPoint
        PlaceFood(food);

        // Notifica al microondas que se ha colocado la comida (si aplica)
        if (microwaveController != null)
        {
            microwaveController.NotifyFoodPlaced(food.gameObject);
        }

        Debug.Log("Comida colocada en el microondas.");
    }

    private bool IsDoorOpen()
    {
        return doorTransform.localRotation == Quaternion.Euler(openDoorRotation);
    }

    private void PlaceFood(Transform food)
    {
        // Cambia el parent de la comida al dropPoint
        food.SetParent(dropPoint);

        // Ajusta la posición, rotación y escala
        food.localPosition = Vector3.zero;
        food.localRotation = Quaternion.identity;
        food.localScale = new Vector3(0.4f, 0.4f, 0.4f);
        ChangeLayerRecursively(gameObject, LayerMask.NameToLayer(groundLayer));
        FoodPickUp foodPickUp = food.GetComponent<FoodPickUp>();
        foodPickUp.isHeld = false;
    }

    private void ChangeLayerRecursively(GameObject obj, int newLayer)
    {
        obj.layer = newLayer; // Cambia la capa del objeto actual

        foreach (Transform child in obj.transform)
        {
            ChangeLayerRecursively(child.gameObject, newLayer); // Cambia la capa de los hijos
        }
    }

    public void ShowText(bool flag)
    {
        interactionText.gameObject.SetActive(flag);
    }
}
