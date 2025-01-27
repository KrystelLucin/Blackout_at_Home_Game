using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerPhoneTask : TaskBase
{
    public PhoneController phoneController; // Referencia al controlador del teléfono

    protected override void Start()
    {
        base.Start();

        // Opcional: Agrega un evento cuando la tarea comience
        OnTaskStart += () =>
        {
            if (!phoneController.isRinging)
            {
                phoneController.StartRinging(); // Asegúrate de que el teléfono empiece a sonar
            }
        };

        // Configura el evento de tarea completada
        OnTaskComplete += () =>
        {
            Debug.Log("Tarea completada: Contestar el teléfono");
        };
    }

    protected override void CheckCompletionCondition()
    {
        // Verifica si el teléfono ha dejado de sonar por la interacción
        if (!phoneController.isRinging)
        {
            CompleteTask();
        }
    }
}
