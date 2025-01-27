using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodController : MonoBehaviour
{
    [Header("Food States")]
    public bool isHot = false; // Indica si la comida está caliente
    public bool isEaten = false; // Indica si la comida ya ha sido comida
    public bool isPickedUp = false; // Indica si la comida está en la mano del jugador

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
        Debug.Log("La comida está caliente.");
    }
}
