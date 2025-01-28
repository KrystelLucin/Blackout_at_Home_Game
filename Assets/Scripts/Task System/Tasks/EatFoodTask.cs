using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatFoodTask : TaskBase
{
    public FoodController foodController; // Referencia al controlador de la comida

    protected override void CheckCompletionCondition()
    {
        // La tarea se completa cuando la comida ha sido comida
        if (foodController != null && foodController.isEaten)
        {
            CompleteTask();
        }
    }
}