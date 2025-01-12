using UnityEngine;

public class CameraLoopMovement : MonoBehaviour
{
    public Transform startPoint; // Posici�n inicial de la c�mara
    public Transform endPoint;   // Posici�n final de la c�mara
    public float duration = 180f; // Duraci�n del recorrido (en segundos)

    private float elapsedTime = 0f; // Tiempo transcurrido
    private bool goingForward = true; // Direcci�n del movimiento

    void Update()
    {
        // Actualizar el tiempo transcurrido
        elapsedTime += Time.deltaTime;

        // Calcular el porcentaje completado
        float t = elapsedTime / duration;

        // Mover la c�mara entre startPoint y endPoint
        if (goingForward)
        {
            transform.position = Vector3.Lerp(startPoint.position, endPoint.position, t);
        }
        else
        {
            transform.position = Vector3.Lerp(endPoint.position, startPoint.position, t);
        }

        // Cuando el movimiento termine, invertir direcci�n
        if (t >= 1f)
        {
            elapsedTime = 0f;
            goingForward = !goingForward;
        }
    }
}
