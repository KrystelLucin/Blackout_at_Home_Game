using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WakeUpAnimation : MonoBehaviour
{
    public Vector3 initialPosition = new Vector3(14.59f, 3.422f, -15.623f);
    public Vector3 finalPosition = new Vector3(15.879f, 4.04f, -15.01f);
    public Quaternion initialRotation = Quaternion.Euler(-90, 0, 0);
    public Quaternion finalRotation = Quaternion.Euler(0, 90, 0);

    public float animationDuration = 3f; // Duración de la animación en segundos
    private float phase1Duration;
    private float phase2Duration;
    private float phase3Duration;

    private float elapsedTime = 0f;
    private int currentPhase = 0; // Controla la fase actual de la animación
    private bool isAnimating = false;

    public System.Action onAnimationEnd; // Evento que se llama al terminar la animación

    void Start()
    {
        phase1Duration = animationDuration * 0.5f; // 50% del tiempo para fase 1
        phase2Duration = animationDuration * 0.25f; // 25% del tiempo para fase 2
        phase3Duration = animationDuration * 0.25f; // 25% del tiempo para fase 3

        // Establecer la posición inicial de la cámara
        transform.position = initialPosition;
        transform.rotation = initialRotation;
    }

    void Update()
    {
        if (isAnimating)
        {
            elapsedTime += Time.deltaTime;

            switch (currentPhase)
            {
                case 0:
                    AnimatePhase1();
                    break;
                case 1:
                    AnimatePhase2();
                    break;
                case 2:
                    AnimatePhase3();
                    break;
            }
        }
    }

    public void PlayAnimation()
    {
        elapsedTime = 0f;
        currentPhase = 0;
        isAnimating = true;
    }

    private void AnimatePhase1()
    {
        float progress = Mathf.Clamp01(elapsedTime / phase1Duration);

        // Movimiento en Z y Y hasta la mitad del Y final
        float targetY = Mathf.Lerp(initialPosition.y, (initialPosition.y + finalPosition.y) / 2, progress);
        float targetZ = Mathf.Lerp(initialPosition.z, finalPosition.z, progress);
        transform.position = new Vector3(initialPosition.x, targetY, targetZ);

        // Rotación en X
        float targetRotationX = Mathf.LerpAngle(initialRotation.eulerAngles.x, finalRotation.eulerAngles.x, progress);
        transform.rotation = Quaternion.Euler(targetRotationX, initialRotation.eulerAngles.y, initialRotation.eulerAngles.z);

        if (progress >= 1f)
        {
            elapsedTime = 0f;
            currentPhase++;
        }
    }

    private void AnimatePhase2()
    {
        float progress = Mathf.Clamp01(elapsedTime / phase2Duration);

        // Rotación en Y
        float targetRotationY = Mathf.LerpAngle(initialRotation.eulerAngles.y, finalRotation.eulerAngles.y, progress);
        transform.rotation = Quaternion.Euler(finalRotation.eulerAngles.x, targetRotationY, initialRotation.eulerAngles.z);

        if (progress >= 1f)
        {
            elapsedTime = 0f;
            currentPhase++;
        }
    }

    private void AnimatePhase3()
    {
        float progress = Mathf.Clamp01(elapsedTime / phase3Duration);

        // Movimiento en X y Y
        float targetX = Mathf.Lerp(initialPosition.x, finalPosition.x, progress);
        float targetY = Mathf.Lerp((initialPosition.y + finalPosition.y) / 2, finalPosition.y, progress);
        transform.position = new Vector3(targetX, targetY, finalPosition.z);

        // Mantener la rotación final
        transform.rotation = Quaternion.Euler(finalRotation.eulerAngles.x, finalRotation.eulerAngles.y, initialRotation.eulerAngles.z);

        if (progress >= 1f)
        {
            isAnimating = false;
            onAnimationEnd?.Invoke(); // Llamar al evento al finalizar
        }
    }
}
