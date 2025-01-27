using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    [Header("Task Management")]
    public List<TaskBase> allTasks; // Lista de todas las tareas del juego
    private int currentTaskIndex = 0; // Índice de la tarea actual

    [Header("UI Components")]
    public TMP_Text taskText; // Texto para mostrar la descripción de la tarea activa

    void Start()
    {
        // Inicializa las tareas
        foreach (var task in allTasks)
        {
            task.OnTaskComplete += HandleTaskCompletion;
        }

        DisplayCurrentTask(); // Muestra la primera tarea activa
    }

    void HandleTaskCompletion()
    {
        // Mueve al siguiente índice y actualiza la interfaz
        currentTaskIndex++;
        DisplayCurrentTask();
    }

    void DisplayCurrentTask()
    {
        // Comprueba si quedan tareas activas
        if (currentTaskIndex < allTasks.Count)
        {
            var currentTask = allTasks[currentTaskIndex];

            if (currentTask.ArePrerequisitesMet() && !currentTask.isCompleted)
            {
                taskText.text = currentTask.taskDescription;
                currentTask.ActivateTask();
            }
        }
        else
        {
            // Si no hay tareas activas
            taskText.text = "Todas las tareas completadas";
        }
    }
}
