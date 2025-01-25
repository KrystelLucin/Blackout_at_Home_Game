using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    public List<Task> Tasks = new List<Task>(); // Lista de tareas
    private int currentTaskIndex = 0; // Índice de la tarea actual

    void Start()
    {
        //InitializeTasks(); // Inicializa las tareas al comienzo del juego
        //DisplayCurrentTask(); // Muestra la primera tarea
    }

    void Update()
    {
        // Verifica si la tarea actual se cumple
        if (currentTaskIndex < Tasks.Count)
        {
            Tasks[currentTaskIndex].CheckCompletion();
            if (Tasks[currentTaskIndex].IsCompleted)
            {
                //CompleteCurrentTask();
            }
        }
    }
}
