using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerPhoneTask : TaskBase
{
    public PhoneController phoneController; // Referencia al controlador del teléfono

    protected override void CheckCompletionCondition()
    {
        // Verifica si el teléfono ha dejado de sonar por la interacción
        if (phoneController.ringingStoped)
        {
            CompleteTask();
        }
    }
}
