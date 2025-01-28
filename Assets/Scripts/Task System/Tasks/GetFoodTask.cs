using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetFoodTask : TaskBase
{
    public FoodPickUp foodPickUp; // Referencia al script FoodPickUp

    protected override void CheckCompletionCondition()
    {
        // Comprueba si la comida ha sido recogida
        if (foodPickUp != null && foodPickUp.isHeld)
        {
            CompleteTask();
        }
    }
}
