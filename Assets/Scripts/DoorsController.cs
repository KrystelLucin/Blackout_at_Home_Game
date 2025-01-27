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
        door.transform.localEulerAngles = new Vector3(0, 0, 0);
    }

    public void Interact()
    {
        isOpen = !isOpen;
        StopAllCoroutines();
        StartCoroutine(RotateDoor());
    }

    private System.Collections.IEnumerator RotateDoor()
    {
        float targetY = isOpen ? openRotationY : 0f; // Determina el ángulo objetivo.
        float currentY = door.transform.localEulerAngles.y;

        while (Mathf.Abs(Mathf.DeltaAngle(currentY, targetY)) > 0.1f)
        {
            currentY = Mathf.MoveTowards(currentY, targetY, rotationSpeed * Time.deltaTime * 100f);
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
