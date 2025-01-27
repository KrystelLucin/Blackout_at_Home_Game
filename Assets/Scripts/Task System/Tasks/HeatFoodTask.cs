using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeatFoodTask : TaskBase
{
    public FoodController food; // Referencia al controlador del microondas

    protected override void CheckCompletionCondition()
    {
        // La tarea se completa cuando la comida se ha calentado y se ha retirado del microondas
        if (food != null && food.isHot)
        {
            CompleteTask();
        }
    }
}
