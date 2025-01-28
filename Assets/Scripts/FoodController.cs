using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodController : MonoBehaviour
{
    [Header("Food States")]
    public bool isHot = false; // Indica si la comida est� caliente
    public bool isEaten = false; // Indica si la comida ya ha sido comida
    public bool isPickedUp = false; // Indica si la comida est� en la mano del jugador
    public bool isOnTable = false; // Indica si la comida est� en la mesa

    public Transform tablePosition; // Posici�n de la mesa

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void HeatFood()
    {
        isHot = true;
        Debug.Log("La comida est� caliente.");
    }

    public void EatFood()
    {
        isEaten = true;
        Debug.Log("La comida ha sido comida.");
    }

    public void PlaceFoodOnTable()
    {
        transform.position = tablePosition.position;
        isPickedUp = false;
        isOnTable = true;
        Debug.Log("La comida ha sido colocada en la mesa.");

    }
}
