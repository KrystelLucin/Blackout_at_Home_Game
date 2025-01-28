using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    [Header("Task Management")]
    public List<TaskBase> allTasks;
    private int currentTaskIndex = 0; 

    [Header("UI Components")]
    public TMP_Text taskText; 

    void Start()
    {
        // Inicializa todas las tareas y vincula sus eventos
        foreach (var task in allTasks)
        {
            task.OnTaskComplete += HandleTaskCompletion;
            task.gameObject.SetActive(false); // Desactiva todas las tareas al inicio
        }

        // Activa la primera tarea (�ndice 0)
        ActivateCurrentTask();
    }

    void HandleTaskCompletion()
    {
        // Avanza al siguiente �ndice cuando la tarea actual se complete
        currentTaskIndex++;

        // Verifica si hay m�s tareas en la lista
        if (currentTaskIndex < allTasks.Count)
        {
            ActivateCurrentTask(); // Activa la siguiente tarea
        }
        else
        {
            // Si no quedan tareas, muestra un mensaje final
            taskText.text = "Todas las tareas completadas.";
            Debug.Log("�Todas las tareas completadas!");
        }
    }

    void ActivateCurrentTask()
    {
        // Verifica si la tarea actual est� dentro de los l�mites de la lista
        if (currentTaskIndex < allTasks.Count && currentTaskIndex >= 0)
        {
            var currentTask = allTasks[currentTaskIndex];
            currentTask.ActivateTask(); // Activa la tarea actual
            taskText.text = currentTask.taskDescription; // Actualiza la interfaz
            Debug.Log($"Nueva tarea activa: {currentTask.taskDescription}");
        }
        else
        {
            Debug.LogWarning("�ndice de tarea fuera de los l�mites de la lista.");
        }
    }
}
