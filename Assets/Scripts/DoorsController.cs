using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DoorsController : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject door;
    [SerializeField] private float openRotationY;
    [SerializeField] private float rotationSpeed = 2f;

    public AudioSource openDoor;
    public AudioSource closeDoor;
    private bool isOpen = false;
    public TMP_Text interactionText;

    private void Start()
    {
        interactionText.gameObject.SetActive(false);
        door.transform.localEulerAngles = Vector3.zero; // Inicializa la rotación de la puerta.
        openDoor.enabled = false;
        closeDoor.enabled = false;
    }

    public void Interact()
    {
        isOpen = !isOpen;

        if (isOpen && openDoor != null)
        {
            openDoor.enabled = true;
            openDoor.Play();
        }
        else if (!isOpen && closeDoor != null)
        {
            closeDoor.enabled = true;
            closeDoor.Play();
        }
        StopAllCoroutines();
        StartCoroutine(RotateDoor());
    }

    private IEnumerator RotateDoor()
    {
        float targetY = isOpen ? openRotationY : 0f; // Determina el ángulo objetivo.
        float currentY = door.transform.localEulerAngles.y;

        // Corrige el ángulo actual para evitar problemas con rotaciones negativas.
        if (currentY > 180f) currentY -= 360f;

        while (Mathf.Abs(Mathf.DeltaAngle(currentY, targetY)) > 0.1f)
        {
            // Actualiza el ángulo interpolado.
            currentY = Mathf.MoveTowardsAngle(currentY, targetY, rotationSpeed * Time.deltaTime * 100f);
            door.transform.localEulerAngles = new Vector3(0, currentY, 0);
            yield return null;
        }

        // Asegura que la rotación final sea precisa.
        door.transform.localEulerAngles = new Vector3(0, targetY, 0);
    }

    public void ShowText(bool flag)
    {
        interactionText.gameObject.SetActive(flag);
    }
}
