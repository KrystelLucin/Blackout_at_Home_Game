using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class TaskBase : MonoBehaviour
{
    [Header("Task Properties")]
    public string taskDescription; // Texto que aparece en pantalla
    public List<TaskBase> prerequisites; // Lista de tareas que deben completarse antes
    public bool isCompleted = false; // Si la tarea ya est� completada

    [Header("Optional Events")]
    public System.Action OnTaskStart; // Evento que se ejecuta al iniciar la tarea
    public System.Action OnTaskComplete; // Evento que se ejecuta al completarse la tarea

    protected virtual void Start()
    {
        // Vincular los eventos de inicio y completitud si es necesario
        OnTaskStart += TaskStarted;
        OnTaskComplete += TaskCompleted;

        // Ocultar o desactivar el componente si no se cumplen los requisitos
        if (!ArePrerequisitesMet())
        {
            gameObject.SetActive(false);
        }
    }

    void Update()
    {
        // Solo verifica la condici�n si la tarea no est� completada y los requisitos est�n cumplidos
        if (!isCompleted && ArePrerequisitesMet())
        {
            CheckCompletionCondition();
        }
    }

    // M�todo abstracto para implementar la l�gica espec�fica de cada tarea
    protected abstract void CheckCompletionCondition();

    // M�todo para completar la tarea
    protected void CompleteTask()
    {
        isCompleted = true;
        OnTaskComplete?.Invoke(); // Llamar el evento de completitud
        Debug.Log($"Tarea completada: {taskDescription}");
    }

    // Verifica si todas las tareas previas est�n completadas
    public bool ArePrerequisitesMet()
    {
        foreach (var prerequisite in prerequisites)
        {
            if (!prerequisite.isCompleted)
            {
                return false;
            }
        }
        return true;
    }

    // M�todo para iniciar la tarea manualmente
    public void ActivateTask()
    {
        if (ArePrerequisitesMet() && !isCompleted)
        {
            gameObject.SetActive(true); // Asegura que el GameObject est� activo
            OnTaskStart?.Invoke();      // Ejecuta el evento de inicio
        }
    }

    // Eventos b�sicos
    private void TaskStarted()
    {
        Debug.Log($"Tarea iniciada: {taskDescription}");
    }

    private void TaskCompleted()
    {
        Debug.Log($"Tarea completada: {taskDescription}");
        gameObject.SetActive(false); // Desactiva el GameObject al completar
    }
}
