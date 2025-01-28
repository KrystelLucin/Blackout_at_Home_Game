using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MicrowaveController : MonoBehaviour, IInteractable
{
    [Header("Microondas Componentes")]
    public BoxCollider microwaveCollider;
    public Transform microwavePlate; // Plato giratorio dentro del microondas
    public Light microwaveLight; // Luz dentro del microondas
    public Transform microwaveDoor; // Puerta del microondas
    private GameObject foodInside; // Comida dentro del microondas

    [Header("Condiciones")]
    public Vector3 closedDoorRotation = Vector3.zero; // Rotación de la puerta cerrada

    [Header("Estados")]
    public bool isFoodInside = false; // Si hay comida dentro
    public bool isMicrowaveRunning = false; // Si el microondas está encendido
    private GameObject currentFood; // Referencia al plato de comida

    [Header("Configuración del Microondas")]
    public float cookingTime = 5f; // Tiempo que el microondas cocinará la comida
    public AudioSource microwaveEndSound; // Sonido del microondas
    public AudioSource microwaveSound; // Sonido del microondas

    public TMP_Text interactionText;

    void Start()
    {
        microwaveSound.enabled = false;
        microwaveEndSound.enabled = false;
    }

    void Update()
    {
        // Verifica si ya no hay comida dentro para reactivar el collider
        if (foodInside == null && !microwaveCollider.enabled)
        {
            ReactivateCollider();
        }
    }

    public void NotifyFoodPlaced(GameObject food)
    {
        foodInside = food;
    }

    public void Interact()
    {
        // Verifica que el microondas pueda iniciar
        if (foodInside == null)
        {
            Debug.Log("No hay comida dentro del microondas.");
            return;
        }

        if (!IsDoorClosed())
        {
            Debug.Log("La puerta debe estar cerrada para iniciar el microondas.");
            return;
        }

        if (!isMicrowaveRunning)
        {
            StartMicrowave();
        }
    }

    private bool IsDoorClosed()
    {
        return microwaveDoor.localRotation == Quaternion.Euler(closedDoorRotation);
    }

    private void StartMicrowave()
    {
        isMicrowaveRunning = true;
        Debug.Log("El microondas ha comenzado.");

        // Activa la luz y empieza a girar el plato
        microwaveLight.enabled = true;
        microwaveSound.enabled = true;
        microwaveSound.Play();

        StartCoroutine(RotatePlate());

        // Detiene el microondas después del tiempo de cocción
        Invoke(nameof(StopMicrowave), cookingTime);
    }

    private IEnumerator RotatePlate()
    {
        float elapsedTime = 0f;
        float rotationSpeed = 60f; // Velocidad de giro en grados por segundo

        while (elapsedTime < cookingTime)
        {
            microwavePlate.Rotate(Vector3.up * rotationSpeed * Time.deltaTime); // Gira más lento
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    private void StopMicrowave()
    {
        isMicrowaveRunning = false;
        microwaveLight.enabled = false;
        microwaveSound.Stop();
        microwaveSound.enabled = false;
        microwaveEndSound.enabled = true;
        microwaveEndSound.Play();

        if (foodInside != null)
        {
            FoodController foodController = foodInside.GetComponent<FoodController>();
            if (foodController != null)
            {
                foodController.HeatFood(); // Cambia el estado a caliente
            }
        }

        if (microwaveCollider != null)
        {
            microwaveCollider.enabled = false;
            Debug.Log("Collider del microondas desactivado. Ahora puedes recoger la comida.");
        }

        Debug.Log("Cocción completada. Puedes sacar la comida.");
    }

    public void ReactivateCollider()
    {
        if (microwaveCollider != null)
        {
            microwaveCollider.enabled = true;
            Debug.Log("Collider del microondas reactivado.");
        }
    }

    public void ShowText(bool flag)
    {
        interactionText.gameObject.SetActive(flag);
    }
}
