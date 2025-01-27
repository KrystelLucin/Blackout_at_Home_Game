using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerPhoneTask : TaskBase
{
    public PhoneController phoneController; // Referencia al controlador del tel�fono

    protected override void Start()
    {
        base.Start();

        // Opcional: Agrega un evento cuando la tarea comience
        OnTaskStart += () =>
        {
            if (!phoneController.isRinging)
            {
                phoneController.StartRinging(); // Aseg�rate de que el tel�fono empiece a sonar
            }
        };

        // Configura el evento de tarea completada
        OnTaskComplete += () =>
        {
            Debug.Log("Tarea completada: Contestar el tel�fono");
        };
    }

    protected override void CheckCompletionCondition()
    {
        // Verifica si el tel�fono ha dejado de sonar por la interacci�n
        if (!phoneController.isRinging)
        {
            CompleteTask();
        }
    }
}
