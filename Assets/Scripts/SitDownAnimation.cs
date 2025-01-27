using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SitDownAnimation : MonoBehaviour
{
    public Vector3 initialPosition; // Posición inicial antes de sentarse
    public Vector3 finalPosition; // Posición final al sentarse
    public Quaternion initialRotation; // Rotación inicial antes de sentarse
    public Quaternion finalRotation; // Rotación final al sentarse

    public float animationDuration = 2f; // Duración de la animación en segundos
    private float elapsedTime = 0f;
    private bool isAnimating = false;

    public System.Action onAnimationEnd; // Evento que se llama al terminar la animación

    void Start()
    {
        // Establecer la posición y rotación inicial
        transform.position = initialPosition;
        transform.rotation = initialRotation;
    }

    void Update()
    {
        if (isAnimating)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / animationDuration;

            // Interpolación lineal para posición y rotación
            transform.position = Vector3.Lerp(initialPosition, finalPosition, t);
            transform.rotation = Quaternion.Lerp(initialRotation, finalRotation, t);

            if (elapsedTime >= animationDuration)
            {
                isAnimating = false;
                onAnimationEnd?.Invoke();
            }
        }
    }

    public void StartSitDownAnimation()
    {
        elapsedTime = 0f;
        isAnimating = true;
    }
}