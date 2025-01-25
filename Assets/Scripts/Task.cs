using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Task : MonoBehaviour
{
    public string TaskName;
    public string Description;
    public bool IsCompleted;
    public Func<bool> CompletionCondition; // Condición para completar la tarea
    public Action OnTaskCompleted; // Acción al completar la tarea

    public Task(string taskName, string description, Func<bool> completionCondition, Action onTaskCompleted = null)
    {
        TaskName = taskName;
        Description = description;
        IsCompleted = false;
        CompletionCondition = completionCondition;
        OnTaskCompleted = onTaskCompleted;
    }

    public void CheckCompletion()
    {
        if (!IsCompleted && CompletionCondition())
        {
            IsCompleted = true;
            OnTaskCompleted?.Invoke();
        }
    }
}
