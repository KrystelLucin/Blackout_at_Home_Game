using UnityEngine;

public class CameraLoopMovement : MonoBehaviour
{
    public Transform startPoint; // Posición inicial de la cámara
    public Transform endPoint;   // Posición final de la cámara
    public float duration = 180f; // Duración del recorrido (en segundos)

    private float elapsedTime = 0f; // Tiempo transcurrido
    private bool goingForward = true; // Dirección del movimiento

    void Update()
    {
        // Actualizar el tiempo transcurrido
        elapsedTime += Time.deltaTime;

        // Calcular el porcentaje completado
        float t = elapsedTime / duration;

        // Mover la cámara entre startPoint y endPoint
        if (goingForward)
        {
            transform.position = Vector3.Lerp(startPoint.position, endPoint.position, t);
        }
        else
        {
            transform.position = Vector3.Lerp(endPoint.position, startPoint.position, t);
        }

        // Cuando el movimiento termine, invertir dirección
        if (t >= 1f)
        {
            elapsedTime = 0f;
            goingForward = !goingForward;
        }
    }
}
