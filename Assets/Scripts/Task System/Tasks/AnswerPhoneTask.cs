using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerPhoneTask : TaskBase
{
    public PhoneController phoneController; // Referencia al controlador del tel�fono

    protected override void CheckCompletionCondition()
    {
        // Verifica si el tel�fono ha dejado de sonar por la interacci�n
        if (phoneController.ringingStoped)
        {
            CompleteTask();
        }
    }
}
